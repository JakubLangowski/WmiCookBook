<template>
    <button>{{ title }}</button>
</template>

<script>
export default {
    name: "ExampleComponent",
    props: {
        title: {
            type: String,
            // required: true,
            // default: () => "XD"
        }
    }
}
</script>

<style scoped>

</style>