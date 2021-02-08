<template>
    <HomeCategoryRow v-if="featuredCategories.length > 0 || loadedFeaturedCategories" :is-loaded="loadedFeaturedCategories" title="Wyróżnione Kategorie" :categories="featuredCategories"/>
    <br>
    <HomeCardRow v-if="featuredRecipes.length > 0 || !loadedFeaturedRecipes" :is-loaded="loadedFeaturedRecipes" title="Wyróżnione Przepisy" :recipes="featuredRecipes"/>
    <br>
    <HomeCardRow v-if="newRecipes.length > 0 || !loadedNewRecipes" :is-loaded="loadedNewRecipes" title="Nowe Przepisy" :recipes="newRecipes"/>
</template>

<script>
import HomeCardRow from "@/components/home/HomeCardRow";
import HomeCategoryRow from "@/components/home/HomeCategoryRow";
import {mapGetters} from "vuex";

export default {
    name: "HomePage",
    components: {
        HomeCategoryRow,
        HomeCardRow
    },
    data: () => ({
        loadedFeaturedRecipes: false,
        featuredRecipes: [],
        loadedNewRecipes: false,
        newRecipes: [],
        loadedFeaturedCategories: false,
        featuredCategories: []
    }),
    mounted() {
        this.fetchFeaturedCategories()
            .then((response) => {
                setTimeout(() => {
                    this.featuredCategories = response.data;
                    this.loadedFeaturedCategories = true;
                }, 200)
            });
        this.fetchFeaturedRecipes()
            .then((response) => {
                setTimeout(() => {
                    this.featuredRecipes = response.data;
                    this.loadedFeaturedRecipes = true;
                }, 200)
            });
        this.fetchNewRecipes()
            .then((response) => {
                setTimeout(() => {
                    this.newRecipes = response.data;
                    this.loadedNewRecipes = true;
                }, 200)
            });
    },
    methods: {
        fetchFeaturedCategories: async function () {
            return this.$api.get('/category')
        },
        fetchFeaturedRecipes: async function () {
            return this.$api.get('/recipe/featured')
        },
        fetchNewRecipes: async function () {
            return this.$api.get('/recipe/new')
        }
    }
}
</script>

<style scoped>

</style>