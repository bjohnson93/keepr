import '@mdi/font/css/materialdesignicons.css'
import 'bootstrap'
import { createApp } from 'vue'
// @ts-ignore
import App from './App.vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import { router } from './router'
// import { VueMasonryPlugin } from "vue-masonry"
import './utils/SocketProvider.js'

// VueMasonryPlugin.config.productionTip = false;

// VueMasonryPlugin.use(Vuetify);
// VueMasonryPlugin.use(VueMasonryPlugin);

// new VueMasonryPlugin({
//   render: (h) => h(App)
// }).$mount("#app");

const root = createApp(App)
registerGlobalComponents(root)

root
  .use(router)
  .mount('#app')

