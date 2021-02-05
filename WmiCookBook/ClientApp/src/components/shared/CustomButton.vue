<template>
    <button :type="type" :disabled="disabled" @click="emitClick"
            class="btn" :class="'btn-' + btnStyle">
        {{ text }}
    </button>
</template>

<script>
export default {
    name: "CustomButton",
    props: {
        text: {
            type: String,
            default: () => "Button",
        },
        btnStyle: {
            type: String,
            default: () => "outline",
            validator: function (value) {
                return ['outline', 'normal'].indexOf(value) !== -1
            }
        },
        type: {
            type: String,
            default: () => "button",
        },
        disabled: {
            type: Boolean,
            default: () => false,
        },
    },
    methods: {
        emitClick: function () {
            this.$emit('click');
        }
    }
}
</script>

<style scoped>
.btn{
    @apply mt-1 inline-flex justify-center py-2 px-4 border
    text-sm leading-5 font-medium rounded transition duration-150 ease-in-out;
}

.btn-normal{
    @apply border-none shadow-lg
    text-white bg-gray-600 hover:bg-red-500
    focus:outline-none focus:bg-red-500 focus:border-red-600 focus:ring-red-600;
}

.btn-outline{
    @apply border-2 border-gray-500 shadow-lg
    text-gray-600 bg-white hover:text-white hover:bg-gray-600
    focus:outline-none focus:text-white focus:bg-gray-600 focus:ring-gray-600;
}   
</style>