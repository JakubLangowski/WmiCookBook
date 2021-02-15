<template>
    <div class="rounded bg-white shadow-xl border border-gray-200" :class="grid">
        <div class="col-span-12 sm:col-span-6 md:col-span-4 lg:col-span-3 h-full rounded bg-white shadow-xl border border-gray-200 animate-pulse">
            <div class="w-full h-48 bg-gray-200 flex justify-center items-center">
                <Spinner />
            </div>
            <h2 class="h-8 mx-4 my-2 bg-gray-200"></h2>
            <div class="grid grid-cols-2 gap-2 mx-4 my-2">
                <span class="w-full h-6 p-3 mr-1 lg:mr-3 bg-gray-200 animate-pulse"></span>
                <span class="w-full h-6 p-3 mr-1 lg:mr-3 bg-gray-200 animate-pulse"></span>
            </div>
        </div>
    </div>
</template>

<script>
import Spinner from "@/components/shared/Spinner";

export default {
    name: "RecipeCardSkeletonLoader",
    components: {
        Spinner
    },
    props: {
        grid: {
            type: String,
            default: () => "col-span-12 sm:col-span-6 lg:col-span-3"
        }
    }
}
</script>

<style scoped>

</style>