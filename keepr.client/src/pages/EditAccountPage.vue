<template>
  <div v-if="account" class="container-fluid">
    
    <section class="row">
      <div class="col-12 p-0">
        <div class="image-container">
          <img class="img-fluid account-img me-4" :src=account.picture :alt=account.name>
          <img class="coverImg-style img-fluid" :src="account.coverImg" :alt=account.name>
        </div>
      </div>
    </section>

    <section class="row mt-5 pt-5">
      <div class="col-10 mx-auto">
        <p class="fs-1">
          Edit Your Account
        </p>
      </div>
    </section>

    <section class="row justify-content-center">
      <div class="col-10">
          <form action="" class="row justify-content-between" @submit.prevent="editAccount()">
            <div class="col-md-6 col-12">
              <div class="mb-3">
                <label for="name">Name</label>
                <input v-model="editable.name" type="text" id="name" name="name" title="Name" class="form-control" maxlength="100" minlength="1" required>
              </div>
              <div class="mb-3">
                <label for="coverImg">Cover Image</label>
                <input v-model="editable.coverImg" type="url" id="coverImg" name="coverImg" title="Cover Image" class="form-control" maxlength="700" minlength="1">
              </div>
              <div class="mb-3">
                <label for="picture">Profile Picture</label>
                <input v-model="editable.picture" type="url" id="picture" name="picture" title="Profile Picture" class="form-control" maxlength="400" minlength="1">
              </div>
            </div>
              <div>
                <button type="submit" class="btn dark-button">
                  Save Changes
                </button>
              </div>
          </form>
      </div>
    </section>
  </div>
</template>


<script>
import { computed, ref, watchEffect } from "vue"
import { useRouter } from "vue-router"
import { AppState } from "../AppState.js"
import Pop from "../utils/Pop.js"
import { accountService } from "../services/AccountService.js"
import { logger } from "../utils/Logger.js"
import { router } from "../router.js"

export default {
  setup(){
    const editable = ref({})
    watchEffect(() => {
      AppState.account
      AppState.profile
      editable.value = {...AppState.account}
    })
    return {
      editable,

      account: computed(() => AppState.account),

      async editAccount(){
        try {
          const accountData = editable.value
          await accountService.editAccount(accountData)
          Pop.toast('Saved Changes', 'success', 'bottom-end')
          router.push({name: 'Account'})
        } catch (error) {
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.coverImg-style{
  height: 100%;
  width: 100%;
  object-fit: cover;
  object-position: center;
}


.account-img{
  height: 17vh;
  width: 17vh;
  border-radius: 50%;
  object-fit: cover;
  object-position: center;
  bottom: -8vh;
  left: 5vw;
  position: absolute;
}

.image-container {
  height: 30vh;
  position: relative;
}

.account-info-style{
  top: -6.5vh;
  margin-bottom: -6.5vh;
  position: relative;
}

@media(min-width: 768px){
  .account-info-style{
  top: -6.5vh;
  margin-bottom: -6.5vh;
  position: relative;
}
}
</style>