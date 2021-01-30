import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from "@/views/Home";
import NotFound from "@/views/errors/NotFound";
import Forbidden from "@/views/errors/Forbidden";


Vue.use(VueRouter)

const routes = [

    { path: '/', component: Home, meta: { title: "Home"} },
    { path: '/home', component: Home, meta: { title: "Home"} },
    
    
    { path: '/forbidden', component: Forbidden, meta: { title: "Forbidden"} },
    { path: "*", component: NotFound, meta: { title: "Not Found"} }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
