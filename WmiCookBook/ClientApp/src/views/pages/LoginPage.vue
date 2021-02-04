<template>
    <div class="min-h-screen flex flex flex-col items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
        <div class="mb-5">
            <img class="mx-auto h-12 w-auto" src="https://tailwindui.com/img/logos/workflow-mark-indigo-500.svg" alt="Workflow">
        </div>
        <div class="max-w-md w-full space-y-8">
            <Form @submit="onSubmit" :validation-schema="schema" class="grid grid-cols-1 grid-rows-2 gap-4 self-center">
                <TextInput name="email" type="email" label="Email" />
                <TextInput name="password" type="password" label="Hasło" />
                <CustomButton type="Submit" text="Zaloguj" btn-style="outline" />
            </Form>
        </div>
        <div class="max-w-md w-full">
            <div v-show="state.error !== ''" class="bg-red-100 border-t-4 border-red-500 rounded-b text-red-900 px-4 py-3 shadow-md" role="alert">
                <div class="flex">
                    <div class="py-1"><svg class="fill-current h-6 w-6 text-red-500 mr-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20"><path d="M2.93 17.07A10 10 0 1 1 17.07 2.93 10 10 0 0 1 2.93 17.07zm12.73-1.41A8 8 0 1 0 4.34 4.34a8 8 0 0 0 11.32 11.32zM9 11V9h2v6H9v-4zm0-6h2v2H9V5z"/></svg></div>
                    <div>
                        <p class="font-bold">{{ state.error }}</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

import { Form } from "vee-validate";
import * as Yup from "yup";
import TextInput from "@/components/shared/TextInput";
import { setLocale } from 'yup';
import CustomButton from "@/components/shared/CustomButton";
import {reactive} from "@vue/reactivity";
import {useStore} from "vuex";
import {useRouter} from "vue-router";

setLocale({
    mixed: {
        default: 'Podaj poprawne dane',
        required: 'Pole jest wymagane'
    },
    string: {
        default: 'Podaj poprawny adres email',
        email: 'Podaj poprawny adres email',
        min: 'Minimalna długość hasła wynosi ${min}',
    },
});

export default {
    name: "LoginPage",
    components: {
        CustomButton,
        TextInput,
        Form,
    },
    setup() {
        const store = useStore()
        const router = useRouter();
        let state = reactive({error: ""})
        // const app = getCurrentInstance()
        // const api = app.appContext.config.globalProperties.$api
    
        function onSubmit(values) {
            store.dispatch("user/login", values)
                .then(() => {
                    router.push({ name: 'AdminDashboardPage' });
                })
        }
        
        const schema = Yup.object().shape({
            email: Yup.string().email().required(),
            password: Yup.string()
                .min(8)
                .required()
                .matches(/[0-9]/g, 'Hasło musi posiadać przynajmniej jedną cyfrę')
                .matches(/(?=.*?[#?!@$%^&*-])/g, 'Hasło musi posiadać przynajmniej jeden znak specjalny')
                .matches(/[a-z]/g, 'Hasło musi posiadać przynajmniej jedną małą literę')
                .matches(/[A-Z]/g, 'Hasło musi posiadać przynajmniej jedną wielką literę')
        });
        return {
            onSubmit,
            schema,
            state
        };
    },
};
</script>

<style scoped>

</style>