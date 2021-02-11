<template>
    <div class="relative w-full md:w-52 lg:w-48 xl:w-56 md:mx-3 p-3 mb-2 md:p-6 bg-white rounded-lg shadow-lg cursor-pointer focus:outline-none"
         :class="{'border-2 border-gray-700' : modelValue === id}"
         tabindex="0" @click="updateActiveIndex" @keydown.space="updateActiveIndex">
        <div class="flex justify-between items-center mb-3">
            <span class="w-12 h-12" :class="icon"></span>
            <div v-if="modelValue === id">
                <svg class="w-6 h-6" width="200px" height="200px" viewBox="0 0 200 200">
                    <g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                        <g id="checkmark-outline" fill-rule="nonzero">
                            <path fill="#374151" d="M31.1442786,171.840796 C5.2779518,146.858262 -5.09578082,109.862896 4.01023318,75.0738981 C13.1162472,40.2848999 40.2848999,13.1162472 75.0738981,4.01023318 C109.862896,-5.09578082 146.858262,5.2779518 171.840796,31.1442786 C209.549474,70.1869539 209.010186,132.247241 170.628714,170.628714 C132.247241,209.010186 70.1869539,209.549474 31.1442786,171.840796 Z" id="Shape"></path>
                            <polygon id="Path" fill="#ffffff" points="66.6666667 89.4527363 89.5522388 112.437811 132.338308 69.6517413 146.268657 83.7810945 89.5522388 140.298507 52.7363184 103.482587 66.6666667 89.3532338"></polygon>
                        </g>
                    </g>
                </svg>
            </div>
        </div>
        <div>
            <h1 class="text-lg font-semibold tracking-wide text-blue-darker my-2 flex flex-row flex-nowrap items-center justify-space-between">
                {{ name }}
            </h1>
        </div>
    </div>
</template>

<script>
export default {
    name: "RadioButton",
    emits: ['update:modelValue'],
    props: {
        modelValue: {
            type: Number,
            default: 1,
        },
        id: {
            type: Number,
            required: true
        },
        name: {
            type: String,
            required: true
        },
        icon: {
            type: String,
            required: true
        },
    },
    methods: {
        updateActiveIndex() {
            this.$emit('update:modelValue', this.id)
        }
    }
}
</script>

<style scoped>

</style>