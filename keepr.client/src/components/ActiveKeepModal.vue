<template>
  <div v-if="keep" class="container-fluid">
    <div class="modal fade modal-xl " id="keepModal" tabindex="-1" aria-labelledby="keep details modal" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content bg-white">
          <div class="modal-body">
            <section class="row">

          <div class="col-12 col-md-6 p-0">
            <img :src="keep.img" :alt="keep.name" class="img-fluid rounded keep-img m-auto">
          </div>
            <div class="col-12 col-md-6 d-flex flex-column justify-content-evenly align-content-center">
              <section class="row">
                <div class="fs-4 d-flex justify-content-center col-12">
                  <div class="me-3 mb-4">
                    <i class="mdi mdi-eye-outline"></i> {{ keep.views }}
                  </div>
                  <div class="ms-3">
                    <i class="mdi mdi-alpha-k-box-outline"></i> {{ keep.kept }}
                  </div>
                </div>
              </section>
              <section class="row ">
                <div class="col-12 p-3 d-flex flex-column justify-content-center align-items-center">
                  <p class="fs-3 text-center fw-bold">{{ keep.name }}</p>
                  <p>{{ keep.description }}</p>
                </div>
              </section>
              <section class="row ">
                <div v-if="account.id" class="mt-4 d-flex justify-content-center col-12">

                  
                  <form v-if="account.id" @submit="createVaultKeep()" class="d-flex justify-content-center align-items-center">
                    <select class="rounded py-2 select" v-model="editable.vaultId" required>
                      <option class="" selected default>Add to Vault</option>
                      <option v-for="v in vaults" :key="v.id" :value="v.id" class="btn lavender-button text-start">{{ v.name }}</option>
                    </select>
                    <button type="submit" class="ms-1 px-4 btn fs-4 dark-button image-text elevation-5 fw-bolder">save</button>
                  </form>
             
                </div>
                <router-link @click="closeModal()" :to="{name: 'Profile Page', params: {profileId: keep.creatorId}}">
                <div :title="`see the page for ${keep.creator.name}`" class="d-flex align-items-center p-2 justify-content-center">
                  <img :src="keep.creator.picture" :alt="keep.creator.name" class="avatar elevation-5">
                  <p class="ms-2 fw-bolder my-0 text-dark">{{ keep.creator.name }}</p>
                </div>
              </router-link>
              </section>
            </div>
    
        </section>
          </div>
        </div>
      </div>
    </div>
  </div>

 
</template>


<script>
import { computed, onMounted, onUnmounted, ref, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js";

import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";
import { Modal } from "bootstrap";

export default {
  setup(){
    const editable = ref({})
    // onUnmounted(() => {
    //   Modal.getOrCreateInstance('#keepModal').hide()
    // })
    return {
      editable,
      closeModal(){
        
        Modal.getOrCreateInstance('#newVaultModal').hide()
      },
      keep: computed(() => AppState.activeKeep),
        
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),

      async createVaultKeep() {
        try 
        {
          const vaultKeepData = editable.value
          vaultKeepData.keepId = this.keep.id
          logger.log('is anything', vaultKeepData)
          await vaultKeepsService.createVaultKeep(vaultKeepData)
          AppState.activeKeep.kept++
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
.avatar{
  border-radius: 50%;
  width: 5vh;
  height: 5vh;
  object-fit: cover;
  object-position: center;
}

.keep-img{
  object-fit: contain;
  width: 100%;
  object-position: center;
  padding-left: 10px;
}
.modal-body{
  padding: 0;
}
@media screen and (max-width: 779px) {
  .keep-img{
  object-fit: contain;
  width: 100%;
  padding: 20px;
  object-position: center;
}
}
</style>