<template>
    <div>
        <h1></h1>
        <h2 v-if="showHiddenH2">Hidden</h2>
        <ExampleComponent :title="title" />
        <ExampleComponent title="Button" />
        <button @click="showHiddenH2 = !showHiddenH2">Show</button>
    </div>
</template>

<script>
import ExampleComponent from "@/components/ExampleComponent";
export default {
    name: "Example",
    components: {
        ExampleComponent
    },
    data: () => ({
        title: "XD",
        counter: 0,
        showHiddenH2: false
    }),
    mounted: function () {
        this.increment()
    },
    methods: {
        increment: function () {
            this.counter++
        }
    }
}
</script>

<style scoped>

</style>