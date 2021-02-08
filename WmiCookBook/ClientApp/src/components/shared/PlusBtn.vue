<template>
    <span class="mt-1 text-gray-400 hover:text-red-500 focus:outline-none focus:text-red-500
                   focus:ring-indigo active:bg-red-700 transition duration-150 ease-in-out" @click.prevent="emitClick">
        <svg style="width:24px;height:24px" viewBox="0 0 24 24">
            <path fill="currentColor" d="M19,13H13V19H11V13H5V11H11V5H13V11H19V13Z" />
        </svg>
    </span>
</template>

<script>
export default {
    name: "PlusButton",
    inheritAttrs: false,
    methods: {
        emitClick: function () {
            this.$emit('click');
        }
    }
}
</script>

<style scoped>

</style>
