<template>
    <div class="py-12 px-4 sm:px-6 lg:px-8">
        <h1 class="text-2xl font-bold text-center">Dodaj przepis</h1>
        <br>
        <Form @submit="onSubmit" :validation-schema="schema" class="grid grid-cols-12 gap-4">
            <div class="col-span-12 lg:col-span-6 grid grid-cols-2 gap-3">
                <TextInput class="col-span-2" name="name" type="text" label="Nazwa" />
                <TextInput class="col-span-1" name="time" type="number" label="Czas" />
                <SelectInput class="col-span-1" name="difficulty" label="Poziom trudności"  :data="selectData"/>
                <SelectInput class="col-span-2" name="categoryId" label="Kategoria" :data="categories"/>
            </div>
            <div class="col-span-12 lg:col-span-6">
                <TextInput class="col-span-2" name="image" type="text" label="Zdjęcie" />
            </div>
            <div class="col-span-12 lg:col-span-6 flex flex-col space-y-1">
                <h3 class="text-xl font-bold text-center">Składniki</h3>
                <br>
                <div class="grid grid-cols-12 gap-2" v-for="(ingredient, idx) in ingredients" :key="idx">
                    <TextInput class="col-span-7" :name="`ingredients[${idx}].name`" type="text" label="Składnik" />
                    <TextInput class="col-span-4" :name="`ingredients[${idx}].quantity`" type="text" label="Ilość" />
                    <div class="col-span-1 flex flex-col justify-around">
                        <TrashBtn v-if="ingredients.length > 1" class="pb-4" @click="removeIngredient(ingredient, idx)"></TrashBtn>
                        <PlusButton v-if="idx + 1 === ingredients.length && ingredients.length < 10" @click="addIngredient"></PlusButton>
                    </div>
                </div>
            </div>
            <div class="col-span-12 lg:col-span-6 flex flex-col space-y-1">
                <h3 class="text-xl font-bold text-center">Kroki</h3>
                <br>
                <div v-for="(step, idx) in steps" :key="idx" class="grid grid-cols-12">
                    <TextareaInput class="col-span-11" :name="`steps[${idx}].description`" label="Opis" ></TextareaInput>
                    <div class="col-span-1 flex flex-col justify-around">
                        <TrashBtn v-if="steps.length > 1" @click="removeStep(step, idx)"></TrashBtn>
                        <br>
                        <PlusButton v-if="idx + 1 === steps.length && steps.length < 20" @click="addStep"></PlusButton>
                    </div>
                </div>
            </div>
            <div class="col-span-12 lg:col-span-6">
                <br>
                <CustomButton type="Submit" text="Dodaj Przepis" btn-style="outline" />
            </div>
        </Form>
    </div>
</template>

<script>
import { Form } from "vee-validate";
import * as Yup from "yup";
import TextInput from "@/components/shared/TextInput";
import { setLocale } from 'yup';
import CustomButton from "@/components/shared/CustomButton";
import {reactive} from "@vue/reactivity";
import SelectInput from "@/components/shared/SelectInput";
import TextareaInput from "@/components/shared/TextareaInput.";
import TrashBtn from "@/components/shared/TrashBtn";
import PlusButton from "@/components/shared/PlusBtn";
import {useRouter} from "vue-router";
import {useStore} from "vuex";
import { getCurrentInstance } from 'vue'
import {onMounted} from "@vue/runtime-core";

setLocale({
    mixed: {
        default: 'Podaj poprawne dane',
        required: 'Pole jest wymagane'
    },
    string: {
        default: 'Podaj poprawne dane',
        min: 'Minimalna długość wynosi ${min}',
        max: 'Maksymalna długość wynosi ${max}',
    },
    array: {
        max: 'Maksymalna długość wynosi ${max}',
    },
});

export default {
    name: "AddRecipePage",
    components: {
        PlusButton,
        TrashBtn,
        TextareaInput,
        SelectInput,
        CustomButton,
        TextInput,
        Form,
    },
    data: () => ({
      selectData: [
          {id: 1, name: "Łatwe"},
          {id: 2, name: "Srednie"},
          {id: 3, name: "Trudne"},
      ]  
    }),
    setup() {
        let state = reactive({error: ""})
        const router = useRouter();
        const store = useStore()
        const app = getCurrentInstance()
        const api = app.appContext.config.globalProperties.$api;
        
        let categories = reactive([]);
        
        function fetchCategories() {
            api.get('/category')
                .then((response) => {
                    categories.push(...response.data)
                })
                .catch(() => {
                    store.dispatch('toast/errorToast', "Wystąpił błąd podczas wczytywania ketegorii")
                })
        }
        
        function onSubmit(values) {
            api.post('/recipe', values)
                .then(() => {
                    store.dispatch('toast/successToast', "Dodano nowy przepis. Przepis oczekuje na zaakceptowanie")
                    router.push({ name: 'RecipesPage'});
                })
                .catch(() => {
                    store.dispatch('toast/errorToast', "Wystąpił błąd podczas dodawania przepisu")
                })
        }

        const schema = Yup.object().shape({
            ingredients: Yup.array().min(1).max(20).of(
                Yup.object().shape({
                    name: Yup.string().min(1).max(100).required(),
                    quantity: Yup.string().max(100).required(),
                })
            ),
            steps: Yup.array().max(10).of(
                Yup.object().shape({
                    description: Yup.string().max(1000).required(),
                })
            ),
            name: Yup.string().max(100).required(),
            image: Yup.string().url().max(500).required(),
            time: Yup.number().integer().transform(value => (isNaN(value) ? undefined : value)).required().min(1).max(1000),
            difficulty: Yup.number().integer().transform(value => (isNaN(value) ? undefined : value)).required().min(1).max(3),
            categoryId: Yup.number().integer().transform(value => (isNaN(value) ? undefined : value)).required(),
        });

        onMounted(() => {
            fetchCategories();
        });
        
        return {
            onSubmit,
            schema,
            state,
            categories,
            ingredients: reactive([
                {
                    name: "",
                    quantity: ""
                }
            ]),
            steps: reactive([
                {
                    description: "",
                }
            ])
        };
    },
    methods: {
        addIngredient() {
            this.ingredients.push(
                {
                    name: "",
                    quantity: ""
                }
            )
        },
        addStep() {
            this.steps.push(
                {
                    description: "",
                }
            );
        },
        removeIngredient(item, index) {
            this.ingredients.splice(index, 1);
        },
        removeStep(item, index) {
            this.steps.splice(index, 1);
        },
    }
}
</script>

<style scoped>

</style>