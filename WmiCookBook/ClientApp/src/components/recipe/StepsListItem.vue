<template>
  <div>
    <div> Krok {{ index }}</div>
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