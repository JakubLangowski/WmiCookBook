import { createRouter, createWebHistory } from 'vue-router'

import HomePage from "@/views/HomePage";
import AddRecipePage from "@/views/AddRecipePage";
import NotFound from "@/views/errors/NotFound";
import Forbidden from "@/views/errors/Forbidden";
import RecipesPage from "@/views/RecipesPage";

const routes = [
    {path: '/', redirect: "/home"},
    {path: '/home', component: HomePage, meta: {title: "Home"} },
    {path: '/recipes', component: RecipesPage, meta: {title: "Przepisy"}},
    {path: '/addRecipe', component: AddRecipePage, meta: {title: "Dodaj Przepis"}},

    { path: '/forbidden', component: Forbidden, meta: {title: "Forbidden"}},
    { path: '/:pathMatch(.*)*', component: NotFound, meta: {title: "Not Found"} },
    { path: '/:pathMatch(.*)', component: NotFound, meta: {title: "Not Found"} },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
