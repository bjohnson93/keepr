<template>
  <div v-if="account" class="container-fluid">
    <section class="row">
      <div class="col-12 d-flex justify-content-center align-items-center">
        <img :src="account.coverImg" class="cover-img rounded">
      </div>
    </section>
    <section class="row">
      <div class="col-12 d-flex flex-column justify-content-center align-items-center">
        <img :src="account.picture" :alt="account.name" class="avatar-profile mb-5">
      </div>
      
      <div class="col-12 d-flex flex-column align-items-center justify-content-center mt-5">
        <div>
          <h1>{{ account.name }}</h1>
        </div>
        <div>
          <p>{{ myVaults.length }} Vaults | {{myKeeps.length}} Keeps</p>
        </div>
      </div>
    </section>

    <section class="row d-flex justify-content-center align-items-center mb-4">
      <div class="col-md-9 col-12">
        <h2 class="mb-4">Vaults</h2>
        <section class="row">
          <div class="col-6 col-md-3 mb-3" v-for="vault in myVaults" :key="vault.id">
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
          <div @click="openKeepCard()" class="col-6 col-md-3 mb-4" v-for="keep in myKeeps" :key="keep.id">
    
            <KeepCard :keep="keep"/>
            
          </div>
        </section>
      </div>
    </section>

  </div>


</template>

<script>
import { computed, onMounted, watchEffect } from 'vue';
import { AppState } from '../AppState';
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { useRoute } from "vue-router";
import { keepsService } from "../services/KeepsService.js";
import { Account, Profile } from "../models/Account.js";
import { logger } from "../utils/Logger.js";
import { router } from "../router.js";
import { Modal } from "bootstrap";
export default {

  setup() {
    const route = useRoute()
    onMounted(() => {
      getMyKeeps()
    })
        watchEffect(() => {
          AppState.account
          AppState.myKeeps
          AppState.myVaults
          AppState.keeps
        })
    async function getMyKeeps() {
            try {
                const profileId = AppState.account.id
                await keepsService.getUsersKeeps(profileId);
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
    return {
            account: computed(() => AppState.account),
            myKeeps: computed(() => AppState.myKeeps),
            myVaults: computed(() => AppState.myVaults),
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
                router.push({name: 'Account'})
                Pop.toast("You successfully deleted your vault!")
              }
              catch (error)
              {
                Pop.error(error.message)
              }
            }
    }
  }
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
  // position: relative;
  // top: 1vh;
  // left: 28vh;
  text-shadow: 1px 1px 5px rgb(248, 243, 243);
}
</style>
