import { createStore } from 'vuex'
import toast from "@/store/modules/toast";
import user from "@/store/modules/user";
import modal from "@/store/modules/modal";

export default createStore({
    strict: true,
    modules: {
        toast,
        user,
        modal
    },
})
