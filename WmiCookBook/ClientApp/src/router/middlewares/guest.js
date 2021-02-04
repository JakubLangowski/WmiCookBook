import store from '@/store'

export default function guest({ from, to, next, router }) {
    if (store.getters["user/isAuthenticated"])
        return router.push({ name: 'AdminDashboardPage' });
    if (from.name !== 'login')
        return next();
    if (to.name === "register")
        return next();
}