<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />

    <ActiveKeepModal/>
    <NewVaultModal/>
    <NewKeepModal/>
  </main>
</template>

<script>
import { computed, watchEffect } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import { Keep } from "./models/Keep.js"
import ActiveKeepModal from "./components/ActiveKeepModal.vue"
import NewVaultModal from "./components/NewVaultModal.vue"
import NewKeepModal from "./components/NewKeepModal.vue"
import KeepCard from "./components/KeepCard.vue"
import { vaultsService } from "./services/VaultsService.js"
import Pop from "./utils/Pop.js"
import { AuthService } from "./services/AuthService.js"

export default {
  setup() {
    watchEffect(() => {
      AppState.account
      AppState.activeKeep
      // AppState.myVaults
      // getMyVaults()
    })
    // async function getMyVaults(){
    //       try 
    //       {
            
    //       await AuthService.getMyVaults()
    //       }
    //       catch (error)
    //       {
    //         Pop.error(error.message)
    //       }
    //     }
    return {
      appState: computed(() => AppState),
      keep: computed(() => AppState.activeKeep)
    }
  },
  components: { Navbar, ActiveKeepModal, NewVaultModal, NewKeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
@import url('https://fonts.googleapis.com/css2?family=Dancing+Script&family=Raleway&display=swap" rel="stylesheet');




:root{
  --main-height: calc(100vh - 32px - 64px);
}
colors{
  color: #C5BAE0;
  color: #443331;
  color: #2D2D2D;
  color: #E9D8D6;  
  color: #DDD5E0;
  color: #F2E1D9;
  color: #FAF4FC;
  color: #DED6E9;
  color: #FEF6F0;
  color: #877A8F;
  color: #919191;
}
body{
  padding-right: 0;
  font-family: 'Raleway', sans-serif;
}

main{
  background-color: #FEF6F0;
  color: #2D2D2D;
}

.darkest-button{
  background-color: #2D2D2D;
  color: #FEF6F0;
}

.home-button{
  background-color: #E9D8D6;
}
.lavender-button{
  background-color: #C5BAE0;
  color: #2D2D2D;
}
.navbar-head{
  font-family: 'Dancing Script', cursive;
}
.dark-button{
  background-color: #716778;
  font-weight: 600;
  color: #FAF4FC;
}
.drop-downs{
  background-color: #DED6E9;
}
.image-text{
  color: #FAF4FC;
  text-shadow: 1px 1px 5px #443331;
}
</style>
