<template>
  <div class="modal fade" id="newKeepModal" tabindex="-1" title="newKeepModalLabel" aria-hidden="true">
  <div class="modal-dialog ">
    <div class="modal-content bg-white">
      <div class="modal-header border-0 ms-2">
        <h5 class="modal-title fs-3" id="newKeepModalTitle">Add your keep</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">

        <form @submit.prevent="createKeep()" id="newKeepModal" class="p-2 mb-3">
          <div class="mb-3">
            <input v-model="editable.name" class="form-control bg-white border-0 border-bottom fs-5" placeholder="Name..." type="text" minlength="1" maxlength="30" required>
          </div>
          <div class="mb-5">
            <input v-model="editable.img" class="form-control bg-white border-0 border-bottom fs-5" placeholder="Image URL..." type="url" minlength="1" maxlength="500" required>
          </div>
          <div class="mb-5">
            <textarea v-model="editable.description" class="form-control bg-white border-0 border-bottom fs-5" placeholder="Keep Description..." minlength="1" maxlength="400" cols="30" rows="2" required></textarea>
          </div>
          <div class="text-end mb-2">
            <button type="submit" title="save keep" class="btn darkest-button">Create</button>
          </div>
        </form>
        </div>
      </div>
    </div>
</div>
</template>


<script>
import { ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";

export default {
  setup(){
    const editable = ref({})
    return {
      editable,
      async createKeep(){
        try 
        {
        const keepData = editable.value
        // logger.log('[CREATING KEEP....]', keepData)
        await keepsService.createKeep(keepData)
        editable.value = {}
        Modal.getOrCreateInstance('#newKeepModal').hide()
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