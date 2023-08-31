<template>
  <div v-if="keep" class="container-fluid ">
    <div class="modal fade modal-xl " id="keepModal" data-bs-backdrop="static" tabindex="-1" aria-labelledby="keep details modal" aria-hidden="true">
      <div class="modal-dialog ">
        <div class="modal-content bg-white">
          <div class="modal-body">
            <section class="row ">
              <div class="col-12 col-md-6 p-0 rounded">
                <img :src="keep.img" :alt="keep.name" class="img-fluid  keep-img   ">
              </div>
              <div class="col-12 col-md-6 m-auto d-flex flex-column justify-content-evenly align-content-center">
                <section class="row">
                  <div class="text-end ">
                    <button type="button" class="btn-close p-2" title="close" data-bs-dismiss="modal" aria-label="Close"></button>
                  </div>
                <div class="fs-4 d-flex justify-content-center col-12">
                  <div class=" mb-4">
                    <p><i class="mdi mdi-eye-outline "></i> {{ keep.views }} </p>
                  </div>
                  <div class="ms-3">
                    <p><i class="mdi mdi-alpha-k-box-outline"></i> {{ keep.kept }}</p>
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
                <div v-if="account.id" class="mt-4 mb-2 d-flex justify-content-center col-12">

                  
                  <form v-if="account.id" @submit.prevent="createVaultKeep()" class="d-flex justify-content-center align-items-center">
                    <select class="rounded py-2 select" v-model="editable.vaultId" required>
                      <option class="" selected default>Add to Vault</option>
                      <option v-for="v in vaults" :key="v.id" :value="v.id" class="btn lavender-button text-start">{{ v.name }}</option>
                    </select>
                    <button type="submit" class="ms-1  px-2 btn fs-5 dark-button image-text elevation-5 fw-bolder">save</button>
                  </form>
             
                </div>
                <router-link @click="closeModal()" :to="{name: 'Profile Page', params: {profileId: keep.creatorId}}">
                <div :title="`see the page for ${keep.creator.name}`" class="d-flex align-items-center p-2 mb-2 justify-content-center">
                  <img :src="keep.creator.picture" :alt="keep.creator.name" class="avatar elevation-5">
                  <p class="ms-2 fw-bolder my-0 text-dark">{{ keep.creator.name }}</p>
                </div>
              </router-link>
              <div v-if="account.id == keep?.creatorId" class="text-center my-2">
                <button role="button" @click="deleteMyKeep(keep.id)" v-if="account.id == keep.creator.id" class="btn  btn-danger delete-icon" title="Delete Your Keep">Delete Keep <i class="mdi mdi delete-icon"></i></button>
              </div>
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
import { computed, ref,  } from "vue";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js";

import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";
import { Modal } from "bootstrap";
import { keepsService } from "../services/KeepsService.js";
import { router } from "../router.js";

export default {
  setup(){
    const editable = ref({})
    return {
      editable,
      closeModal(){
        
        Modal.getOrCreateInstance('#keepModal').hide()
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
      },
      async deleteMyKeep(keepId){
        try 
        {
          const wantsToDelete = await Pop.confirm("ARE YOU SURE YOU WANT TO DELETE?")
          if(!wantsToDelete){
            return
          }
          await keepsService.deleteMyKeep(keepId)
          Pop.toast("You successfully deleted your keep!")
          Modal.getOrCreateInstance('#keepModal').hide()
          router.push({name: 'Home'})
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
  object-fit: cover;
  width: auto;
  height: 100%;
  object-position: center;
  border-top-left-radius: 2%;
  border-bottom-left-radius: 2%;
  // padding-left: 10px;
}
.modal-body{
  padding: 0;
}
@media screen and (max-width: 779px) {
  .keep-img{
  object-fit: cover;
  width: auto;
  padding: 12px;
  padding-top: 0;
  object-position: center;
  border-top-left-radius: 2%;
  border-top-right-radius: 2%;
}
}
</style>