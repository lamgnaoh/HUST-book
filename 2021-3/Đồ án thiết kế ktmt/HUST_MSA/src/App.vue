<template>
  <!-- <the-container/> -->
  <router-view></router-view>
  <Loader :isShowLoader="isShowLoader" />
  <PopupAddStudent
    v-if="isShowAddStudent"
    @hideAddStudent="
      () => {
        isShowAddStudent = false;
      }
    "
  />
  <Toast />
  <ConfirmDialog></ConfirmDialog>
</template>

<script>
import Toast from "primevue/toast";
import ConfirmDialog from "primevue/confirmdialog";
import Loader from "./components/base/Loader.vue";

export default {
  name: "App",
  components: {
    Loader,
    Toast,
    ConfirmDialog,
  },
  data() {
    return {
      isShowLoader: false,
    };
  },

  created() {
    this.emitter.on("showLoader", () => {
      this.isShowLoader = true;
    });
    this.emitter.on("hideLoader", () => {
      this.isShowLoader = false;
    });
  },
};
</script>

<style>
* {
  font-family: "GoogleSans-Regular";
  font-size: 13px;
}
a:-webkit-any-link {
  color: -webkit-link;
  cursor: pointer;
  text-decoration: none;
}
.position-re {
  position: relative;
}
.icon-20 {
  height: 20px;
  width: 20px;
}
.input-24 {
  width: 100%;
}
.p-button-label {
  font-weight: 600;
}
.p-padding {
  padding: 0.5rem 2.5rem !important;
}
::placeholder {
  font-family: "GoogleSans-Italic";
  font-size: 13px;
}
.input-240 {
  width: 240px;
}
.input-350 {
  width: 350px;
}
.input-padding-right {
  padding-right: 32px !important;
}
.i-file{
  
}
.m-button {
  background-color: #2196f3;
  border: 1px solid #2196f3;
  font-size: 1rem;
  color: #ffffff;
  cursor: pointer;
  font-weight: 600;
  border-radius: 3px;
}
.m-button:hover {
  background: #0d89ec;
  color: #ffffff;
  border-color: #0d89ec;
}
.m-input {
  height: 32px;
  border: 1px solid #bbbbbb;
  border-radius: 2px;
  padding: 0 12px 0 10px;
  outline: none;
  font-size: 13px;
}
.p-inputtext {
  width: 100%;
}
.m-input:focus {
  border: 1px solid #6558ef;
}
.m-textarea {
  height: auto;
  border: 1px solid #bbbbbb;
  border-radius: 2px;
  padding: 10px;
  outline: none;
  font-size: 13px;
}
.m-textarea:focus {
  border: 1px solid #6558ef;
}
::-webkit-scrollbar {
  width: 10px;
  background-color: #f1f1f1;
}

::-webkit-scrollbar-thumb {
  background-color: #b8bcc3;
  border-radius: 5px;
}

::-webkit-scrollbar-thumb:hover {
  background-color: #888888;
}

::-webkit-scrollbar:horizontal {
  height: 10px;
  background-color: #f1f1f1;
  z-index: 30000;
}

::-webkit-scrollbar-thumb:horizontal {
  background: #b8bcc3;
}

::-webkit-scrollbar-thumb:horizontal:hover {
  background: #888888;
}

@font-face {
  font-family: "GoogleSans-Bold";
  src: url("./assets/fonts/notosans-bold.3ede5aff.woff2") format("opentype");
}

@font-face {
  font-family: "GoogleSans-Thin";
  src: url("./assets/fonts/notosans-semibold.df3e6dc4.woff2") format("opentype");
}

@font-face {
  font-family: "GoogleSans-Italic";
  src: url("./assets/fonts/notosans-italic.1c1aaac9.woff2") format("opentype");
}

@font-face {
  font-family: "GoogleSans-Regular";
  src: url("./assets/fonts/notosans-regular.2cb88a13.woff2") format("opentype");
}
</style>
