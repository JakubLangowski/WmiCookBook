<template>
    <div id="app" class="bg-gray-50 bg-opacity-50">
        <Header />
        <main class="mx-0 grid md:mx-auto p-3 md:container h-full min-h-64">
            <router-view></router-view>
        </main>
        <Footer />
    </div>
</template>

<script>
import Header from "@/views/layout/Header/Header";
import Footer from "@/views/layout/Footer/Footer";
export default {
    name: "App",
    components: {
        Footer,
        Header
    },
    data: () => ({
        title: '',
    }),
    created() {
        this.title = this.$route.meta.title ? this.$route.meta.title + ' | WmiCookBook' : 'WmiCookBook';
    },
    watch: {
        $route(to){
            this.title = to.meta.title ? this.$route.meta.title + ' | WmiCookBook' : 'WmiCookBook';
        },
        title() {
            document.title = this.title
        }
    }
}
</script>

<style lang="scss">

</style>
