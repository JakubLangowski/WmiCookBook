<template>
    <div class="flex justify-center items-center" @click="toggleSwitch">
        <div class="w-12 h-6 flex items-center bg-gray-300 rounded-full p-1 duration-300 ease-in-out" :class="{ 'bg-gray-700': switchValue}">
            <div class="bg-white w-4 h-4 rounded-full shadow-md transform duration-300 ease-in-out" :class="{ 'translate-x-6': switchValue}"></div>
        </div>
        <h2 class="pl-2 font-semibold text-gray-700">{{ title }}</h2>
    </div>
</template>

<script>
export default {
    name: "Switch",
    props: {
        switchValue: Boolean,
        title: {
            type: String,
            required: true
        }
    },
    emits: ['update:switchValue'],
    methods: {
        toggleSwitch() {
            this.$emit('update:switchValue', !this.switchValue)
        }
    }
}
</script>

<style scoped>

</style>