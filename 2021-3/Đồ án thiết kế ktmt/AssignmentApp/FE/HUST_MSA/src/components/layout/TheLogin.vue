<template>
  <div class="background">
    <div class="login-area">
      <div class="img-login"></div>
      <div class="main-login">
        <div class="logo-login"></div>
        <div class="header-login">HỆ THỐNG QUẢN LÝ BÀI TẬP SINH VIÊN</div>
        <div class="form-login">
          <p>Đăng nhập</p>
          <div class="form-group">
            <font-awesome-icon icon="user" class="icon-input" />
            <input
              class="input-login"
              type="text"
              placeholder="Tên đăng nhập"
              v-model="userModel.email"
            />
          </div>
          <div class="form-group">
            <font-awesome-icon icon="lock" class="icon-input" />
            <input
              class="input-login"
              :type="visibility"
              name=""
              id="password"
              placeholder="Mật khẩu"
              v-model="userModel.password"
            />
          </div>
          <div class="show-pass">
            <label style="padding-left: 25px" class="custom-checkbox">
              <i @click="showPass()" v-if="visibility == 'password'">
                <font-awesome-icon icon="eye-slash" />
              </i>
              <i @click="hidePass()" v-if="visibility == 'text'">
                <font-awesome-icon icon="eye" />
              </i>
              <p>Hiển thị mật khẩu</p>
            </label>
          </div>
          <button
            type="button"
            @click="btnLogin"
            class="p-padding m-button"
            ref="loginBtn"
          >
            Đăng nhập
          </button>
          <router-link to="/admin/accountmanage" class="btn-login">
            <button ref="admin" style="display: none"></button>
          </router-link>
          <router-link to="/teacher/detailteacher" class="btn-login">
            <button ref="teacher" style="display: none"></button>
          </router-link>
          <router-link to="/student" class="btn-login">
            <button ref="student" style="display: none"></button>
          </router-link>
        </div>
        <div class="download-app">
          <p>Tải app tại</p>
          <div class="app-wrapper">
            <div class="app-store"></div>
            <div class="google-play"></div>
          </div>
        </div>
        <div class="footer-login">
          <div>
            <font-awesome-icon icon="phone" class="icon-footer" />
          </div>
          <div class="main-footer">
            <p>TƯ VẤN VÀ HỖ TRỢ</p>
            <p>Hotline:1900xxxx</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
// import Button from "primevue/button";
import AuthApi from "@/api/entities/AuthApi";
export default {
  name: "TheLogin",
  components: {
    // Button,
  },
  data() {
    return {
      visibility: "password",
      role: "",
      email: "",
      pass: "",
      userModel: {},
      rememberMe: true,
    };
  },

  methods: {
    showPass() {
      this.visibility = "text";
    },
    hidePass() {
      this.visibility = "password";
    },
    btnLogin() {
      AuthApi.login(this.userModel.email, this.userModel.password)
        .then(async (res) => {
          console.log(res);
          sessionStorage.setItem("token", "Bearer " + res.data);
          AuthApi.getInfo().then((res) => {
            sessionStorage.setItem("fullName", res.data.fullName);
            sessionStorage.setItem("role", res.data.stringrole);
            sessionStorage.setItem("id", res.data.userId);
            sessionStorage.setItem("username", res.data.username);
            sessionStorage.setItem("email", res.data.email);
            sessionStorage.setItem("phoneNumber", res.data.phoneNumber);
            sessionStorage.setItem("mssv", res.data.mssv);
            sessionStorage.setItem("password", res.data.password);
          });
          this.role = sessionStorage.getItem("role");

          if (this.role == "admin") {
            this.$refs.admin.click();
          }
          if (this.role == "teacher") {
            this.$refs.teacher.click();
          }
          if (this.role == "student") {
            this.$refs.student.click();
          }
          setTimeout(() => {
            this.$refs.loginBtn.click();
          }, 100);
          this.$toast.add({
            severity: "success",
            summary: "Đăng nhập thành công!",
            life: 3000,
          });
        })
        .catch((err) => {
          console.log(err);
          this.$toast.add({
            severity: "error",
            summary: "Đăng nhập thất bại!",
            detail: "vui lòng kiểm tra email và mật khẩu",
            life: 3000,
          });
        });
    },
  },
};
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
.background {
  display: flex;
  flex-direction: row;
  width: 100%;
  height: 100vh;
  position: fixed;
  background-image: linear-gradient(180deg, #daa02e, #0f757f);
  justify-content: center;
  align-items: center;
}
.login-area {
  display: flex;
  flex-direction: row;
  width: 1024px;
  height: 530px;
  background-color: #fff;
  border-radius: 5px;
}
.img-login {
  width: 50%;
  background-image: url("../../assets/imgs/login-img.PNG");
  background-size: cover;
  border-top-left-radius: 5px;
  border-bottom-left-radius: 5px;
}
.main-login {
  display: flex;
  flex-direction: column;
  width: 50%;
  align-items: center;
}
.logo-login {
  width: 55%;
  height: 50px;
  background-image: url("../../assets/imgs/logo-login.png");
  background-size: contain;
  background-repeat: no-repeat;
  margin-top: 20px;
  margin-bottom: 20px;
}
.header-login {
  font-size: 18px;
  font-weight: 600;
  margin-bottom: 40px;
}
.form-login {
  display: flex;
  flex-direction: column;
  align-items: center;
}
.form-login > p {
  font-size: 17px;
  font-weight: 600;
  margin-bottom: 20px;
}
.form-group {
  position: relative;
  margin-bottom: 15px;
}
.icon-input {
  position: absolute;
  left: 25px;
  top: 10px;
  color: #686868;
}
.input-login {
  width: 350px;
  padding: 0 12px 0 50px;
  background-color: #e6e6e6;
  border: none;
  height: 32px;
  box-shadow: none;
  border-radius: 20px;
  outline: none;
}
.input-login:focus {
  border: none;
}
.show-pass,
.custom-checkbox {
  display: flex;
  margin-bottom: 10px;
  height: 22px;
  width: 100%;
  align-items: center;
  justify-content: end;
  color: #686868;
}
.custom-checkbox > p {
  padding-left: 5px;
}
.download-app {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px 0 15px 0;
}
.app-wrapper {
  display: flex;
  margin-top: 10px;
}
.app-store {
  width: 100px;
  height: 32px;
  background-image: url("../../assets/imgs/appstore.png");
  background-size: contain;
  background-repeat: no-repeat;
  margin-right: 6px;
}
.google-play {
  width: 110px;
  height: 32px;
  background-image: url("../../assets/imgs/ggplay.png");
  background-size: contain;
  background-repeat: no-repeat;
}
.footer-login {
  background-color: #dcdcdc;
  width: 100%;
  height: 79px;
  border-bottom-right-radius: 5px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.icon-footer {
  font-size: 30px;
  padding-right: 16px;
  color: #686868;
}
.main-footer {
  font-weight: 600;
}
.main-footer > p:first-child {
  color: #027185;
}
.main-footer > p:last-child {
  color: #daa02e;
}
</style>