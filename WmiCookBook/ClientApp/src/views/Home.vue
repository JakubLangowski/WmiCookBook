<template>
    <div class="mx-0 grid md:mx-auto p-3 md:container bg-gray-100 bg-opacity-50">
        <HomeCardRow :is-loaded="loadedFeaturedRecipes" title="Wyróżnione Przepisy" :recipes="featuredRecipes"/>
        <br>
        <HomeCardRow :is-loaded="loadedPopularRecipes" title="Popularne Przepisy" :recipes="popularRecipes"/>
        <br>
        <HomeCardRow :is-loaded="loadedNewRecipes" title="Nowe Przepisy" :recipes="newRecipes"/>
    </div>
</template>

<script>
import HomeCardRow from "@/components/home/HomeCardRow";

export default {
    name: "Home",
    components: {
        HomeCardRow
    },
    data: () => ({
        loadedFeaturedRecipes: false,
        featuredRecipes: [],
        loadedPopularRecipes: false,
        popularRecipes: [],
        loadedNewRecipes: false,
        newRecipes: []
    }),
    mounted() {
        this.fetchFeaturedRecipes()
            .then((response) => {
                setTimeout(() => {
                    this.featuredRecipes = response;
                    this.loadedFeaturedRecipes = true;
                }, 700)
            });
        this.fetchPopularRecipes()
            .then((response) => {
                setTimeout(() => {
                    this.popularRecipes = response;
                    this.loadedPopularRecipes = true;
                }, 1000)
            });
        this.fetchNewRecipes()
            .then((response) => {
                setTimeout(() => {
                    this.newRecipes = response;
                    this.loadedNewRecipes = true;
                }, 1200)
            });
    },
    methods: {
        fetchFeaturedRecipes: async function () {
            return [
                {
                    id: 1,
                    name: "Super szybkie tagliatelle z kurczakiem",
                    difficultyLevel: 1,
                    time: 30,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/super-szybkie-tagliatelle-z-kurczakiem658590.jpg"
                },
                {
                    id: 2,
                    name: "Piersi z kurczaka z mozzarellą i pomidorami",
                    difficultyLevel: 2,
                    time: 45,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/piersi-z-kurczaka-z-mozzarella-i-pomidorami-video.jpg"
                },
                {
                    id: 3,
                    name: "Roladki po bolońsku",
                    difficultyLevel: 1,
                    time: 120,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/roladki-po-bolonsku543812.jpg"
                },
                {
                    id: 4,
                    name: "Naleśniki z kurczakiem i pieczarkami",
                    difficultyLevel: 1,
                    time: 60,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/nalesniki-z-kurczakiem-i-pieczarkami.jpg"
                }
            ];
        },
        fetchPopularRecipes: async function () {
            return [
                {
                    id: 6,
                    name: "Zapiekanka makaronowa \"PYCHOTKA\"",
                    difficultyLevel: 1,
                    time: 45,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/zapiekanka-makaronowa-pychotka.jpg"
                },
                {
                    id: 7,
                    name: "Duszona karkówka w sosie własnym z cebulą",
                    difficultyLevel: 2,
                    time: 60,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/karkowka-duszona-w-sosie-wlasnym-z-cebula.jpg"
                },
                {
                    id: 8,
                    name: "Makaron w sosie pieczarkowym",
                    difficultyLevel: 1,
                    time: 20,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/cheesy-chicken-and-pasta_a9727.jpg"
                },
                {
                    id: 9,
                    name: "Zapiekanka z brokułami",
                    difficultyLevel: 1,
                    time: 25,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/pasta-and-broccoli-casserole_3491496.jpg"
                },
            ]
        },
        fetchNewRecipes: async function () {
            return [
                {
                    id: 6,
                    name: "Zapiekanka makaronowa \"PYCHOTKA\"",
                    difficultyLevel: 1,
                    time: 45,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/zapiekanka-makaronowa-pychotka.jpg"
                },
                {
                    id: 7,
                    name: "Duszona karkówka w sosie własnym z cebulą",
                    difficultyLevel: 2,
                    time: 60,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/karkowka-duszona-w-sosie-wlasnym-z-cebula.jpg"
                },
                {
                    id: 8,
                    name: "Makaron w sosie pieczarkowym",
                    difficultyLevel: 1,
                    time: 20,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/cheesy-chicken-and-pasta_a9727.jpg"
                },
                {
                    id: 9,
                    name: "Zapiekanka z brokułami",
                    difficultyLevel: 1,
                    time: 25,
                    image: "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/pasta-and-broccoli-casserole_3491496.jpg"
                },
            ]
        }
    }
}
</script>

<style scoped>

</style>