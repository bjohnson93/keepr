<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />

  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import { Keep } from "./models/Keep.js"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

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

main{
  background-color: #FEF6F0;
  color: #2D2D2D;
}

.home-button{
  background-color: #E9D8D6;
}

.dark-button{
  background-color: #877A8F;
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
