import { createStore } from 'vuex'
import toast from "@/store/modules/toast";
import user from "@/store/modules/user";

export default createStore({
    strict: true,
    modules: {
        toast,
        user,
    },
})
