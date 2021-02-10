<template>
  <h3 class="text-3xl font-bold">Lista zakupów: </h3>
  <table class="table-auto ingredientsList">
    <thead>
    <tr>
      <td><b>Składnik </b></td>
      <td><b>Ilość </b></td>
    </tr>
    </thead>
    <tbody>
    <IngredientsListItem
        v-for="({name, quantity},index) in ingredients"
        :name="name"
        :quantity="quantity"
        :key="index"
    />
    </tbody>
  </table>
</template>

<script>
import IngredientsListItem from "@/components/recipe/IngredientsListItem";

export default {
  name: "IngredientsList",
  components: {IngredientsListItem},
  props: {
    ingredients: {
      type: [],
      required: true,
    }
  },
}
</script>

<style lang="scss">
.ingredientsList {
  width: 100%;

  tr {
    td:nth-child(2) {
      text-align: right;
    }
  }
}
</style>