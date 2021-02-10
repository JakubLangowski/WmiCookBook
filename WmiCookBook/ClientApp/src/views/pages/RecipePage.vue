<template>
  <div class="flex flex-wrap">
    <div class="leftCol">
      <h1 class="text-3xl py-4 font-bold text-gray-500">{{ recipe.name }}</h1>
      <div class="grid grid-cols-2 mx-1 my-2">
                    <span class="text-center">
                        <span class="p-3 mr-1 lg:mr-3" :class="getDifficultyLevelIcon(difficulty)"></span>
                        {{ getDifficultyLevelText(recipe.difficulty) }}
                    </span>
        <span class="text-center">
                        <span class="p-3 mr-1 lg:mr-3 timer-outline-icon"></span>
                        {{ recipe.time }} min
                    </span>
      </div>
    </div>
    <div class="rightCol">
      <img :src="recipe.image" :alt="recipe.name"/>
      <IngredientsList :ingredients="recipe.ingredients"/>
    </div>


  </div>
</template>

<script>
import {recipe} from "@/fixtures/recipe";
import IngredientsList from "@/components/recipe/IngredientsList";

export default {
  name: "RecipePage",
  components: {IngredientsList},
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
  //min-width: 50%;
  flex: 1 1 50%;

  border: 1px solid blue;
}

.rightCol {
  //min-width: 640px;
  //max-width: 50%;
  flex: 1 1 50%;
  border: 1px solid red;

  img {
    min-width: 300px;
    width: 100%;
  }
}
</style>