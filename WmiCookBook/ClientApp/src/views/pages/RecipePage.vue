<template>
    <div>
        Strona z pojedyńczym przepisem
        <pre>
            {{ recipe }}
        </pre>
    </div>
</template>

<script>
export default {
    name: "RecipePage",
    data: () => ({
        recipe: null,
    }),
    mounted() {
        this.fetchRecipe()
            .then((response) => {
                this.recipe = response.data;
            });
    },
    methods: {
        fetchRecipe: async function () {
            return this.$api.get('/recipe/' + this.$route.params.id);
        },
    }
}
</script>

<style scoped>

</style>