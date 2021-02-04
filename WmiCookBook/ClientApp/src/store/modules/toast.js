import {useToast} from "vue-toastification";
const toast = useToast()

const actions = {
    successToast: ({state, commit}, payload) => {
        toast.success(payload)
    },
    errorToast: ({state, commit}, payload) => {
        toast.error(payload)
    },
};

export default {
    namespaced: true,
    actions,
}
