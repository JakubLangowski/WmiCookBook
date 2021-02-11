<template>
    <div class="py-12 px-4 sm:px-6 lg:px-8">
        <Form @submit="onSubmit" :validation-schema="schema" class="grid grid-cols-12 gap-4">
            <div class="col-span-12 lg:col-span-6 grid grid-cols-2 gap-3 bg-white shadow-2xl p-3 rounded-lg">
                <div class="col-span-2 flex justify-between items-center w-full">
                    <h1 class="col-span-2 text-2xl font-bold text-center text-gray-700">Dodaj przepis</h1>
                    <CustomButton type="Submit" text="Dodaj" btn-style="outline"/>
                </div>
                <TextInput class="col-span-2" name="name" type="text" label="Nazwa"/>
                <TextInput class="col-span-1" name="time" type="number" label="Czas"/>
                <SelectInput class="col-span-1" name="categoryId" label="Kategoria" :data="categories"/>
                <div class="col-span-2">
                    <div class="flex justify-start align-items-center flex-col md:flex-row">
                        <RadioButton
                            v-for="(option, index) in selectData" :key="index"
                            :id="option.id"
                            :name="option.name"
                            :icon="`difficulty-level-${option.id}-icon`"
                            v-model="selectedDifficulty"
                        ></RadioButton>
                    </div>
                </div>
            </div>
            <div class="col-span-12 lg:col-span-6 recipe-image-wrapper" @click="selectImage">
                <div class="col-span-12 lg:col-span-6 recipe-image-wrapper" :class="(imageValid) ? 'border-gray-700' : 'border-red-500'" >
                    <img class="recipe-img" :class="(!file) ? 'scale-down-image' : ''" :src="imageBase64" alt="uploadContainer"/>
                </div>
                <input class="hidden" @change="previewFiles" ref="fileInput" type="file" name="image">
            </div>
            <div class="col-span-12 lg:col-span-6 flex flex-col space-y-1 bg-white shadow-2xl p-3 rounded-lg">
                <h3 class="text-2xl font-bold text-center text-gray-700">Składniki</h3>
                <br>
                <div class="grid grid-cols-12 gap-2" v-for="(ingredient, idx) in ingredients" :key="idx">
                    <TextInput class="col-span-7" :name="`ingredients[${idx}].name`" type="text" label="Składnik"/>
                    <TextInput class="col-span-4" :name="`ingredients[${idx}].quantity`" type="text" label="Ilość"/>
                    <div class="col-span-1 flex flex-col justify-between">
                        <TrashBtn v-if="ingredients.length > 1" class="p-0 m-0"
                                  @click="removeIngredient(ingredient, idx)"></TrashBtn>
                        <PlusButton v-if="idx + 1 === ingredients.length && ingredients.length < 10"
                                    @click="addIngredient"></PlusButton>
                    </div>
                </div>
            </div>
            <div class="col-span-12 lg:col-span-6 flex flex-col space-y-1 bg-white shadow-2xl p-3 rounded-lg">
                <h3 class="text-2xl font-bold text-center text-gray-700">Kroki</h3>
                <br>
                <div v-for="(step, idx) in steps" :key="idx" class="grid grid-cols-12">
                    <TextareaInput class="col-span-11" :name="`steps[${idx}].description`" label="Opis"></TextareaInput>
                    <div class="col-span-1 flex flex-col justify-around">
                        <TrashBtn v-if="steps.length > 1" @click="removeStep(step, idx)"></TrashBtn>
                        <br>
                        <PlusButton v-if="idx + 1 === steps.length && steps.length < 20" @click="addStep"></PlusButton>
                    </div>
                </div>
            </div>
        </Form>
    </div>
</template>

<script>
import {Form} from "vee-validate";
import * as Yup from "yup";
import TextInput from "@/components/shared/TextInput";
import {setLocale} from 'yup';
import CustomButton from "@/components/shared/CustomButton";
import {reactive, ref} from "@vue/reactivity";
import SelectInput from "@/components/shared/SelectInput";
import TextareaInput from "@/components/shared/TextareaInput.";
import TrashBtn from "@/components/shared/TrashBtn";
import PlusButton from "@/components/shared/PlusBtn";
import {useRouter} from "vue-router";
import {useStore} from "vuex";
import {getCurrentInstance} from 'vue'
import {onMounted} from "@vue/runtime-core";
import RadioButton from "@/components/shared/RadioButton";

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
        RadioButton,
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
            {id: 1, name: "Łatwy"},
            {id: 2, name: "Sredni"},
            {id: 3, name: "Trudny"},
        ],
        imageBase64: "data:image/png;base64,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"
    }),
    setup() {
        let state = reactive({error: ""})
        const router = useRouter();
        const store = useStore()
        const app = getCurrentInstance()
        const api = app.appContext.config.globalProperties.$api;

        const selectedDifficulty = ref(1);
        const imageValid = ref(true);
        const image = ref("");
        const file = ref(null);
        let categories = reactive([]);

        function fetchCategories() {
            api.get('/category')
                .then((response) => {
                    categories.push(...response.data)
                })
                .catch((error) => {
                    this.$helper.handleErrors(error);
                    store.dispatch('toast/errorToast', "Wystąpił błąd podczas wczytywania ketegorii")
                })
        }

        function validateImage() {
            const pattern = /^data:image\/(?:jpeg|png)(?:;charset=utf-8)?;base64,(?:[A-Za-z0-9]|[+/])+={0,2}/;
            imageValid.value = pattern.test(image.value);
            return imageValid.value;
        }
        
        function onSubmit(values) {
            
            if (!validateImage()) return;
            
            const formData = new FormData();
            formData.append('Name', values.name);
            formData.append('Image', file.value);
            formData.append('Difficulty', selectedDifficulty.value.toString());
            formData.append('Time', values.time);
            formData.append('CategoryId', values.categoryId);
            
            for (let i = 0; i < values.steps.length; i++) {
                formData.append(`Steps[${i}][Description]`, values.steps[i].description);
            }
            for (let i = 0; i < values.ingredients.length; i++) {
                formData.append(`Ingredients[${i}][Name]`, values.ingredients[i].name);
                formData.append(`Ingredients[${i}][Quantity]`, values.ingredients[i].quantity);
            }
            
            api.post('/recipe', formData, {
                headers: {'Content-Type': 'multipart/form-data' }
            })
                .then(() => {
                    store.dispatch('toast/successToast', "Dodano nowy przepis. Przepis oczekuje na zaakceptowanie")
                    router.push({name: 'RecipesPage'});
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
            image,
            file,
            imageValid,
            selectedDifficulty,
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
        selectImage() {
            this.$refs.fileInput.click()
        },
        previewFiles: async function (event) {
            const files = event.target.files;
            if (files.length > 0) {
                this.file = files[0];
                let reader = new FileReader();
                reader.onload = (e) => {
                    this.imageBase64 = e.target.result;
                    this.image = e.target.result;
                }
                reader.readAsDataURL(files[0]);
            }
        },
    }
}
</script>

<style scoped>

.recipe-image-wrapper {
    overflow: hidden;
    width: 100%;
    height: 100%;
    min-height: 375px;
    border-radius: 3px;
    position: relative;
    @apply border-2;
}
.recipe-img {
    width: 100%;
    height: 100%;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%,-50%);
    object-fit: cover;
}

</style>