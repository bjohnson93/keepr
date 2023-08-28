<template>
  <nav class="navbar navbar-expand-lg navbar-dark  px-3">
    <router-link class=" d-flex" :to="{ name: 'Home' }">
      <div class="d-flex p-4 flex-column align-items-center">
        <button class="home-button btn fw-bolder">Home</button>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <div class="dropdown">
  <button class="btn fw-bolder dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
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
import Login from './Login.vue';
import NewKeepModal from "./NewKeepModal.vue";
import NewVaultModal from "./NewVaultModal.vue";
export default {
  setup() {
    return {}
  },
  components: { Login, NewKeepModal, NewVaultModal }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
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
