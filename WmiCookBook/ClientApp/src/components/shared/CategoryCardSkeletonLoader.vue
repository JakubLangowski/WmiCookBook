<template>
    <div class="col-span-12 sm:col-span-6 lg:col-span-3 rounded bg-white shadow-xl border border-gray-200">
        <div class="flex flex-col my-auto h-full items-center img-hover-zoom overflow-hidden relative">
            <div class="w-full h-40 bg-gray-200 flex justify-center items-center">
                <Spinner />
            </div>
        </div>
    </div>
</template>

<script>
import Spinner from "@/components/shared/Spinner";
export default {
    name: "CategoryCardSkeletonLoader",
    components: {Spinner},
}
</script>

<style scoped>

</style>