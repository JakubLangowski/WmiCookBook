import { createRouter, createWebHistory } from 'vue-router'

import HomePage from "@/views/HomePage";
import AddRecipePage from "@/views/AddRecipePage";
import NotFound from "@/views/errors/NotFound";
import Forbidden from "@/views/errors/Forbidden";
import RecipesPage from "@/views/RecipesPage";
import RecipePage from "@/views/RecipePage";

const routes = [
    {path: '/', redirect: "/home"},
    {path: '/home', component: HomePage, name: "HomePage", meta: {title: "Home"} },
    {path: '/recipes', component: RecipesPage, name: "RecipesPage", meta: {title: "Przepisy"}},
    {path: '/addRecipe', component: AddRecipePage, name: "AddRecipePage", meta: {title: "Dodaj Przepis"}},
    {path: '/recipe/:id', component: RecipePage, name: "RecipePage", meta: {title: "Przepis"}},

    { path: '/forbidden', component: Forbidden, name: "ForbiddenPage", meta: {title: "Forbidden"}},
    { path: '/:pathMatch(.*)*', component: NotFound, name: "NotFoundPage", meta: {title: "Not Found"} },
    { path: '/:pathMatch(.*)', component: NotFound, name: "NotFoundPage", meta: {title: "Not Found"} },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
