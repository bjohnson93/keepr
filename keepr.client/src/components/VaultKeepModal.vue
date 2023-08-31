<template>
    <div @click="setActiveVaultKeep(keep.id)"  title="see the details of this keep" :style="{'background-image': `url(${keep?.img})`}" class="keep-card fluid m-auto rounded img-fluid elevation-5 d-flex flex-sm-column align-items-end justify-content-between p-3 selectable">
    <i role="button" @click="deleteVaultKeep(keep.id)" v-if="account.id == keep.creator.id" class="mdi mdi-alpha-x-circle text-danger fs-5 delete-icon" title="Delete Your Keep"></i>


      <p class="fs-md-3 fs-5 image-text fw-bolder mt-4">{{ keep.name }}</p>
        <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" class="ms-1 avatar elevation-5">

        </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    vaultKeep: {Type: VaultKeep, required: true}
  },
  setup(props){
    return {
      account: computed(() => AppState.account),
      async setActiveVaultKeep(){
        try 
        {
          await vaultKeepsService.setActiveVaultKeep(props.vaultKeep)
          AppState.activeVaultKeep.views++;
          const vaultKeepId = props.vaultKeep.id
          // await vaultKeepsService.getVaultKeepById(vaultKeepId)
          // **FIXME - not sure if we need this above?
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

</style>