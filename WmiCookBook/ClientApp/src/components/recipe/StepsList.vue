<template>
    <h3 class="text-3xl font-bold"> Przygotowanie: </h3>
    <StepsListItem
        v-for="({description},index) in steps"
        :description="description"
        :index="index"
        :key="index"
    />
</template>

<script>

import StepsListItem from "@/components/recipe/StepsListItem";

export default {
  name: "StepsList",
  components: {StepsListItem},
  props: {
    steps: {
      type: Array,
      required: true,
    }
  },
}
</script>

<style lang="scss">

</style>