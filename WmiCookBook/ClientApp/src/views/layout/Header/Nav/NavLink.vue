<template>
  <router-link
      :to="to"
      v-slot="{  isActive, navigate}"
  >
    <div
        v-show="!isMobile"
        class="base"
        v-bind:class="{active:isActive, nonActive: !isActive}"
        @click="navigate"
    >
      {{ title }}
    </div>

    <div
        v-show="isMobile"
        class="baseMobile"
        v-bind:class="{activeMobile:isActive, nonActiveMobile: !isActive}"
        @click="navigate"
    >
      {{ title }}
    </div>

  </router-link>
</template>

<script>
export default {
  name: "NavLink",
  props: {
    to: String,
    title: String,
    isMobile: {
      type: Boolean,
      default: false
    }
  },
}
</script>

<style scoped lang="scss">
.base {
  @apply px-3 py-2 rounded-md text-sm font-medium;
}

.nonActive {
  @apply text-gray-300 hover:bg-gray-700 hover:text-white;
}

.active {
  @apply bg-gray-900 text-white;
}

.baseMobile {
  @apply block px-3 py-2 rounded-md text-base font-medium;
}

.activeMobile {
  @apply bg-gray-900 text-white;
}

.nonActiveMobile {
  @apply text-gray-300 hover:bg-gray-700 hover:text-white;
}

</style>