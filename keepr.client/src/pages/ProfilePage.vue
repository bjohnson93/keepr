<template>
  <div v-if="profile" class="container-fluid">
    <section class="row">
      <div class="col-12 d-flex justify-content-center align-items-center">
        <img :src="profile.coverImg" :alt="profile.name" class="cover-img rounded">
      </div>
    </section>
    <section class="row">
      <div class="col-12 d-flex flex-column justify-content-center align-items-center">
        <img :src="profile.picture" :alt="profile.name" class="avatar-profile mb-5">
      </div>
      
      <div class="col-12 d-flex flex-column align-items-center justify-content-center mt-5">
        <div>
          <h1>{{ profile.name }}</h1>
        </div>
        <div>
          <p>{{ usersVaults.length }} Vaults | {{usersKeeps.length}} Keeps</p>
        </div>
      </div>
    </section>

    <section class="row d-flex justify-content-center align-items-center mb-4">
      <div class="col-md-9 col-12">
        <h2 class="mb-4">Vaults</h2>
        <section class="row">
          <div class="col-6 col-md-3 mb-3" v-for="vault in usersVaults" :key="vault.id">
            <router-link :to="{name: 'Vault Page', params: {vaultId: vault.id}}">
              <div :style="{'background-image': `url(${vault?.img})`}" class="m-auto rounded vault-card img-fluid elevation-4 d-flex flex-sm-column align-items-end justify-content-between selectable">
                <i role="button" @click="deleteMyVault(vault.id)" v-if="account.id == vault.creatorId" class="mdi mdi-alpha-x-circle text-danger fs-5 delete-icon selectable" title="Delete Your Vault"></i>
              <p class="fs-4 image-text p-3 fw-bolder">{{ vault.name.toUpperCase() }}</p>
              <i v-if="vault.isPrivate" class="mdi mdi-lock image-text fs-3 p-1"></i>
            </div>
          </router-link>
          </div>
        </section>

      </div>
    </section>

    <section class="row d-flex justify-content-center align-items-center">
      <div class="col-md-9 col-12">
        <h2 class="mb-4">Keeps</h2>
        <section class="row">

          <div @click="openKeepCard()" class="col-6 col-md-3 mb-4" v-for="keep in usersKeeps" :key="keep.id">
    
            <KeepCard :keep="keep"/>
            <!-- <ActiveKeepModal/> -->
    
          </div>
        </section>
      </div>
    </section>

  </div>

  <div v-else class="container-fluid">
    <section class="row d-flex justify-content-center align-items-center">
      <div class="col-12 p-3 text-center">
       <h1><i class="mdi mdi-clock"> </i> Loading...</h1> 
      </div>
    </section>
  </div>
</template>


<script>
import { computed, onMounted, watchEffect } from "vue";
import { vaultsService } from "../services/VaultsService.js"
import Pop from "../utils/Pop.js";
import { useRoute } from "vue-router";
import { profilesService } from "../services/ProfilesService.js"
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import KeepCard from "../components/KeepCard.vue";
import { Modal } from "bootstrap";
export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            route.params.profileId
            getUsersProfile();
        });
        watchEffect(() => {
          getUsersKeeps();
          getUsersVaults();
        })
        async function getUsersProfile() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getUsersProfile(profileId);
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        async function getUsersKeeps() {
            try {
                const profileId = route.params.profileId;
                await keepsService.getUsersKeeps(profileId);
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        async function getUsersVaults(){
          try 
          {
             const profileId = route.params.profileId;
             await vaultsService.getUsersVaults(profileId);
          }
          catch (error)
          {
            Pop.error(error.message)
          }
        }

        return {
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            usersKeeps: computed(() => AppState.usersKeeps),
            usersVaults: computed(() => AppState.usersVaults),
            openKeepCard(){
              Modal.getOrCreateInstance("#keepModal").show()
            },
            async deleteMyVault(vaultId){
              try 
              {
                const wantsToDelete = await Pop.confirm("Are you sure you want to delete your vault?")
                if(!wantsToDelete){
                  return
                }
                await vaultsService.deleteMyVault(vaultId)
                Pop.toast("You successfully deleted your vault!")
              }
              catch (error)
              {
                Pop.error(error.message)
              }
            }
        };
    },
    components: { KeepCard }
}
</script>


<style lang="scss" scoped>
.cover-img{
  height: 40vh;
  width: 100vh;
  object-fit: cover;
  object-position: center;
}

.avatar-profile{
  width: 10vh;
  height: 10vh;
  object-fit: cover;
  object-position: center;
  border-radius: 50%;
  position: absolute;
  top: 41vh;
  box-shadow: 2px 2px 10px gray;
}

.vault-card{
  height: 25vh;
  object-fit: cover;
  object-position: center;
}


.delete-icon{
  // display: block;
  // position: absolute;
  // top: 1vh;
  // left: 28vh;
  text-shadow: 1px 1px 5px rgb(248, 243, 243);
}


</style>