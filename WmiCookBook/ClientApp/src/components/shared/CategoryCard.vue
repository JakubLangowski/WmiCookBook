<template>
    <router-link :to="to" class="col-span-12 sm:col-span-6 lg:col-span-3 rounded bg-white shadow-xl border border-gray-200">
        <div class="flex flex-col my-auto h-full items-center img-hover-zoom overflow-hidden relative">
            <img class="w-full h-auto" :src="image" :alt="name">
            <div class="w-full h-full flex flex-col justify-around z-20 absolute bg-black bg-opacity-30 img-hover-text">
                <h2 class="text-2xl uppercase shadow-2xl text-center text-white font-semibold mx-2 my-2">{{ name }}</h2>
            </div>
        </div>
    </router-link>
</template>

<script>
export default {
    name: "CategoryCard",
    props: {
        to: {
            type: String,
            required: true
        },
        name: {
            type: String,
            required: true
        },
        image: {
            type: String,
            required: true
        },
    },
}
</script>

<style scoped>

</style>