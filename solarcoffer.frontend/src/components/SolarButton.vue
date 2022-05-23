<template>
    <div class="btn-link">
        <button @click="visitRoute">
            <slot></slot>
        </button>
    </div>
    
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
    name: "SolarButton",
    components:{},
    props: {
        link: String
    },
    methods: {
        visitRoute(): void{
        if(this.link)
            this.$router.push(this.link)
    }
    }
    
})

</script>

<style scoped lang="scss">

</style>