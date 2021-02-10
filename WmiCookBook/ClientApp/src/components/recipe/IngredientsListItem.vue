<template>
  <div>
    <CheckboxInput  :name="name"  :text="name"/>
    {{ quantity }}
  </div>
</template>

<script>
import CheckboxInput from "@/components/shared/CheckboxInput";
export default {
  name: "IngredientsLisItem",
  components: {CheckboxInput},
  props: {
    name: {
      type: String,
      required: true,
    },
    quantity: {
      type: String,
      required: true,
    },
  },
}
</script>

<style scoped>

</style>