import jwt_decode from "jwt-decode";
import axios from 'axios'

const state = {
    id: null,
    email: null,
    role: null,
    token: localStorage.getItem('token') || null,
    refreshToken: localStorage.getItem('refreshToken') || null,
    tokenExp: 0,
};

const getters = {
    userEmail: state => state.email,
    isAuthenticated: state => (state.token != null && state.refreshToken != null),
    token: state => state.token,
    id: state => parseInt(state.id),
};

const mutations = {
    setInitData: (state) => {
        let token = localStorage.getItem('token') || null;
        if (token) {
            let tokenData = jwt_decode(token);
            if (tokenData){
                state.tokenExp = tokenData.exp;
                state.email = tokenData.sub;
                state.id = parseInt(tokenData.id);
            }
        }
    },
    setAuthData: (state, payload) => {
        localStorage.setItem('token', payload.token);
        localStorage.setItem('refreshToken', payload.refreshToken);
        state.token = payload.token;
        state.refreshToken = payload.refreshToken;
        let tokenData = jwt_decode(payload.token);
        if (tokenData){
            state.tokenExp = tokenData.exp;
            state.role = tokenData.role ? tokenData.role : null;
            state.email = tokenData.sub;
            state.id = parseInt(tokenData.id);
        }
    },
    removeAuthData: (state) => {
        localStorage.removeItem('token');
        localStorage.removeItem('refreshToken');
        state.token = null
        state.refreshToken = null
        state.id = null
        state.email = ""
        state.tokenExp = 0
    }
};

const actions = {
    init({commit}) {
        commit('setInitData');
    },
    login({commit, dispatch}, formData) {
        return new Promise(((resolve, reject) => {
            axios.post('/api/auth/login', formData)
                .then(result => {
                    commit('setAuthData', result.data);
                    dispatch('toast/successToast', "Zalogowano", {root:true})
                    resolve(result)
                })
                .catch(error => {
                    dispatch('toast/errorToast', "Wystąpił błąd podczas logowania", {root:true})
                    reject(error)
                })
        }))
    },
    logout({commit, state, dispatch}){
        return new Promise(((resolve, reject) => {
            commit('removeAuthData')
            if (state.token == null){
                dispatch('toast/successToast', "Wylogowano", {root:true})
                resolve()
            }
            else {
                dispatch('toast/errorToast', "Wystąpił błąd podczas próby wylogowania", {root:true})
                reject()
            }
        }))
    }
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
