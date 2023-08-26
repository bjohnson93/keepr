<template>
  <div @click="setActiveKeep(keep.id)" data-bs-toggle="modal" data-bs-target="#keepModal" title="see the details of this keep" :style="{'background-image': `url(${keep?.img})`}" class="m-auto rounded keep-card img-fluid elevation-5 d-flex flex-sm-column align-items-end justify-content-between p-3 selectable">
    

      <p class="fs-3 image-text ps-2 fw-bolder">{{ keep.name }}</p>
        <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" class="avatar elevation-5">
        </div>

</template>


<script>
import { watchEffect } from "vue";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js"

export default {
  props: { 
    keep: {Type: Keep, required: true}
  },
  setup(props){
    watchEffect(() => {
      AppState.activeKeep
    })
    return {
      async setActiveKeep(){
        try 
        {
          await keepsService.setActiveKeep(props.keep)
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
.keep-card{
  width: 100%;
  height: 50vh;
  background-position: center;
  background-size: cover;
}
.avatar{
  width: 8vh;
  height: 8vh;
  object-fit: cover;
  object-position: center;
  border-radius: 50%;
}
</style>