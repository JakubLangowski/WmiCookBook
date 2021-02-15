<template>
    <div v-if="isLoaded" class="grid grid-cols-12 gap-2">
        <RecipeCard v-for="({id, name, difficulty, time, image}, index) in recipes" :key="index"
                    grid="col-span-12 sm:col-span-6 lg:col-span-4 xl:col-span-3"
                    :to="{ name: 'RecipePage', params: { id: id }}" :name="name" :time="time" :difficulty="difficulty" :image="image"/>
    </div>
    <div v-else class="grid grid-cols-12 gap-2">
        <RecipeCardSkeletonLoader v-for="index in skeletonLoaderLength" :key="index" grid="col-span-12 sm:col-span-6 lg:col-span-4 xl:col-span-3"/>
    </div>
</template>

<script>
import RecipeCardSkeletonLoader from "@/components/home/RecipeCardSkeletonLoader";
import RecipeCard from "@/components/home/RecipeCard";

export default {
    name: "RecipesList",
    components: {
        RecipeCardSkeletonLoader,
        RecipeCard
    },
    props: {
        recipes: {
            type: Array,
            required: true
        },
        isLoaded: {
            type: Boolean,
            required: true
        },
        skeletonLoaderLength: {
            type: Number,
            default: () => 12
        }
    }
}
</script>

<style scoped>

</style>