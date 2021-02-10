<template>
    <div v-if="recipeLoaded" class="grid grid-cols-12">
        <div class="col-span-12 lg:col-span-6">
            <h1 class="text-3xl text-center lg:text-5xl font-bold text-gray-700 md:p-3 lg:p-7 ">{{ recipe.name }}</h1>
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
        <div class="col-span-12 lg:col-span-6">
            <img class="object-contain h-96 w-full" :src="recipe.image" :alt="recipe.name"/>
        </div>
        <div class="col-span-12 lg:col-span-6 py-3 md:p-5 lg:p-8">
            <IngredientsList :ingredients="recipe.ingredients"/>
        </div>
        <div class="col-span-12 lg:col-span-6 py-3 md:p-5 lg:p-8">
            <StepsList :steps="recipe.steps"/>
        </div>
    </div>
</template>

<script>
import IngredientsList from "@/components/recipe/IngredientsList";
import StepsList from "@/components/recipe/StepsList";

export default {
    name: "RecipePage",
    components: {
        StepsList,
        IngredientsList
    },
    data: () => ({
        recipeLoaded: false,
        recipe: {},
    }),
    mounted() {
        this.fetchRecipe()
            .then((response) => {
                this.recipe = response.data;
                this.recipeLoaded = true;
            });
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
    }
}
</script>

<style lang="scss">

.leftCol {
    flex: 1 1 50%;
}

.rightCol {
    flex: 1 1 50%;

    //img {
    //  border-radius: 5px;
    //  min-width: 500px;
    //  @media (max-width: 500px) {
    //    min-width: 300px;
    //  }
    //  width: 100%;
    //}
}
</style>