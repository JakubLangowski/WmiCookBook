<template>
    <AdminRecipeTable endpoint="/recipe/not-accepted" title="Przepisy do akceptacji"/>
</template>

<script>

import AdminRecipeTable from "@/components/admin/recipes/AdminRecipeTable";
export default {
    name: "AdminRecipesPage",
    components: {AdminRecipeTable},
}
</script>

<style scoped>

</style>