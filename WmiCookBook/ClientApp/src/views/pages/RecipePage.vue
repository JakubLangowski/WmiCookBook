<template>
  <div class="flex flex-wrap">
    <div class="leftCol">
      <h1 class="text-3xl font-bold text-gray-500 md:p-5 lg:p-8">{{ recipe.name }}</h1>
      <div class="grid grid-cols-2 py-3 md:p-5 lg:p-8">
                    <span class="text-center">
                        <span class="p-3 mr-1 lg:mr-3" :class="getDifficultyLevelIcon(difficulty)"></span>
                        {{ getDifficultyLevelText(recipe.difficulty) }}
                    </span>
        <span class="text-center">
                        <span class="p-3 mr-1 lg:mr-3 timer-outline-icon"></span>
                        {{ recipe.time }} min
                    </span>
      </div>
      <div class="py-3 md:p-5 lg:p-8">
        <StepsList :steps="recipe.steps"/>
      </div>

    </div>
    <div class="rightCol">
      <img :src="recipe.image" :alt="recipe.name"/>
      <div class="py-3 md:p-5 lg:p-8">
        <IngredientsList :ingredients="recipe.ingredients"/>
      </div>
    </div>


  </div>
</template>

<script>
import {recipe} from "@/fixtures/recipe";
import IngredientsList from "@/components/recipe/IngredientsList";
import StepsList from "@/components/recipe/StepsList";

export default {
  name: "RecipePage",
  components: {StepsList, IngredientsList},
  data: () => ({
    // recipe:undefined
    recipe: recipe,
  }),
  mounted() {

    // this.fetchRecipe()
    //     .then((response) => {
    //       this.recipe = response.data;
    //     });
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
          return 'Łatwe'
        case 2:
          return 'Średnie'
        case 3:
          return 'Trudne'
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

  img {
    min-width: 500px;
    @media (max-width: 500px) {
      min-width: 300px;
      border: 5px solid green;
    }
    width: 100%;
  }
}
</style>