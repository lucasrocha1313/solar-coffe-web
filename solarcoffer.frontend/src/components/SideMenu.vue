<template>
    <div class="side-menu-container">

    </div>
</template>

<script lang="ts">
export default {
    name: 'SideMenu',
}
</script>

<style scoped lang="scss">
    @import "@/scss/global.scss";
    .side-menu-container {
        background-color: #fcfcfc;
        height: 100vh;
        width: $menu-width;
        display: flex;
        flex-direction: column;
        padding: 0.8rem;
        box-shadow: 0 1px 1px rgba(0,0,0,0.12), 0 1px 1px rgba(0,0,0,0.24);
        box-sizing: border-box;
    }
</style>