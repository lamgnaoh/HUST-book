<template>
    <div class="area-content">
        <TheHeader/>
        <TheMainContent/>
    </div>
</template>

<script>
import TheHeader from './TheHeader.vue';
import TheMainContent from './TheMainContent.vue';
export default {
  name: 'TheContainer',
  components: {
    TheHeader,
    TheMainContent
}
}
</script>

<style>
.area-content {
    display: flex;
    flex-direction: column;
    flex-grow: 2;
    width: 42%;
}


.main-content {
  display: flex;
  flex-direction: column;
  background-color: #f4f5f8;
  width: 100%;
  height: calc(100% - 48px);
  padding: 0 30px 20px 20px;
  box-sizing: border-box;

}

.main-content .header-content {
    display: flex;
    justify-content: space-between;
    height: 36px;
    align-items: stretch;
    margin-top: 24px;
    margin-bottom: 24px;
}

.header-content .title-content {
    display: flex;
    font-size: 24px;
    font-weight: 700;
    color: #111;
    line-height: 36px;
}
</style>