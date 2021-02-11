<template>
    <router-link :to="to" v-slot="{  isActive, navigate }" class="text-md font-semibold text-white">
        <div class="base" v-bind:class="{active:isActive, nonActive: !isActive}" @click="navigate">
            {{ title }}
        </div>
    </router-link>
</template>

<script>
export default {
    name: "AdminNavLink",
    props: {
        to: {
            type: Object,
            required: true
        },
        title: {
            type: String,
            required: true
        },
    },
}
</script>

<style scoped>
.base {
    @apply px-3 py-2 rounded-md text-sm font-medium;
}

.nonActive {
    @apply text-gray-300 hover:bg-gray-700 hover:text-white;
}

.active {
    @apply bg-gray-900 text-white;
}
</style>