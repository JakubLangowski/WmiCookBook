<template>
    <div class="floating-input">
        <select v-model="selected" @change="changed" :required="display" @input="handleChange"
                :class="(errorMessage) ? 'error' : ''">
            <option value=""></option>
            <option v-for="(item, index) in data" :key="index" :value="item[objKey]">{{ item[objValueKey] }}</option>
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
        selectedCategory: {
            required: true,
        },
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
    data: () => ({
       selected: null, 
    }),
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
    },
    watch: {
        selectedCategory: function (value) {
            this.selected = value;
        }
    },
    methods: {
        changed: function () {
            this.$emit('changed', this.selected);
        }
    }
}
</script>

<style scoped>

</style>