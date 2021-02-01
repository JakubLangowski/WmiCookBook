import {createRouter, createWebHistory} from 'vue-router'

import HomePage from "@/views/pages/HomePage";
import AddRecipePage from "@/views/pages/AddRecipePage";
import RecipesPage from "@/views/pages/RecipesPage";
import RecipePage from "@/views/pages/RecipePage";
import NotFound from "@/views/errors/NotFound";
import Forbidden from "@/views/errors/Forbidden";
import ContactPage from "@/views/pages/ContactPage";
import RegulationsPage from "@/views/pages/RegulationsPage";

const routes = [
    {path: '/', redirect: "/home"},
    {path: '/home', component: HomePage, name: "HomePage", meta: {title: "Home"} },
    {path: '/recipes', component: RecipesPage, name: "RecipesPage", meta: {title: "Przepisy"}},
    {path: '/addRecipe', component: AddRecipePage, name: "AddRecipePage", meta: {title: "Dodaj Przepis"}},
    {path: '/recipe/:id', component: RecipePage, name: "RecipePage", meta: {title: "Przepis"}},

    {path: '/contact', component: ContactPage, name: "ContactPage", meta: {title: "Kontakt"}},
    {path: '/regulations', component: RegulationsPage, name: "RegulationsPage", meta: {title: "Regulamin"}},

    {path: '/forbidden', component: Forbidden, name: "ForbiddenPage", meta: {title: "Forbidden"}},
    {path: '/:pathMatch(.*)*', component: NotFound, name: "NotFoundPage", meta: {title: "Not Found"}},
    {path: '/:pathMatch(.*)', component: NotFound, name: "NotFoundPage", meta: {title: "Not Found"}},
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
