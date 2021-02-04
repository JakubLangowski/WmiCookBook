import store from '@/store'

export default function auth({ from, next, router }) {
    if (store.getters["user/isAuthenticated"])
        return next();
    if (from.name !== 'login')
        return router.push({ name: 'LoginPage' });
}