import Vue from 'vue'
import axios from 'axios'
import store from '../store'

const instance = axios.create({
    baseURL:  '/api/',
});

instance.defaults.withCredentials = true;
instance.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

instance.interceptors.request.use(function(config) {
    const token = store.getters["user/token"]
    if ( token != null ) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, function(err) {
    return Promise.reject(err);
});
Vue.use({
    install: () => {
        Vue.prototype.$api = instance
        Vue.$api = instance
    }
})


export default instance;
