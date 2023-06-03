<template>
    <div class="container">
        <MenuBarAdmin/>
        <TheAreaContent/>
    </div>
</template>

<script>
import TheAreaContent from '../../components/layout/TheAreaContent.vue';
import MenuBarAdmin from './MenuBarAdmin.vue';
export default {
  name: 'TheContainer',
  components: {
    TheAreaContent,
    MenuBarAdmin
}
}
</script>


<style>
* {
    margin: 0;
    box-sizing: border-box;
    text-decoration-color: initial;
    margin: 0;
    padding: 0;
    font-family: GoogleSans-Regular;
    font-size: 13px;
}

.container {
    display: flex;
    flex-direction: row;
    width: 100%;
    height: 100vh;
    position: fixed;
}
</style>