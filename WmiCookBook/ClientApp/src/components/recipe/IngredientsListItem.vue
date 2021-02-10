<template>
    <tr>
        <td>
            <CheckboxInput :name="name" :text="name"/>
        </td>
        <td>
            {{ quantity }}
        </td>
    </tr>
</template>

<script>
import CheckboxInput from "@/components/shared/CheckboxInput";

export default {
    name: "IngredientsListItem",
    components: {CheckboxInput},
    props: {
        name: {
            type: String,
            required: true,
        },
        quantity: {
            type: String,
            required: true,
        },
    },
}
</script>

<style scoped>

</style>