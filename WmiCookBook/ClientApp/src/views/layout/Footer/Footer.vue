<template>
  <footer class="bg-gray-800">
    <div class="max-w-7xl mx-auto px-2 sm:px-6 lg:px-8">
      <div class="relative flex items-center h-16 space-x-4">
        <p class="text-white">
          <router-link to="/regulations">
            Regulamin
          </router-link>
        </p>
        <p class="text-white"> | </p>
        <p class="text-white">
          <router-link to="/contact">
            Kontakt
          </router-link>
        </p>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: "Footer"
}
</script>

<style scoped>

</style>