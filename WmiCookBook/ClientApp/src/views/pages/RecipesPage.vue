<template>
    <div>
        <h1 class="text-4xl font-semibold text-center text-gray-800 max-h-20">Przepisy</h1>
        <div class="grid grid-cols-12 gap-y-3 gap-x-8">
            <div class="col-span-12 xl:col-start-1 xl:col-span-12">
                <SelectInput @changed="categoryChanged"
                             :selectedCategory="categoryId"
                             class="w-full md:w-64 float-right"
                             name="Kategoria" label="Kategoria"
                             :data="categories"
                             obj-key="id" obj-value-key="name">
                </SelectInput>
            </div>
            <div class="col-span-12 xl:col-start-1 xl:col-span-12 h-full mt-6">
                <RecipesList v-if="recipes.length > 0 || !recipesLoaded" :is-loaded="recipesLoaded" title="Przepisy" :recipes="recipes" :skeleton-loader-length="12"/>
                <br>
                <div class="flex justify-center">
                    <Pagination
                        :first-page="meta.firstPage"
                        :last-page="meta.lastPage"
                        :next-page="meta.nextPage"
                        :previous-page="meta.previousPage"
                        :page-number="meta.pageNumber"
                        :page-size="meta.pageSize"
                        :total="meta.total"
                        :total-pages="meta.totalPages"
                        @paginationClick="fetchRecipes"
                    />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import RecipesList from "@/components/recipes/RecipesList";
import Pagination from "@/components/shared/Pagination";
import SelectInput from "@/components/shared/SelectInput";
export default {
  name: "RecipesPage",
    components: {SelectInput, Pagination, RecipesList},
    data: () => ({
        recipesLoaded: false,
        recipes: [],
        categoryId: null,
        meta: {
            total: 0,
            totalPages: 0,
            pageNumber: 0,
            pageSize: 0,
            nextPage: "",
            previousPage: "",
            firstPage: "",
            lastPage: ""
        },
        showMobileMenu: false,
        categories: []
    }),
    mounted() {
        this.categoryId = this.$route.query.categoryId;
        this.fetchCategories()
        this.fetchRecipes()
    },
    methods: {
        categoryChanged: function (categoryId) {
            this.categoryId = parseInt(categoryId);
            if (Number.isInteger(categoryId)) {
                this.$router.replace({ name: 'RecipesPage', query: { categoryId: categoryId }})
            } else {
                this.$router.replace({ name: 'RecipesPage'})
            }
            this.fetchRecipes(1)
        },
        fetchCategories: async function () {
            this.$api.get('/category')
                .then((response) => {
                    setTimeout(() => {
                        this.categories = response.data;
                    }, 200)
                })
                .catch(() => {
                    this.$store.dispatch('toast/errorToast', "Wystąpił błąd podczas wczytywania kategorii")
                })
        },
        fetchRecipes: async function (page = 1) {
            this.recipesLoaded = false;
            // this.scrollTop();
            
            const params = {
                pageNumber: page,
            }
    
            if (parseInt(this.categoryId)) {
                params.CategoryId = this.categoryId
            }
            
            this.$api.get('/recipe',{
                params
            })
                .then((response) => {
                    this.recipes = response.data.data;
                    this.meta = response.data.meta;
                    setTimeout(() => this.recipesLoaded = true, 200)
                })
                .catch(() => {
                    this.$store.dispatch('toast/errorToast', "Wystąpił błąd podczas wczytywania przepisów")
                })
        },
        // scrollTop: function () {
        //     scroll({
        //         top: 0,
        //         behavior: "smooth"
        //     });
        // },
    }
}
</script>

<style scoped lang="scss">
.navbar {
    transition: all 330ms ease-out;
}

@media (min-width: 1024px) {
    .navbar-open {
        transform: translateX(0%)!important;
    }
    .navbar-close {
        transform: translateX(0%)!important;
    }
}

.navbar-open, {
    transform: translateX(0%);
}
.navbar-close, {
    transform: translateX(-100%);
}

.nonActive {
    @apply text-gray-300 hover:bg-gray-700 hover:text-white;
}

.active {
    @apply bg-gray-900 text-white;
}
</style>