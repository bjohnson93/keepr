<template>
  <div class="container-fluid mt-3">
    <section class="row">
      <div class="col-6 col-md-3 mb-4" v-for="keep in keeps" :key="keep.id">

        <KeepCard :keep="keep"/>

      </div>
    </section>
  </div>
</template>

<script>
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js"
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js"
import KeepCard from "../components/KeepCard.vue";

export default {
    setup() {
        onMounted(() => {
            getKeeps();
        });
        async function getKeeps() {
            try {
                await keepsService.getKeeps();
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        return {
            keeps: computed(() => AppState.keeps)
        };
    },
    components: { KeepCard }
}
</script>

<style scoped lang="scss">

</style>
