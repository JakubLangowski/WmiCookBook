import { createApp } from 'vue'
import App from "@/views/layout/App";
import router from './router'
import store from './store'
import api from "./plugins/api";
import helper from "./plugins/helper";
import '@/assets/css/main.scss'


createApp(App)
    .use(store)
    .use(router)
    .use(api)
    .use(helper)
    .mount('#app')
