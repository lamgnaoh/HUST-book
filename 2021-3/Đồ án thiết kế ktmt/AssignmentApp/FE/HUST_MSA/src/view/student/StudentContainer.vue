<template>
    <div class="container">
        <MenuBarStudent/>
        <TheAreaContent/>
    </div>
</template>

<script>
import MenuBarStudent from './MenuBarStudent.vue';
import TheAreaContent from '../../components/layout/TheAreaContent.vue';
export default {
  name: 'TheContainer',
  components: {
    MenuBarStudent,
    TheAreaContent,
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