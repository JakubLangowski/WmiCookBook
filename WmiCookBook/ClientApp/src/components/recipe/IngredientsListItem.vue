<template>
    <tr>
        <td>
            <span class="inline-flex items-center mt-3">
                <span class="ml-2 font-semibold">{{ name }}</span>
            </span>
        </td>
        <td>
            {{ quantity }}
        </td>
    </tr>
</template>

<script>

export default {
    name: "IngredientsListItem",
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