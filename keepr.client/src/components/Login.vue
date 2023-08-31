<template>
  <span class="navbar-text">
    <button class="btn selectable  fw-bolder text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown my-2 my-lg-0">
        <div type="button" title="see account options" class=" border-0 selectable no-select" data-bs-toggle="dropdown" >
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" role="button" title="Account Options" height="40" class=" elevation-5 circle-pic" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-end dropdown-menu-start p-0" title="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                See my Account
              </div>
            </router-link>
            <router-link :to="{ name: 'Edit Account Page' }" v-if="account.id">
              <li class="text-start dropdown-item  mb-2">
                Edit Account
              </li>
            </router-link>
            <!-- <router-link :to="{ name: 'Profile Page', params: {profileId: account?.Id} }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Go to my Profile
              </div>
            </router-link> -->
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.circle-pic{
  border-radius: 50%;
  object-fit: cover;
  object-position: center;
  width: 5vh;
  height: 5vh;
}
</style>
