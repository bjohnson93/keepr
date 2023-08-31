<template>
  <div v-if="vault" class="container-fluid">
    <section class="row">
      <div class="col-12 d-flex justify-content-center align-items-center">
        <img :src="vault.img" :alt="vault.name" class="cover-img rounded">
      </div>
    </section>
    <section class="row">
      
      <div class="col-12 d-flex flex-column align-items-center justify-content-center mt-5 image-text vault-info fw-bolder">
      
          <p class="fs-2">{{ vault.name.toUpperCase() }}</p>
          <p class="fs-5">by {{ vault.creator.name }}</p>
      </div>
 
        <div class="col-12 d-flex justify-content-center align-items-center">
          <p class="mt-3">{{keeps.length}} Keeps</p>
        </div>
    </section>

<!-- //FIXME - need to change keep card in vault to be a VAULT KEEP, and have a DELETE VAULT KEEP function -->
        <section class="row d-flex justify-content-center align-items-center">
          <div class="col-md-9 col-12">
            <section class="row">
              <div @click="openKeepCard()" class="col-6 col-md-3 mb-4" v-for="keep in keeps" :key="keep.id">
                <KeepCard :keep="keep"/>
              </div>
            </section>
          </div>
        </section>

  </div>
</template>


<script>
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import {vaultKeepsService} from "../services/VaultKeepsService.js"
import KeepCard from "../components/KeepCard.vue";
import { router } from "../router.js";
import { Modal } from "bootstrap";

export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            route.params.vaultId;
            getVaultById();
            getKeepsByVaultId();
        });
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                await vaultsService.getVaultById(vaultId);
            }
            catch (error) {
                Pop.error(error.response.data);
                if(error.response.data.includes('Nope')){
                  router.push({name: "Home"})
                }
            }
        }
        async function getKeepsByVaultId() {
            try {
                const vaultId = route.params.vaultId;
                await vaultKeepsService.getKeepsByVaultId(vaultId);
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        return {
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.vaultKeeps),
            openKeepCard(){
              Modal.getOrCreateInstance("#keepModal").show()
            }
        };
    },
    components: { KeepCard }
}
</script>


<style lang="scss" scoped>
.cover-img{
  height: 25vh;
  width: 70vh;
  object-fit: cover;
  object-position: center;
}
.vault-info{
  object-fit: cover;
  object-position: center;
  position: absolute;
  top: 9vh;
}
</style>