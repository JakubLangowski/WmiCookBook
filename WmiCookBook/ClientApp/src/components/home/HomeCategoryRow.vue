<template>
    <h1 class="text-3xl text-center py-4 font-bold text-gray-500">{{ title }}</h1>
    <div v-if="isLoaded" class="grid grid-cols-12 gap-2">
        <CategoryCard v-for="({id, name, image}, index) in categories" :key="index"
                    :to="{ name: 'RecipesPage', params: { id: id }}" :name="name" :image="image"/>
    </div>
    <div v-else class="grid grid-cols-12 gap-2">
        <CategoryCardSkeletonLoader v-for="index in 4" :key="index" />
    </div>
</template>

<script>
import CategoryCard from "@/components/shared/CategoryCard";
import CategoryCardSkeletonLoader from "@/components/shared/CategoryCardSkeletonLoader";

export default {
    name: "HomeCategoryRow",
    components: {
        CategoryCardSkeletonLoader,
        CategoryCard,
    },
    props: {
        title: {
            type: String,
            required: true
        },
        categories: {
            type: Array,
            required: true
        },
        isLoaded: {
            type: Boolean,
            required: true
        }
    }
}
</script>

<style scoped>

</style>