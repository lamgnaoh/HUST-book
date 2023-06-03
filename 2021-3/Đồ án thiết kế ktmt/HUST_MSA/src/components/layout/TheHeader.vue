<template>
  <div class="header">
    <div class="title-header">
      <img src="../../assets/imgs/logo.png" alt="" />
      <p>HỆ THỐNG QUẢN LÝ SINH VIÊN HUST MSA</p>
    </div>
    <div class="user">
      <div class="icon-rell" @click="toggle2">
        <font-awesome-icon
          class="icon-20"
          icon="bell"
          @btnClick="btnNotiOnClick"
        />
        <OverlayPanel
          ref="op"
          :breakpoints="{ '960px': '75vw', '640px': '100vw' }"
          :style="{ width: '250px' }"
          
        >
          <h1>Thông báo</h1>
          <div class="">Thông báo1</div>
          <div class="">Thông báo1</div>
          <div class="">Thông báo1</div>

        </OverlayPanel>
      </div>
      <div class="group-logout" @click="toggle" style="cursor: pointer">
        <div
          class="user-icon"
          aria-haspopup="true"
          aria-controls="overlay_tmenu"
        >
          <font-awesome-icon id="tohere" class="icon-20" icon="user" />
          <router-link to="/#" class="btn-login">
            <button ref="login" style="display: none" ></button>
          </router-link>
          <TieredMenu
            id="overlay_tmenu"
            ref="menu"
            :model="items"
            :popup="true"
            @click="logOut"
          />
        </div>
        <div class="user-name">{{ this.fullName }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import TieredMenu from "primevue/tieredmenu";
import OverlayPanel from "primevue/overlaypanel";
export default {
  name: "TheHeader",
  components: {
    TieredMenu,
    OverlayPanel,
  },
  data() {
    return {
      items: [
        {
          label: "Quit",
          icon: "pi pi-fw pi-sign-out",
        },
      ],
      isShowNoti: false,
      fullName: "",
    };
  },
  methods: {
    toggle(event) {
      this.$refs.menu.toggle(event);
    },
    toggle2(event) {
      this.$refs.op.toggle(event);
    },
    btnNotiOnClick() {
      this.isShowNoti = true;
    },
    logOut() {
      sessionStorage.clear();
      this.$refs.login.click();
    },
  },
  created() {
    this.fullName = sessionStorage.getItem("fullName");
  },
};
</script>

<style>
.area-content .header {
  display: flex;
  background-color: #fff;
  justify-content: space-between;
  align-items: center;
  padding: 8px 20px 8px 20px;
  height: 48px;
}
.title-header {
  display: flex;
  color: #000;
  font-size: 14px;
  font-weight: 600;
  align-items: center;
}
.title-header > img {
  height: 15px;
  margin-left: -10px;
  margin-right: 5px;
  opacity: 0.7;
}
.title-header > p {
  font-size: 14px;
  padding-left: 10px;
}
.header .user {
  display: flex;
  flex-direction: row;
  align-items: center;
  color: #000;
}
.user-icon,
.icon-rell {
  margin-left: 10px;
  margin-right: 10px;
}
.group-logout {
  display: flex;
  width: 170px;
}
.width {
  width: 250px !important;
}
</style>