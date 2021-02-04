<template>
  <nav>
    <nav class="bg-gray-800">
      <div class="max-w-7xl mx-auto px-2 sm:px-6 lg:px-8">
        <div class="relative flex items-center justify-between h-16">
          <div class="absolute inset-y-0 left-0 flex items-center sm:hidden">
            <!-- Mobile menu button-->
            <button @click="menuOpened = !menuOpened"
                    class="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
                    aria-expanded="false">
              <span class="sr-only">Open main menu</span>
              <svg v-show="!menuOpened" class="block h-6 w-6" xmlns="http://www.w3.org/2000/svg" fill="none"
                   viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"/>
              </svg>
              <svg v-show="menuOpened" class="h-6 w-6" xmlns="http://www.w3.org/2000/svg" fill="none"
                   viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
              </svg>
            </button>
          </div>

          <div class="flex-1 flex items-center justify-center sm:items-stretch sm:justify-start">
            <div class="flex-shrink-0 flex items-center">
              <img class="block lg:hidden h-8 w-auto"
                   src="https://tailwindui.com/img/logos/workflow-mark-indigo-500.svg" alt="Workflow">
              <img class="hidden lg:block h-8 w-auto"
                   src="https://tailwindui.com/img/logos/workflow-logo-indigo-500-mark-white-text.svg" alt="Workflow">
            </div>
            <div class="hidden sm:block sm:ml-6">
              <div class="flex space-x-4">

                <NavLink
                    v-for="({to, title}, index) in links"
                    :key="index"
                    :to="to"
                    :title="title"
                />
                  <NavLink
                      v-if="!isAuthenticated"
                      to="/login"
                      title="Login"
                  />
                  <NavLink
                      v-if="isAuthenticated"
                      to="/admin/dashboard"
                      title="Panel Administratora"
                  />
              </div>
            </div>
          </div>
        </div>
      </div>

      <!--
        Mobile menu, toggle classes based on menu state.
        Menu open: "block", Menu closed: "hidden"
      -->
      <div v-show="menuOpened" class="sm:hidden">
        <div
            class="px-2 pt-2 pb-3 space-y-1"
        >
          <NavLink
              v-for="({to, title}, index) in links"
              :key="index"
              :to="to"
              :title="title"
              :isMobile="true"
          />
          <NavLink
              v-if="isAuthenticated"
              to="/admin/dashboard"
              title="Panel Administratora"
              :isMobile="true"
          />
        </div>
      </div>
    </nav>
  </nav>
</template>

<script>

import NavLink from "@/views/layout/Header/Nav/NavLink";
import {mapGetters} from "vuex";

export default {
  name: "Nav",
  components: {NavLink},
  data: () => ({
    menuOpened: false,
    links: [
      {
        to: "/home",
        title: "Strona Główna"
      },
      {
        to: "/recipes",
        title: "Przepisy"
      },
      {
        to: "/addRecipe",
        title: "Dodaj Przepis"
      }
    ]
  }),
    computed: {
        ...mapGetters('user', [
            'isAuthenticated',
        ])
    }
}
</script>

<style scoped>

</style>