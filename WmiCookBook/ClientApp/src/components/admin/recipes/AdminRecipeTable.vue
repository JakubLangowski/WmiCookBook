<template>
    <AdminWrapper>
        <h1 class="text-center text-gray-700 font-bold text-3xl">{{ title }}</h1>
        <br>
        <div v-if="type === 'accepted'" class="flex justify-end pb-3">
            <Switch v-model:switchValue="showFeatured" title="Wyróżnione"/>
        </div>
        <div v-if="!isLoaded">
            <AdminRecipeTableSkeletonLoader/>
        </div>
        <table v-else class="border-collapse w-full">
            <thead>
            <tr>
                <th>Nazwa</th>
                <th>Kategoria</th>
                <th>Akcje</th>
            </tr>
            </thead>
            <tbody>
            <tr v-if="recipes.length <= 0">
                <td colspan="3" class="text-center font-bold text-2xl text-gray-700 h-48">
                    Brak przepisów
                </td>
            </tr>
            <tr v-else v-for="(recipe, index) in recipes" :key="index">
                <td>
                    <span>Nazwa</span>
                    <router-link :to="{ name: 'RecipePage', params: { id: recipe.id }}">{{ recipe.name }}</router-link>
                </td>
                <td>
                    <span>Kategoria</span>
                    {{ recipe.category.name }}
                </td>
                <td>
                    <div class="flex flex-row justify-center items-center">
                        <a v-if="type === 'accepted'" class="w-6 h-6 p-3 pr-12 cursor-pointer"
                           @click="toggleFeatured(recipe)"
                           :class="(recipe.isFeatured) ? 'star-icon' : ' star-outline-icon'"
                        ></a>
                        <TrashBtn @click="showDeleteModal(recipe)" class="w-6 h-6 p-3 cursor-pointer"/>
                    </div>
                </td>
            </tr>
            </tbody>
        </table>
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
        <Modal/>
    </AdminWrapper>
</template>

<script>
import TrashBtn from "@/components/shared/TrashBtn";
import AdminWrapper from "@/views/pages/admin/AdminWrapper";
import Pagination from "@/components/shared/Pagination";
import Modal from "@/components/shared/Modal";
import AdminRecipeTableSkeletonLoader from "@/components/admin/recipes/AdminRecipeTableSkeletonLoader";
import Switch from "@/components/shared/Switch";

export default {
    name: "AdminRecipeTable",
    props: {
        endpoint: {
            type: String,
            required: true
        },
        title: {
            type: String,
            required: true
        }
    },
    components: {Switch, AdminRecipeTableSkeletonLoader, Modal, Pagination, TrashBtn, AdminWrapper},
    data: () => ({
        recipes: [],
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
        showFeatured: false,
        isLoaded: false,
        type: "accepted"
    }),
    mounted() {
        this.fetchRecipes();
        if (this.$route.name === "AdminNotAcceptedRecipesPage") {
            this.type = "notAccepted"
        }
    },
    watch: {
        showFeatured: function () {
            this.fetchRecipes();
        }
    },
    methods: {
        fetchRecipes: function (page = 1) {
            this.isLoaded = false;
            const params = {
                pageNumber: page,
            }
            if (this.showFeatured) {
                params.featured = this.showFeatured
            }

            this.$api.get(this.endpoint, {
                params
            })
                .then(response => {
                    this.recipes = response.data.data;
                    this.meta = response.data.meta;
                    setTimeout(() => this.isLoaded = true, 200)
                })
                .catch(() => {
                    this.$store.dispatch('toast/errorToast', "Wystąpił błąd podczas wczytywania przepisów")
                })
        },
        toggleFeatured: function ({id, isFeatured}) {
            this.$api.patch(`/recipe/${id}/featured`, {
                Featured: !isFeatured
            })
                .then(() => {
                    this.$store.dispatch('toast/successToast', (isFeatured) ? 'Usunięto przepis z wyróżnionych' : 'Dodano przepis do wyróżnionych')
                    this.recipes.find(x => x.id === id).isFeatured = !isFeatured;
                })
                .catch(() => this.$store.dispatch('toast/errorToast',
                    (this.recipe.isFeatured) ? 'Wystąpił błąd podczas usuwania przepis z wyróżnionych' : 'Wystąpił błąd podczas dodawania przepis do wyróżnionych'))
        },
        showDeleteModal: function ({id}) {
            this.$store.dispatch('modal/showModal', {
                title: 'Czy na pewno chcesz usunąć przepis',
                callback: () => this.deleteRecipe(id),
            });
        },
        deleteRecipe: function (id) {
            this.$api.delete(`/recipe/${id}`)
                .then(() => {
                    this.$store.dispatch('toast/successToast', "Usunięto przepis")
                    this.fetchRecipes();
                })
                .catch(() => this.$store.dispatch('toast/errorToast', "Wystąpił błąd podczas usuwania przepisu"))
        }
    }
}
</script>

<style scoped lang="scss">
thead {
    tr {
        th {
            @apply p-3 font-bold uppercase bg-gray-200 text-gray-600 border border-gray-300 hidden lg:table-cell;
        }
    }
}

tbody {
    tr {
        :first-child {
            @apply font-semibold;
        }

        @apply bg-white lg:hover:bg-gray-100 flex lg:table-row flex-row lg:flex-row flex-wrap lg:flex-nowrap mb-10 lg:mb-0;
        td {
            @apply w-full lg:w-auto p-3 text-gray-800 text-center border border-b block lg:table-cell relative lg:static;
        }

        td:first-child {
            span {
                @apply lg:hidden absolute top-0 left-0 bg-gray-200 px-1 py-1 text-xs font-semibold;
            }
        }

        td:nth-child(2) {
            span {
                @apply lg:hidden absolute top-0 left-0 bg-gray-200 px-1 py-1 text-xs font-semibold;
            }
        }
    }
}
</style>