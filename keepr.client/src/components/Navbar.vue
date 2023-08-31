<template>
  <nav class="navbar navbar-expand-lg navbar-dark  px-3">
    <router-link class=" d-flex" :to="{ name: 'Home' }">
      <div class="d-flex p-4 flex-column align-items-center">
        <button class="home-button btn fw-bolder">Home</button>
      </div>
    </router-link>
    <!-- <img src="KeeprLogo" class="img-fluid" alt=""> -->

    <button class="navbar-toggler " type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="dark-button rounded navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          
          <div class="dropdown">
  <button v-if="account" class="btn fw-bolder dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
    create
  </button>
  <ul class="dropdown-menu lavender-button" aria-labelledby="dropdownMenuButton1">
    <li><button class="btn dropdown-item lavender-button fw-bolder" data-bs-toggle="modal" data-bs-target="#newKeepModal">new keep</button></li>
    <li><button class="btn dropdown-item lavender-button fw-bolder" data-bs-toggle="modal" data-bs-target="#newVaultModal">new vault</button></li>
  </ul>
</div>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
  <NewKeepModal/>
  <NewVaultModal/>
</template>

<script>
import { computed } from "vue";
// import {KeeprLogo} from "../assets/img/KeeprLogo.jpg";
import { AppState } from "../AppState.js";
import Login from './Login.vue';
import NewKeepModal from "./NewKeepModal.vue";
import NewVaultModal from "./NewVaultModal.vue";
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      // KeeprLogo
    }
  },
  components: { Login, NewKeepModal, NewVaultModal }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Dancing+Script&display=swap" rel="stylesheet');
a:hover {
  text-decoration: none;
}

.logo{
  font-family: 'Dancing Script', cursive;
}
.navbar{
  background-color: #FEF6F0;
}
.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
