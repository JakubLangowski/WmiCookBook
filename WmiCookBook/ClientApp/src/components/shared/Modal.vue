<template>
    <div v-show="show" class="fixed w-full h-full top-0 left-0 flex items-center justify-center z-20" tabindex="0">
        <div @click="hide" class="modal-overlay absolute w-full h-full bg-gray-900 opacity-50 z-20"></div>
        <div class="modal-container bg-white w-11/12 md:max-w-md mx-auto rounded shadow-lg z-50 overflow-y-auto">
            <div @keydown.esc="hide" @click="hide" class="modal-close absolute top-0 right-0 cursor-pointer flex flex-col items-center mt-4 mr-4 text-white text-sm z-50">
                <svg class="fill-current text-white" xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 18 18">
                    <path d="M14.53 4.53l-1.06-1.06L9 7.94 4.53 3.47 3.47 4.53 7.94 9l-4.47 4.47 1.06 1.06L9 10.06l4.47 4.47 1.06-1.06L10.06 9z"></path>
                </svg>
                <span class="text-sm">(Esc)</span>
            </div>
            <div class="modal-content py-4 text-left px-6 relative">
                <div class="flex justify-between items-center pb-3">
                    <p class="text-2xl font-bold">{{ title }}</p>
                    <div @click="hide" class="modal-close cursor-pointer z-50 absolute top-3 right-3">
                        <svg class="fill-current text-black" xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 18 18">
                            <path d="M14.53 4.53l-1.06-1.06L9 7.94 4.53 3.47 3.47 4.53 7.94 9l-4.47 4.47 1.06 1.06L9 10.06l4.47 4.47 1.06-1.06L10.06 9z"></path>
                        </svg>
                    </div>
                </div>
                <div class="flex justify-end pt-2">
                    <button class="px-4 bg-transparent p-3 rounded-lg text-indigo-500 hover:bg-gray-100 hover:text-indigo-400 mr-2" @click="confirm">Potwierdź</button>
                    <button class="modal-close px-4 bg-indigo-500 p-3 rounded-lg text-white hover:bg-indigo-400" @click="hide">Zamknij</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import {mapGetters} from "vuex";

export default {
    name: "Modal",
    computed: {
        ...mapGetters('modal', [
            'title',
            'callback',
            'show'
        ])
    },
    created() {
        document.addEventListener('keyup', this.escCallback);
    },
    beforeUnmount() {
        window.removeEventListener('keyup', this.escCallback);
    },
    methods: {
        escCallback: function (evt) {
            if (evt.keyCode === 27) {
                this.hide();
            }
        },
        confirm() {
            this.callback();
            this.hide();
        },
        hide() {
            this.$store.dispatch('modal/hideModal');
        }
    }
}
</script>

<style scoped>
.modal {
    transition: opacity 0.25s ease;
}
body.modal-active {
    overflow-x: hidden;
    overflow-y: visible !important;
}
</style>