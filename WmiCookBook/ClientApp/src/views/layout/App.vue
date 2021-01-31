<template>
    <div id="app">
        <Header />
        <router-view></router-view>
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
