<template>
    <h1 class="text-3xl text-center py-4 font-bold text-gray-500">{{ title }}</h1>
    <div v-if="isLoaded" class="grid grid-cols-12 gap-2">
        <RecipeCard v-for="({id, name, difficulty, time, image}, index) in recipes" :key="index"
                    :to="{ name: 'RecipePage', params: { id: id }}" :name="name" :time="time" :difficulty="difficulty" :image="image"/>
    </div>
    <div v-else class="grid grid-cols-12 gap-2">
        <RecipeCardSkeletonLoader v-for="index in 4" :key="index" />
    </div>
</template>

<script>
import RecipeCard from "@/components/home/RecipeCard";
import RecipeCardSkeletonLoader from "@/components/home/RecipeCardSkeletonLoader";
export default {
    name: "HomeCardRow",
    components: {
        RecipeCardSkeletonLoader,
        RecipeCard
    },
    props: {
        title: {
            type: String,
            required: true
        },
        recipes: {
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