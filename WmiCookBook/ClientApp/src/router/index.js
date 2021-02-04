import {createRouter, createWebHistory} from 'vue-router'

import HomePage from "@/views/pages/HomePage";
import AddRecipePage from "@/views/pages/AddRecipePage";
import RecipesPage from "@/views/pages/RecipesPage";
import RecipePage from "@/views/pages/RecipePage";
import NotFound from "@/views/errors/NotFound";
import Forbidden from "@/views/errors/Forbidden";
import ContactPage from "@/views/pages/ContactPage";
import RegulationsPage from "@/views/pages/RegulationsPage";
import LoginPage from "@/views/pages/LoginPage";
import AdminDashboardPage from "@/views/pages/admin/AdminDashboardPage";
import auth from "@/router/middlewares/auth";
import guest from "@/router/middlewares/guest";
import AdminLayout from "@/views/pages/admin/AdminLayout";

const routes = [
    {path: '/', redirect: "/home"},
    {path: '/home', component: HomePage, name: "HomePage", meta: {title: "Home"} },
    {path: '/recipes', component: RecipesPage, name: "RecipesPage", meta: {title: "Przepisy"}},
    {path: '/addRecipe', component: AddRecipePage, name: "AddRecipePage", meta: {title: "Dodaj Przepis"}},
    {path: '/recipe/:id', component: RecipePage, name: "RecipePage", meta: {title: "Przepis"}},

    {path: '/login', component: LoginPage, name: "LoginPage", meta: {title: "Login", middleware: [guest],}},
    {path: '/admin', component: AdminLayout, meta: {title: "Admin Panel", middleware: [auth]}, children: [
            {path: '', component: AdminDashboardPage, meta: {title: "Admin Panel", middleware: [auth]}},
            {path: 'dashboard', component: AdminDashboardPage, name: "AdminDashboardPage", meta: {title: "Admin Panel", middleware: [auth]}},
        ]
    },
    
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
    
// Creates a `nextMiddleware()` function which not only
// runs the default `next()` callback but also triggers
// the subsequent Middleware function.
function nextFactory(context, middleware, index) {
    const subsequentMiddleware = middleware[index];
    // If no subsequent Middleware exists,
    // the default `next()` callback is returned.
    if (!subsequentMiddleware) return context.next;
    return (...parameters) => {
        // Run the default Vue Router `next()` callback first.
        context.next(...parameters);
        // Then run the subsequent Middleware with a new
        // `nextMiddleware()` callback.
        const nextMiddleware = nextFactory(context, middleware, index + 1);
        subsequentMiddleware({ ...context, next: nextMiddleware });
    };
}

router.beforeEach((to, from, next) => {
    if (to.meta.middleware) {
        const middleware = Array.isArray(to.meta.middleware)
            ? to.meta.middleware
            : [to.meta.middleware];
        const context = {
            from,
            next,
            router,
            to,
        };
        const nextMiddleware = nextFactory(context, middleware, 1);

        return middleware[0]({...context, next: nextMiddleware});
    }

    return next();
});

export default router
