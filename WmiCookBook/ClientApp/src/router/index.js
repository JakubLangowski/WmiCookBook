import Vue from 'vue'
import VueRouter from 'vue-router'
import HomePage from "@/views/HomePage";
import AddRecipePage from "@/views/AddRecipePage";
import NotFound from "@/views/errors/NotFound";
import Forbidden from "@/views/errors/Forbidden";
import RecipesPage from "@/views/RecipesPage";


Vue.use(VueRouter)

const routes = [

    { path: '/', component: HomePage, meta: { title: "Home"} },
    { path: '/home', component: HomePage, meta: { title: "Home"}},
    { path: '/recipes', component: RecipesPage, meta: { title: "Przepisy"}},
    { path: '/addRecipe', component: AddRecipePage, meta: { title: "Utwórz Przepis"}},

    { path: '/forbidden', component: Forbidden, meta: { title: "Forbidden"} },
    { path: "*", component: NotFound, meta: { title: "Not Found"} }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
