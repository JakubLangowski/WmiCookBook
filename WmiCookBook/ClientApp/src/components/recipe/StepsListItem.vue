<template>
  <div class="py-2 text-center lg:text-left ">
    <div class="text-xl font-bold pb-2"> Krok {{ index + 1 }}</div>
    <div> {{ description }}</div>
  </div>
</template>

<script>

export default {
  name: "StepsListItem",
  props: {
    description: {
      type: String,
      required: true,
    },
    index: {
      required: true,
    },
  },
}
</script>

<style scoped>

</style>