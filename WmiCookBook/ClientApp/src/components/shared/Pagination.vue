<template>
    <nav v-if="totalPages > 1" class="pagination-wrapper" aria-label="Pagination">
        <span @click="previousNextClick(previousPage)" class="pagination-right">
            <span class="sr-only">Previous</span>
            <svg class="h-5 w-5" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd" d="M12.707 5.293a1 1 0 010 1.414L9.414 10l3.293 3.293a1 1 0 01-1.414 1.414l-4-4a1 1 0 010-1.414l4-4a1 1 0 011.414 0z"/>
            </svg>
        </span>
        <span v-for="page in pageArray" :key="page" @click="pageClick(page)" class="pagination-center" :class="{ 'pagination-active' : page === pageNumber}">
            {{ page }}
        </span>
        <span @click="previousNextClick(nextPage)" class="pagination-left">
            <span class="sr-only">Next</span>
            <svg class="h-5 w-5" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z"/>
            </svg>
        </span>
        <span class="pl-6">
            <input class="pagination-input" type="text" :value="pageNumber" ref="paginationInput" @focusout="inputClick">
        </span>
    </nav>
</template>

<script>
export default {
    name: "Pagination",
    emits: ['paginationClick'],
    props: {
        total: {
            type: Number,
            required: true,
        },
        totalPages: {
            type: Number,
            required: true,
        },
        pageNumber: {
            type: Number,
            required: true,
        },
        pageSize: {
            type: Number,
            required: true,
        },
        nextPage: {
            required: true,
        },
        previousPage: {
            required: true,
        },
        firstPage: {
            required: true,
        },
        lastPage: {
            required: true,
        },
    },
    computed: {
        pageArray: function () {
            const array = [];
            if (this.previousPage != null) {
                array.push(this.pageNumber - 1)
            }
            array.push(this.pageNumber)
            if (this.nextPage != null) {
                array.push(this.pageNumber + 1)
            }
            return array
        },
    },
    methods: {
        inputClick: function () {
            if (this.$refs.paginationInput.value <= this.totalPages && this.$refs.paginationInput.value >= 1 && parseInt(this.$refs.paginationInput.value) !== this.pageNumber) {
                this.$emit('paginationClick', this.$refs.paginationInput.value)
            }
        },
        pageClick: function (page) {
            if (this.pageNumber !== page){
                this.$emit('paginationClick', page)
            }
        },
        previousNextClick: function (url) {
            const pageNumber = this.getParams(url);
            if (pageNumber) {
                this.$emit('paginationClick', pageNumber)
            }
        },
        getParams: function (url) {
            if (url) {
                url = new URL(url);
                const urlParams = new URLSearchParams(url.search);
                return urlParams.get('pageNumber')
            }
            return null;
        }
    }
}
</script>

<style scoped lang="scss">
.pagination-wrapper{
    @apply relative z-0 inline-flex shadow-sm -space-x-px;

    .pagination-active{
        color: rgba(239, 68, 68)!important;
    }
    .pagination-right{
        @apply cursor-pointer relative inline-flex items-center px-2 py-2 rounded-l-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50;
    }
    .pagination-left{
        @apply cursor-pointer relative inline-flex items-center px-2 py-2 rounded-r-md border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50;
    }
    .pagination-center{
        @apply cursor-pointer relative inline-flex items-center px-4 py-2 border border-gray-300 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50;
    }
    .pagination-input {
        @apply w-12 cursor-pointer relative inline-flex items-center px-2 py-2 rounded-md text-center border border-gray-300 bg-white text-sm font-medium text-gray-500 hover:bg-gray-50;
    }
}
</style>