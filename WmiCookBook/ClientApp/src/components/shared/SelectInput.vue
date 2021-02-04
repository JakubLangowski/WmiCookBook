<template>
    <div class="floating-input">
        <select @change="handleChange" :required="display" @input="handleChange"
                :class="(errorMessage) ? 'error' : ''">
            <option value=""></option>
            <option v-for="(item, index) in data"  :key="index" :value="item[objKey]">{{ item[objValueKey] }}</option>
        </select>
        <label>{{ (label) ? label : '' }}</label>
        <transition>
            <span v-if="errorMessage" class="swing-in-top-fwd puff-in-center">{{ errorMessage }}</span>
        </transition>
    </div>
</template>

<script>
import { useField } from "vee-validate";

export default {
    name: "SelectInput",
    props: {
        data: {
            type: Array,
            required: true,
        },
        display: {
            type: Boolean,
            default: () => true,
        },
        label: {
            type: String,
            default: () => "",
        },
        name: {
            type: String,
            default: () => "input",
        },
        objKey: {
            type: String,
            default: () => 'id',
        },
        objValueKey: {
            type: String,
            default: () => 'name',
        }
    },
    setup(props) {
        const {
            value: inputValue,
            errorMessage,
            handleChange,
            meta,
        } = useField(props.name, undefined, {
            initialValue: props.value,
        });
        return {
            handleChange,
            errorMessage,
            inputValue,
            meta,
        };
    }
}
</script>

<style scoped>

</style>