
const state = {
    title: "",
    callback: null,
    show: false
};

const getters = {
    title: state => state.title,
    callback: state => state.callback,
    show: state => state.show,
};

const mutations = {
    showModal: (state, {title, callback}) => {
       state.title = title;
       state.callback = callback;
       state.show = true;
    },
    hideModal: (state) => {
        state.show = false;
    },
}

const actions = {
    showModal: ({commit}, {title, callback}) => {
        commit('showModal', {title, callback} )
    },
    hideModal: ({commit}) => {
        commit('hideModal')
    }
};

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions,
}
