<template>
  <IngredientsLisItem
      v-for="({name, quantity},index) in ingredients"
      :name="name"
      :quantity="quantity"
      :key="index"
  />
</template>

<script>
import IngredientsLisItem from "@/components/recipe/IngredientsListItem";

export default {
  name: "IngredientsList",
  components: {IngredientsLisItem},
  props: {
    ingredients: {
      type: [],
      required: true,
    }
  },
}
</script>

<style scoped>

</style>