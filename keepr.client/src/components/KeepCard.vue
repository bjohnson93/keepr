<template>
  <!-- <div @click="setActiveKeep(keep.id)"  title="see the details of this keep" :style="{'background-image': `url(${keep?.img})`}" class="masonry-with-flex  rounded keep-card img-fluid elevation-5 p-3 selectable">
    <i role="button" @click="deleteMyKeep(keep.id)" v-if="account.id == keep.creator.id" class="mdi mdi-alpha-x-circle text-danger fs-5 delete-icon" title="Delete Your Keep"></i>
    <div class="">

      <p class="fs-md-3 fs-5 image-text mt-4">{{ keep.name }}</p>
        <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" class="avatar elevation-5">
    </div>
        </div> -->

<!-- --original div without masonry-- -->
  <div @click="setActiveKeep(keep.id)"  title="see the details of this keep" :style="{'background-image': `url(${keep?.img})`}" class="keep-card fluid m-auto rounded img-fluid elevation-5 d-flex flex-sm-column align-items-end justify-content-between p-3 selectable">


      <p class="fs-md-3 fs-5 image-text fw-bolder mt-4">{{ keep.name }}</p>
        <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" class="ms-1 avatar elevation-5">

        </div>
</template>


<script>
import { computed, watchEffect } from "vue";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js"
import { Modal } from "bootstrap";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";

export default {
  props: { 
    keep: {Type: Keep, required: true}
  },
  setup(props){
    watchEffect(() => {
      AppState.activeKeep
    })
    return {
      account: computed(() => AppState.account),
      async setActiveKeep(){
        try 
        {
          // logger.log('[keep prop..]', props.keep.id)
          await keepsService.setActiveKeep(props.keep)
          AppState.activeKeep.views++;
          const keepId = props.keep.id
          await keepsService.getKeepById(keepId)
        }
        catch (error)
        {
          Pop.error(error.message)
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.keep-card{
  width: 100%;
  height: 40vh;
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

.delete-icon{
  position: absolute;
  top: -1.5vh;
  right: -1vh;
  text-shadow: 1px 1px 5px rgb(248, 243, 243);
}

@media screen and (max-width: 779px) {
  .keep-card{
    height: 25vh;
  }
}



// body {
//   margin: 8px;
// }

// .masonry-root {
//   display: flex;
//   flex-direction: column;
//   flex-wrap: wrap;
//   .masonry-cell {
//     flex: 1;
//     margin: 4px;
//     .masonry-item {
//       color: white;
//       margin: px;
//       text-align: center;
//       font-family: system-ui;
//       font-weight: 900;
//       font-size: 32px;
//     }
//   } 
//   @for $i from 1 through 512 { 
//     .masonry-cell:nth-child(#{$i}) {
//       .masonry-item {
//         $h: (random(128) + 64) + px;
//         height: $h;
//         line-height: $h;
//       }
//     }
//   }
// }

// body {
//   margin: 0;
//   padding: 1rem;
// }

// .masonry-with-flex {
//   display: flex;
//   flex-direction: column;
//   flex-wrap: wrap;
//   max-height: 1000px;
//   div {
//     width: 150px;
//     color: white;
//     margin: 0 1rem 1rem 0;
//     text-align: center;
//     font-family: system-ui;
//     font-weight: 900;
//     font-size: 2rem;
//   } 
//   @for $i from 1 through 36 { 
//     div:nth-child(#{$i}) {
//       $h: (random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
//   }
// }


</style>