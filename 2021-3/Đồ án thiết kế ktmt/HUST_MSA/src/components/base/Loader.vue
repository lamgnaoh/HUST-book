<template>
  <div :class="['loader-wrapper', { 'show-loader': isShowLoader }]">
    <div class="loader">
      <div class="loader-modal"></div>
      <div class="loader-img"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheLoader",
  props: {
    isShowLoader: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style scoped>
.loader-wrapper {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  z-index: 33000;
  visibility: hidden;
}

.show-loader {
  visibility: visible !important;
}

.loader-wrapper .loader {
  width: 100%;
  height: 100%;
  position: relative;
}

.loader-wrapper .loader .loader-modal {
  width: 100%;
  height: 100%;
  background-color: #000000;
  opacity: 0.1;
}

.loader-wrapper .loader .loader-img {
  background-image: url("../../assets/Spin-1s-200px.svg");
  background-repeat: no-repeat;
  background-size: contain;
  width: 80px;
  height: 90px;
  position: absolute;
  top: 50%;
  left: 50%;
  margin-top: -40px;
  margin-left: -40px;
}
</style>