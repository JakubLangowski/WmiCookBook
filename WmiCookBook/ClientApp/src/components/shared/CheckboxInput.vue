<template>
    <label class="inline-flex items-center mt-3">
        <input type="checkbox" :name="name" :id="name" @change="handleChange(value)" class="form-checkbox outline-none focus:outline-none h-5 w-5 text-red-600">
        <span class="ml-2 font-semibold" :class="(errorMessage) ? 'text-red-600' : 'text-gray-700'">{{ text }}</span>
    </label>
</template>

<script>
import {useField} from "vee-validate";

export default {
    name: "CheckboxInput",
    props: {
        name: {
            type: String,
            required: true,
        },
        text: {
            type: String,
            required: true,
        },
        modelValue: {
            type: null,
        },
        value: {
            type: Boolean,
        },
    },
    setup(props) {
        const { handleChange, errorMessage } = useField(props.name, undefined, {
            type: "checkbox",
            valueProp: props.value,
        });
        return {
            errorMessage,
            handleChange,
        };
    },
}
</script>

<style scoped>

</style>