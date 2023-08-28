<template>

 <div class="modal fade" id="newVaultModal" tabindex="-1" aria-labelledby="newVaultModal" aria-hidden="true">
  <div class="modal-dialog ">
    <div class="modal-content bg-white">
      <div class="modal-header border-0 ms-2">
        <h5 class="modal-title fs-3" id="newVaultModalLabel">Add your vault</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">

        <form @submit.prevent="createVault()" class="p-2 mb-3">
          <div class="mb-3">
            <input v-model="editable.name" class="form-control bg-white border-0 border-bottom fs-5" placeholder="Name..." type="text" minlength="1" maxlength="30" required>
          </div>
          <div class="mb-5">
            <input v-model="editable.img" class="form-control bg-white border-0 border-bottom fs-5" placeholder="Image URL..." type="url" minlength="1" maxlength="500" required>
          </div>
          <div class="mb-5">
            <textarea v-model="editable.description" class="form-control bg-white border-0 border-bottom fs-5" placeholder="Keep Description..." minlength="1" maxlength="400" cols="30" rows="2" required></textarea>
          </div>
          <div class="mb-5 form-check d-flex justify-content-end">
            <div>

              <p class="text-secondary small-text m-auto ">Private Vaults can only be seen by you</p>
              <input v-model="editable.isPrivate" class="form-check-input border-0" type="checkbox" id="privateCheck" title="make vault private?">
              <label for="privateCheck" class="form-check-label">
                Make Vault Private?
              </label>
            </div>
          </div>
          <div class="text-end mb-2">
            <button type="submit" title="save keep" class="btn darkest-button">Create Vault</button>
          </div>
        </form>
        </div>
      </div>
    </div>
</div>
</template>


<script>
import { ref, watchEffect } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";

export default {
  setup(){
    const editable = ref({})
    return {
      editable,
      async createVault(){
        try 
        {
        const vaultData = editable.value
        // logger.log('[CREATING VAULT...]', vaultData)
        await vaultsService.createVault(vaultData)
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
.small-text{
  font-size: 10px;
}
</style>