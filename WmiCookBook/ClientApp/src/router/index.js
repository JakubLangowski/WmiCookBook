import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from "@/views/Home";
import NotFound from "@/views/errors/NotFound";
import Forbidden from "@/views/errors/Forbidden";
import Example from "@/views/Example";


Vue.use(VueRouter)

const routes = [

    { path: '/', component: Home, meta: { title: "Home"} },
    
    { path: '/home', component: Home, meta: { title: "Home"}, 
        children: [
            { path: 'dashboard', component: Forbidden, meta: { title: "Home"} },
            { path: ':id', component: Forbidden, meta: { title: "Home"} },
        ] 
    },

    { path: '/example', component: Example, meta: { title: "Example"},
        children: [
            { path: 'dashboard', component: Example, meta: { title: "Example"} },
            { path: ':id', component: Example, meta: { title: "Example"} },
        ]
    },
    
    { path: '/forbidden', component: Forbidden, meta: { title: "Forbidden"} },
    { path: "*", component: NotFound, meta: { title: "Not Found"} }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
