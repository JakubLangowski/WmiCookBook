import store from '@/store'
import router from "@/router";

const helpers = {
    handleErrors: function (error) {
        if (error.response) {
            if (error.response.status === 422){
                let errors = error.response.data.errors;
                let result = {}
                for (error of errors){
                    result[error.fieldName] = error.errors;
                }
                return result;
            } else if (error.response.status === 404) {
                router.push({name: 'NotFoundPage'});
            } else {
                store.dispatch('toast/errorToast', error.response.data.message)
            }
        } else if (error.request) {
            store.dispatch('toast/errorToast', "The request was made but no response was received")
        } else {
            store.dispatch('toast/errorToast', "There was an error during request")
        }
        return null;
    }
}


export default {
    install: (app) => {
        app.config.globalProperties.$helper = helpers;
    }
}
