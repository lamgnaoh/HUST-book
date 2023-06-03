<template>
  <div class="main-content">
    <div class="header-content">
      <div class="title-content">Quản lý tài khoản</div>
      <Button
        label="Thêm tài khoản mới"
        icon="pi pi-plus"
        class="p-button-lg"
        @Click="btnAddOnClick"
      />
    </div>
    <div class="manage-content">
      <div class="manage-toolbar">
        <div class="search-account">
          <span class="p-input-icon-left">
            <i class="pi pi-search" @click="enterSearch"/>
            <InputText type="text" v-model="keyword" placeholder="Tìm tài khoản" class="input-240" @keyup.enter="enterSearch"/>
          </span>
        </div>
      </div>
      <div class="table-account">
        <DataTable :value="tableDataList" responsiveLayout="scroll">
          <Column field="username" header="TÀI KHOẢN"></Column>
          <Column field="fullName" header="HỌ VÀ TÊN"></Column>
          <Column field="roles" header="VAI TRÒ">
            <template #body="slotProps">
              <p>{{slotProps.data.roles.join(' ')}}</p>
            </template>
          </Column>
          <Column field="mssv" header="MSSV"></Column>
          <Column field="email" header="EMAIL"></Column>
          <Column field="phoneNumber" header="SỐ ĐIỆN THOẠI"></Column>
          <Column field="id" header="CHỨC NĂNG">
            <template #body="slotProps">
              <Button
                icon="pi pi-trash"
                class="p-button-rounded p-button-danger mb-2"
                @click="btnDeleteUser(slotProps.data.id,slotProps.data.username)"
              />
              <Button
                icon="pi pi-user-edit"
                class="p-button-rounded p-button-warning"
                @click="btnUpdateUser(slotProps.data.id)"
              />
            </template>
          </Column>
        </DataTable>
      </div>
    </div>
  </div>
  <Dialog
    header="Chi tiết tài khoản"
    v-model:visible="displayBasic"
    :breakpoints="{ '960px': '55vw', '640px': '40vw' }"
    :style="{ width: '30vw' }"
  >
    <div class="field">
      <label for="input">Vai trò</label>
      <div class="flex-group">
        <div class="field-radiobutton">
          <RadioButton
            inputId="1"
            name="roleIDs"
            :value="1"
            v-model="userModel.roleIDs"
          />
          <label for="1">Admin</label>
        </div>
        <div class="field-radiobutton">
          <RadioButton
            inputId="2"
            name="roleIDs"
            :value="2"
            v-model="userModel.roleIDs"
          />
          <label for="2">Teacher</label>
        </div>
        <div class="field-radiobutton">
          <RadioButton
            inputId="3"
            name="roleIDs"
            :value="3"
            v-model="userModel.roleIDs"
          />
          <label for="3">Student</label>
        </div>
      </div>
    </div>
    <div class="field">
      <label for="input">Tài khoản</label>
      <InputText
        id="input"
        type="username"
        v-model="userModel.username"
        @focus="
          () => {
            touched.username = true;
          }
        "
        :class="{
          'border-red': v$.userModel.username.$invalid && touched.username,
        }"
      />
    </div>
    <div class="field" v-if="this.modeAdd">
      <label for="input">Mật khẩu</label>
      <InputText
        id="input"
        type="username"
        v-model="userModel.password"
        @focus="
          () => {
            touched.password = true;
          }
        "
        :class="{
          'border-red': v$.userModel.password.$invalid && touched.password,
        }"
      />
    </div>
    <div class="field">
      <label for="input">Họ và tên</label>
      <InputText
        id="input"
        type="username"
        v-model="userModel.fullName"
        @focus="
          () => {
            touched.fullName = true;
          }
        "
        :class="{
          'border-red': v$.userModel.fullName.$invalid && touched.fullName,
        }"
      />
    </div>
    <div class="field">
      <label for="input">Mã số sinh viên</label>
      <InputText id="input" type="username" v-model="userModel.mssv" />
    </div>
    <div class="field">
      <label for="input">Email</label>
      <InputText
        id="input"
        type="username"
        v-model="userModel.email"
        @focus="
          () => {
            touched.email = true;
          }
        "
        :class="{
          'border-red': v$.userModel.email.$invalid && touched.email,
        }"
      />
    </div>
    <div class="field">
      <label for="input">Số điện thoại</label>
      <InputText
        id="input"
        type="username"
        v-model="userModel.phoneNumber"
        @focus="
          () => {
            touched.phoneNumber = true;
          }
        "
        :class="{
          'border-red': v$.userModel.phoneNumber.$invalid && touched.phoneNumber,
        }"
      />
    </div>
    <template #footer>
      <Button
        label="Yes"
        icon="pi pi-check"
        @click="updateUserConfirm"
        autofocus
      />
    </template>
  </Dialog>
</template>
  
  <script>
import UserApi from "../../api/entities/UserApi";
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import Button from "primevue/button";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import RadioButton from "primevue/radiobutton";
import { required, helpers } from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
export default {
  name: "TheButton",
  components: {
    Button,
    DataTable,
    Column,
    Dialog,
    InputText,
    RadioButton,
  },
  setup() {
    return { v$: useVuelidate() };
  },
  validations() {
    return {
      userModel: {
        username: {
          required: helpers.withMessage(
            "Tài khoản không được để trống",
            required
          ),
        },
        password: {
          required: helpers.withMessage(
            "Mật khẩu không được để trống",
            required
          ),
        },
        fullName: {
          required: helpers.withMessage(
            "Họ và tên không được để trống",
            required
          ),
        },
        email: {
          required: helpers.withMessage("Email không được để trống", required),
        },
        phoneNumber: {
          required: helpers.withMessage(
            "Số điện thoại không được để trống",
            required
          ),
        },
      },
    };
  },
  data() {
    return {
      // giá trị cho biết các trường đã focus vào lần nào hay chưa
      touched: {
        username: false,
        fullName: false,
        password: false,
        email: false,
        phoneNumber: false,
      },
      modeAdd: false,
      employeeSelected: {},
      tableDataList: [],
      // các biến lưu dữ liệu của paging
      totalRecord: 0,
      currentPage: 1,
      pagingSize: 20,
      // các biến liên quan đến xử lí với dialog
      isShowDialog: false,
      dialogMode: "add",
      isReOpenDialog: false,
      // lưu giá trị nhận được từ inputSearch
      searchTerms: "",
      displayBasic: false,
      userModel: {},
      originalModel: {},
      roleList: null,
      idUserUpdate: "",
      keyword:"",
    };
  },
  methods: {
    enterSearch(){
      UserApi.search(this.keyword).then((res)=>{
        console.log(res.data);
        this.tableDataLis=[];
        this.tableDataList=res.data;
      })
    },
    btnAddOnClick() {
      this.modeAdd = true;
      this.displayBasic = true;
      this.userModel = {};
      this.userModel.roleIDs = 3;
      this.touched = {
        username: false,
        fullName: false,
        password: false,
        email: false,
        phoneNumber: false,
      };
    },
    btnUpdateUser(idUser) {
      UserApi.getById(idUser).then((res) => {
        console.log(res);
        this.userModel = res.data;
        [this.userModel.roleIDs]=this.userModel.roleIDs;
        this.userModel.password = "dsdfsdf";
        this.idUserUpdate = idUser;
        this.originalModel = Object.assign({}, this.userModel);
      });
      this.displayBasic = true;
      this.modeAdd = false;
    },
    async updateUserConfirm() {
      this.touched = {
        username: true,
        fullName: true,
        password: true,
        email: true,
        phoneNumber: true,
      };
      const result = await this.v$.$validate();
      if (!result) {
        //sự kiện gửi thông tin thông tin lỗi cho popup
        this.$confirm.require({
          message: this.v$.$silentErrors[0].$message,
          header: "Warning!",
          icon: "pi pi-exclamation-triangle",
        });
      } else {
        if (this.modeAdd) {
          if (this.userModel) {
            this.userModel.roleIDs=[this.userModel.roleIDs];
            UserApi.add(this.userModel)
              .then((res) => {
                this.displayBasic = false;
                console.log(res);
                this.$toast.add({
                  severity: "success",
                  summary: "SUCCESS",
                  detail: "Thêm thành công!",
                  life: 3000,
                });
                this.load();
              })
              .catch((err) => {
                console.log(err);
                this.displayBasic = false;
                this.$toast.add({
                  severity: "error",
                  summary: `ERROR`,
                  detail: "Thêm thất bại!",
                  life: 3000,
                });
              });
          } else {
            this.$toast.add({
              severity: "warn",
              summary: `WARNING`,
              detail: "Thêm thất bại!",
              life: 3000,
            });
          }
        } else {
          if (
            JSON.stringify(this.originalModel) ===
            JSON.stringify(this.userModel)
          ) {
            this.$toast.add({
              severity: "warn",
              summary: "WARNING",
              detail: "Dữ liệu chưa được thay đổi",
              life: 3000,
            });
          } else {
            this.userModel.roleIDs=[this.userModel.roleIDs];
            UserApi.update(this.idUserUpdate, this.userModel)
              .then(async (res) => {
                this.displayBasic = false;
                console.log(res);
                this.$toast.add({
                  severity: "success",
                  summary: "SUCCESS",
                  detail: "Cập nhật thành công!",
                  life: 3000,
                });
                this.load();
              })
              .catch((err) => {
                console.log(err);
                this.displayBasic = false;
                this.$toast.add({
                  severity: "success",
                  summary: "Cập nhật thất bại!",
                  detail: "vui lòng kiểm tra lại",
                  life: 3000,
                });
                this.load();
              });
          }
        }
      }
    },
    btnDeleteUser(idUser,name) {
      this.$confirm.require({
        message: `Bạn có thực sự muốn xóa tài khoản <${name}> không`,
        header: "Xác nhận",
        icon: "pi pi-exclamation-triangle",
        accept: () => {
          UserApi.delete(idUser)
            .then((res) => {
              console.log(res);
              this.$confirm.close();
              this.$toast.add({
                severity: "success",
                summary: "Xóa thành công!",
                detail: "vui lòng kiểm tra",
                life: 3000,
              });
              this.load();
            })
            .catch((err) => {
              console.log(err);
              this.$confirm.close();
              this.$toast.add({
                severity: "error",
                summary: `Xóa thất bại!`,
                detail: "vui lòng kiểm tra lại",
                life: 3000,
              });
            });
        },
        reject: () => {
          //callback to execute when user rejects the action
          this.$confirm.close();
        },
        onHide: () => {
          //Callback to execute when dialog is hidden
          this.$confirm.close();
        },
      });
    },

    getQueryStringFilter() {
      var paramStrs = `PageNumber=${this.currentPage}&PageSize=${this.pagingSize}`;
      if (this.searchTerms !== undefined && this.searchTerms !== "") {
        paramStrs += `&searchTerms=${this.searchTerms}`;
      }
      return paramStrs;
    },
    /**
     * Gọi api filter để thực hiện lấy dữ liệu đã được tìm kiếm và phân trang,
     * nhận res.data là list employee truyền cho Table và ToltalRecord để truyền cho pagingBar
     * Author TrungTQ
     * */
    load() {
      this.emitter.emit("showLoader");
      var vm = this;
      UserApi.getFilterPaging(this.getQueryStringFilter()).then((res) => {
        console.log(res);
        vm.tableDataList = res.data;
        this.emitter.emit("hideLoader");
      });
    },
  },
  created() {
    this.load();
    this.emitter.on("load", () => {
      this.load();
    });
    this.emitter.on("hideDialog", () => {
      this.isShowDialog = false;
    });
  },
};
</script>

  <style>
.manage-content {
  background-color: #fff;
  width: 100%;
  height: calc(100vh - 154px);
  display: flex;
  flex-direction: column;
  overflow: auto;
}
.manage-toolbar {
    position: sticky;
    z-index: 2;
    top: 0;
    background-color: #fff;
}
.search-account {
  display: flex;
  justify-content: end;
  padding: 24px 20px;
}
.flex-group {
  display: flex !important;
  justify-content: space-around;
}
.field-radiobutton {
  display: flex !important;
}
.field-radiobutton > label {
  margin-left: 10px;
}
.icon-search {
  position: absolute;
  right: 27px;
  top: 22px;
  color: #68686863;
  font-size: 19px;
}
.table-account {
  width: 100%;
  height: calc(100% - 68px);
  padding: 0 20px 20px 20px;
}
td:last-child {
  align-items: center;
  display: flex;
  justify-content: space-around;
}
.field * {
  display: block;
}
.field {
  margin-bottom: 10px;
}
#input {
  width: 100%;
}
.field > label {
  font-weight: 600;
  margin-bottom: 5px;
}
[notvalid] {
  border: 1px solid #ff4747;
}

.border-red {
  border: 1px solid red !important;
}
.p-datatable-thead{
  position: sticky;
  top: 0;
}
</style>