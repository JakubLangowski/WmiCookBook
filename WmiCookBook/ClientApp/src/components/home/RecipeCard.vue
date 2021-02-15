<template>
    <router-link :to="to" class="rounded bg-white shadow-2xl border border-gray-200" :class="grid">
        <div class="flex flex-col my-auto h-full items-center img-hover-zoom overflow-hidden">
            <div class="recipe-card-image-wrapper">
                <img class="recipe-card-img" :src="image" :alt="name">
            </div>
            <div class="w-full h-full flex flex-col justify-around z-20">
                <h2 class="text-lg text-center text-gray-700 font-semibold mx-2 my-2">{{ name }}</h2>
                <div class="grid grid-cols-2 mx-1 my-2">
                    <span class="text-center">
                        <span class="p-3 mr-1 lg:mr-3" :class="getDifficultyLevelIcon(difficulty)"></span>
                        {{ getDifficultyLevelText(difficulty) }}
                    </span>
                    <span class="text-center">
                        <span class="p-3 mr-1 lg:mr-3 timer-outline-icon"></span>
                        {{ time }} min
                    </span>
                </div>
            </div>
        </div>
    </router-link>
</template>

<script>
export default {
    name: "RecipeCard",
    props: {
        to: {
            type: Object,
            required: true
        },
        name: {
            type: String,
            required: true
        },
        difficulty: {
            type: Number,
            required: true
        },
        time: {
            type: Number,
            required: true
        },
        image: {
            type: String,
            required: true
        },
        grid: {
            type: String,
            default: () => "col-span-12 sm:col-span-6 lg:col-span-3"
        }
    },
    methods: {
        getDifficultyLevelIcon: function (level) {
            switch (level) {
                case 1:
                    return 'difficulty-level-1-icon'
                case 2:
                    return 'difficulty-level-2-icon'
                case 3:
                    return 'difficulty-level-3-icon'
            }
        },
        getDifficultyLevelText: function (level) {
            switch (level) {
                case 1:
                    return 'Łatwe'
                case 2:
                    return 'Średnie'
                case 3:
                    return 'Trudne'
            }
        },
    }
}
</script>

<style scoped lang="scss">
.recipe-card-image-wrapper {
    overflow: hidden;
    width: 100%;
    height: 100vh;
    max-height: 200px;
    border-radius: 3px;
    position: relative;
}
.recipe-card-img {
    width: 100%;
    height: auto;
    position: absolute;
}
</style>