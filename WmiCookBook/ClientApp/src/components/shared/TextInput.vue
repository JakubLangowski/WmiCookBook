<template>
    <div class="floating-input">
        <input
            :name="name"
            :id="name"
            :type="type"
            :value="inputValue"
            @input="handleChange"
            @blur="handleBlur"
            :class="(errorMessage) ? 'error' : ''"
        />
        <label :for="name">{{ label }}</label>
        <transition>
            <span v-show="errorMessage" class="puff-in-center swing-in-top-fwd">{{ errorMessage }}</span>
        </transition>
    </div>
</template>

<script>
import { useField } from "vee-validate";

export default {
    name: "TextInput",
    props: {
        type: {
            type: String,
            default: "text",
        },
        value: {
            type: String,
            default: "",
        },
        name: {
            type: String,
            required: true,
        },
        label: {
            type: String,
            required: true,
        },
    },
    setup(props) {
        const {
            value: inputValue,
            errorMessage,
            handleBlur,
            handleChange,
            meta,
        } = useField(props.name, undefined, {
            initialValue: props.value,
        });

        return {
            handleChange,
            handleBlur,
            errorMessage,
            inputValue,
            meta,
        };
    }
}
</script>

<style scoped>

</style>