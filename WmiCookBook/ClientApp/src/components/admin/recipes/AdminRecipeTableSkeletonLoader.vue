<template>
    <table class="border-collapse w-full">
        <thead>
        <tr class="h-10">
            <th></th>
            <th></th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="i in 10" :key="i">
            <td>
                <p class="bg-gray-200 animate-pulse p-3 rounded-lg"></p>
            </td>
            <td>
                <p class="bg-gray-200 animate-pulse p-3 rounded-lg"></p>
            </td>
            <td>
                <p class="bg-gray-200 animate-pulse p-3 rounded-lg"></p>
            </td>
        </tr>
        </tbody>
    </table>
</template>

<script>
export default {
    name: "AdminRecipeTableSkeletonLoader"
}
</script>

<style scoped lang="scss">
thead {
    tr {
        th {
            @apply p-3 font-bold uppercase bg-gray-200 border border-gray-300 hidden lg:table-cell;
        }
    }
}
tbody {
    tr {
        :first-child {
            @apply font-semibold;
        }
        @apply bg-white lg:hover:bg-gray-100 flex lg:table-row flex-row lg:flex-row flex-wrap lg:flex-nowrap mb-10 lg:mb-0;
        td {
            @apply w-full lg:w-auto p-3 text-gray-800 text-center border border-b block lg:table-cell relative lg:static;
        }
        td:first-child {
            span {
                @apply lg:hidden absolute top-0 left-0 bg-gray-200 px-1 py-1 text-xs font-semibold;
            }
        }
    }
}
</style>