<template>
  <div v-if="keep" class="container-fluid">
    <div class="modal fade modal-xl" id="keepModal" tabindex="-1" aria-labelledby="keep details modal" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-body">
            <section class="row">

          <div class="col-6 p-0">
            <img :src="keep.img" :alt="keep.name" class="img-fluid rounded-top rounded-bottom keep-img">
          </div>
            <div class="col-6 d-flex flex-column justify-content-evenly align-content-center">
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
              <section class="row">
                <div class="col-12">
                  <p class="fs-3 text-center fw-bold">{{ keep.name }}</p>
                  <p>{{ keep.description }}</p>
                </div>
              </section>
              <section class="row">
                <div class="mt-4 d-flex justify-content-between col-12">
                  <p class="fs-4">create</p>
                  <button class="px-4 btn fs-4 dark-button image-text elevation-5 fw-bolder">save</button>
                  <div :title="`see the page for ${keep.creator.name}`" class="d-flex align-items-center">
                    <img :src="keep.creator.picture" :alt="keep.creator.name" class="avatar elevation-5">
                    <p class="ms-2 fw-bolder">{{ keep.creator.name }}</p>
                  </div>
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
import { computed } from "vue";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js";

export default {
  setup(){
    async function getKeepById(){
      try 
      {
        // let keepId = 
      }
      catch (error)
      {
        Pop.error(error.message)
      }
    }
    return {
      keep: computed(() => AppState.activeKeep)
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
}
.modal-body{
  padding: 0;
}
</style>