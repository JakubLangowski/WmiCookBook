import Vue from 'vue'
import App from './views/layout/App.vue'
import router from './router'
import store from './store'
import api from "./plugins/api";
import Helper from "./plugins/helper";
import "@/plugins/vee-validate";
import '@/assets/css/main.scss'

Vue.use(Helper)

Vue.config.productionTip = false

new Vue({
    router,
    store,
    api,
    render: h => h(App)
}).$mount('#app')
