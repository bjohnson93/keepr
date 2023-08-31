<template>
  <div class=" container-fluid mt-3 ">
    <section class=" row  p-2">
      <!-- had masonry on container fluid and card-columns on row -->
      <!-- <div @click="openKeepCard()" class=" mb-4 " v-for="keep in keeps" :key="keep.id" > -->
      <div @click="openKeepCard()" class="col-md-3 col-6 mb-4 p-md-3 p-1" v-for="keep in keeps" :key="keep.id" >

        <KeepCard :keep="keep"/>
        <!-- <ActiveKeepModal/> -->

      </div>
    </section>
  </div>
</template>

<script>
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js"
import { computed, onMounted, onUnmounted, watchEffect } from "vue";
import { AppState } from "../AppState.js"
import KeepCard from "../components/KeepCard.vue";
import { Modal } from "bootstrap";

export default {
    setup() {
      onUnmounted(() => {
      Modal.getOrCreateInstance('#keepModal').hide()
      
    })
        watchEffect(() => {
          getKeeps();
        })
        async function getKeeps() {
            try {
                await keepsService.getKeeps();
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        return {

            keeps: computed(() => AppState.keeps),
            openKeepCard(){
              Modal.getOrCreateInstance("#keepModal").show()
            }
        };
    },
    components: { KeepCard }
}
</script>

<style scoped lang="scss">
body{
  padding-right: 0;
}

body {
  margin: 8px;
}

.masonry { /* Masonry container */
    -webkit-column-count: 4; /*number of columns in row*/
    -moz-column-count:4;
    column-count: 4;
    -webkit-column-gap: 3em;
    -moz-column-gap: 3em;
    column-gap: 3em; /*no difference seen/
    // margin: 1.5em; /*taking this off made horizontal scroll go away*/
    padding: 0; /*no difference seen*/
    -moz-column-gap: 3em; /*doesn't seem like it does much that I can tell*/
    -webkit-column-gap: 3em; /*doesn't seem like it does much that I can tell*/
    column-gap: 1.5em;  /*vertical gap between columns*/
    font-size: .85em; /*shrinks width of cards...*/
}
.card-columns{
  display:grid;
}


@media screen and (max-width: 779px) {
  .masonry { /* Masonry container */
    -webkit-column-count: 2;
    -moz-column-count:2;
    column-count: 2;
    -webkit-column-gap: 2em;
    -moz-column-gap: 1em;
    column-gap: 2em;
    margin: 1.5em;
    padding: 0;
}
}


</style>
