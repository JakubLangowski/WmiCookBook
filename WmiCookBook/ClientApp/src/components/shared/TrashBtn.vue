<template>
    <span class="mt-1 text-gray-400 hover:text-red-500 focus:outline-none focus:text-red-500
                   focus:ring-indigo active:bg-red-700 transition duration-150 ease-in-out" @click.prevent="emitClick">
        <svg style="width:24px;height:24px" class="mx-auto" viewBox="0 0 24 24">
            <path fill="currentColor" d="M9,3V4H4V6H5V19A2,2 0 0,0 7,21H17A2,2 0 0,0 19,19V6H20V4H15V3H9M7,6H17V19H7V6M9,8V17H11V8H9M13,8V17H15V8H13Z" />
        </svg>
    </span>
</template>

<script>
export default {
    name: "TrashBtn",
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
