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
                router.push({name: 'notFound'});
            } else {
                store.dispatch('snackbar/createErrorSnackbar', error.response.data.message)
            }
        } else if (error.request) {
            store.dispatch('snackbar/createErrorSnackbar', "The request was made but no response was received")
        } else {
            store.dispatch('snackbar/createSnackbar', "There was an error during request")
        }
        return null;
    }
}


export default {
    install: (app) => {
        app.config.globalProperties.$helper = helpers;
    }
}
