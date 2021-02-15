import axios from 'axios'
import store from '../store'
import router from '../router'

const instance = axios.create({
    baseURL:  '/api/',
});

instance.defaults.withCredentials = true;
instance.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

instance.interceptors.request.use(async function(config) {
    const token = store.getters["user/token"]
    
    if ( token != null ) {
        const tokenExpired = store.getters["user/tokenExpired"]
        if (Math.floor(Date.now() / 1000) >= (tokenExpired - 120)) {
            await store.dispatch('user/refreshToken')
                .then((result) => {
                    config.headers.Authorization = `Bearer ${result}`;
                    return config;
                })
                .catch(() => {
                    store.commit('user/removeAuthData');
                    router.push({ name: "LoginPage"})
                })
        } else {
            config.headers.Authorization = `Bearer ${token}`;
        }
    }
    
    return config;
}, function(err) {
    return Promise.reject(err);
});

export default {
    install: (app) => {
        app.config.globalProperties.$api = instance;
    }
}
