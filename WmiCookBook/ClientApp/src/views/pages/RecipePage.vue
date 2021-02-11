<template>
    <div v-if="recipeLoaded" class="grid grid-cols-12 gap-5 pt-3">
        <div class="col-span-12 lg:col-span-6 bg-white shadow-lg rounded-lg relative">
            <CustomButton v-if="isAuthenticated && !recipe.isAccepted" 
                          class="absolute top-0 right-0" 
                          type="Submit" 
                          text="Akceptuj" 
                          btn-style="outline" 
                          @click="showAcceptModal"
            />
            <CustomButton v-if="isAuthenticated && !recipe.isAccepted"
                          class="absolute top-0 left-0"
                          type="Submit"
                          text="Usuń"
                          btn-style="outline"
                          @click="showDeleteModal"
            />
            <h1 class="mt-3 text-3xl text-center lg:text-5xl font-bold text-gray-700 md:p-3 lg:p-7">{{ recipe.name }}</h1>
            <h3 class="text-xl text-center lg:text-xl font-bold text-gray-700 md:p-3 lg:p-7 p-3">Kategoria: <span class="font-semibold">{{ recipe.category.name }}</span></h3>
            <div class="grid grid-cols-2 py-3 h-48 lg:h-52 items-center">
                <span class="text-center flex flex-col">
                    <span class="text-lg font-semibold text-gray-700 pb-2">Poziom trudności</span>
                    <span class="p-3 p-6 lg:p-8" :class="getDifficultyLevelIcon(recipe.difficulty)"></span>
                    <span class="text-lg font-semibold text-gray-700 pb-2">{{getDifficultyLevelText(recipe.difficulty) }}</span>
                </span>
                <span class="text-center flex flex-col">
                    <span class="text-lg font-semibold text-gray-700 pb-2">Czas wykonania</span>
                    <span class="p-3 p-6 lg:p-8 timer-outline-icon"></span>
                    <span class="text-lg font-semibold text-gray-700 pb-2">{{ recipe.time }} min</span>
                </span>
            </div>
        </div>
        <div class="col-span-12 lg:col-span-6 recipe-image-wrapper">
            <img class="recipe-img" :src="recipe.image" :alt="recipe.name"/>
        </div>
        <div class="col-span-12 lg:col-span-6 py-3 md:p-5 lg:p-8 bg-white shadow-2xl rounded-lg p-3">
            <IngredientsList :ingredients="recipe.ingredients"/>
        </div>
        <div class="col-span-12 lg:col-span-6 py-3 md:p-5 lg:p-8 bg-white shadow-2xl rounded-lg p-3">
            <StepsList :steps="recipe.steps"/>
        </div>
        <Modal />
    </div>
</template>

<script>
import IngredientsList from "@/components/recipe/IngredientsList";
import StepsList from "@/components/recipe/StepsList";
import CustomButton from "@/components/shared/CustomButton";
import {mapGetters} from "vuex";
import Modal from "@/components/shared/Modal";

export default {
    name: "RecipePage",
    components: {
        Modal,
        CustomButton,
        StepsList,
        IngredientsList
    },
    data: () => ({
        recipeLoaded: false,
        recipe: {},
    }),
    computed: {
        ...mapGetters('user', [
            'isAuthenticated',
        ])
    },
    mounted() {
        this.fetchRecipe()
            .then((response) => {
                this.recipe = response.data;
                this.recipeLoaded = true;
            })
            .catch(error => this.$helper.handleErrors(error));
    },
    methods: {
        fetchRecipe: async function () {
            return this.$api.get('/recipe/' + this.$route.params.id);
        },
        getDifficultyLevelIcon: function (level) {
            switch (level) {
                case 1:
                    return 'difficulty-level-1-icon'
                case 2:
                    return 'difficulty-level-2-icon'
                case 3:
                    return 'difficulty-level-3-icon'
            }
        },
        getDifficultyLevelText: function (level) {
            switch (level) {
                case 1:
                    return 'Łatwy'
                case 2:
                    return 'Średni'
                case 3:
                    return 'Trudny'
            }
        },
        showAcceptModal: function () {
            this.$store.dispatch('modal/showModal', {
                title: 'Zaakceptować przepis',
                callback: () => this.acceptRecipe(this.$route.params.id),
            });
        },
        acceptRecipe: function (id) {
            this.$api.patch(`/recipe/${id}/accept`)
                .then(() => {
                    this.$store.dispatch('toast/successToast', "Przepis zaakceptowano")
                    this.$router.push({ name: "AdminNotAcceptedRecipesPage"})
                })
                .catch(() => this.$store.dispatch('toast/errorToast', "Wystąpił błąd podczas akceptowania przepisu"))
        },
        showDeleteModal: function () {
            this.$store.dispatch('modal/showModal', {
                title: 'Czy na pewno chcesz usunąć przepis',
                callback: () => this.deleteRecipe(this.$route.params.id),
            });
        },
        deleteRecipe: function (id) {
            this.$api.delete(`/recipe/${id}`)
                .then(() => {
                    this.$store.dispatch('toast/successToast', "Usunięto przepis")
                    this.$router.push({ name: "AdminNotAcceptedRecipesPage"})
                })
                .catch(() => this.$store.dispatch('toast/errorToast', "Wystąpił błąd podczas usuwania przepisu"))
        }
    }
}
</script>

<style lang="scss" scoped>
    .recipe-image-wrapper {
        overflow: hidden;
        width: 100%;
        height: 100%;
        min-height: 375px;
        border-radius: 3px;
        position: relative;
    }
    .recipe-img {
        object-fit: cover;
        width: 100%;
        height: 100%;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%,-50%);
    }
</style>