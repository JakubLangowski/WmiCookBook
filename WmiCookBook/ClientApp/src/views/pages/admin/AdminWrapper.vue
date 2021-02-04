<template>
    <div class="grid grid-cols-12 gap-y-3 gap-x-8">
        <aside
            :class="showMobileMenu ? 'navbar-open' : 'navbar-close'"
            class="navbar lg:translate-x-full w-1/2 rounded-md lg:w-full h-full top-0 left-0 fixed z-10 lg:static
                   bg-gray-800 shadow-xl pt-6 px-3 col-span-12 lg:col-span-3">
            <div class="flex flex-col items-center text-lg font-semibold text-gray-700 space-y-2 px-6 text-center lg:text-left">
                <h1 class="my-4 text-lg font-semibold text-white text-center">Panel Administratora</h1>
                <CustomButton class="w-48" type="button" text="Wyloguj" @click="logout"></CustomButton>
            </div>
        </aside>
        <div class="col-span-12 lg:col-span-9 h-full mt-6">
            <slot></slot>
        </div>
        <div class="block lg:hidden fixed bottom-0 left-0 bg-gray-800 w-full py-3 grid grid-cols-2 z-50">
                <span @click="showMobileMenu = !showMobileMenu"
                      :class="{'bg-red-700' : showMobileMenu}"
                      class="menu-icon bg-white cursor-pointer w-8 h-8 place-self-center"></span>
            <span @click="scrollTop" class="arrow-up-icon bg-white cursor-pointer w-8 h-8 place-self-center"></span>
        </div>
    </div>
</template>

<script>
import CustomButton from "@/components/shared/CustomButton";

export default {
    name: "AdminWrapper",
    components: {CustomButton},
    data: () => ({
        showMobileMenu: false,
    }),
    methods: {
        logout: function () {
            this.$store.dispatch("user/logout")
                .then(() => {
                    this.$router.push({ name: 'HomePage' });
                })
        },
        scrollTop: function () {
            scroll({
                top: 0,
                behavior: "smooth"
            });
        },
    }
}
</script>

<style scoped lang="scss">
.navbar {
    transition: all 330ms ease-out;
}

@media (min-width: 1024px) {
    .navbar-open {
        transform: translateX(0%)!important;
    }
    .navbar-close {
        transform: translateX(0%)!important;
    }
}

.navbar-open, {
    transform: translateX(0%);
}
.navbar-close, {
    transform: translateX(-100%);
}
</style>