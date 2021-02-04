import { createApp } from 'vue'
import App from "@/views/layout/App";
import router from './router'
import store from './store'
import api from "./plugins/api";
import helper from "./plugins/helper";
import '@/assets/css/main.scss'
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

const options = {
    position: "top-right",
    timeout: 3000
};

store.dispatch('user/init');

const app = createApp(App)
    .use(store)
    .use(router)
    .use(api)
    .use(helper)
    .use(Toast, options)
    .mount('#app')