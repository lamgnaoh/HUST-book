<template>
  <div class="main-content">
    <div class="header-content">
      <div class="title-content">Assignment</div>
    </div>
    <!-- <router-view></router-view> -->
    <div class="assignment-main-content">
      <div class="tool-header">
        <Button
          label="Tạo lớp"
          icon="pi pi-plus"
          @Click="addClass"
          class="p-button-lg"
        />
      </div>
      <div tag="main" name="card" class="body-content">
        <div class="classroom-group">
          <router-link
            to="/teacher/homeassignment"
            v-for="column in classList"
            :key="column.name"
            class="card-class"
            @dblclick="goTodetail(column.classId)"
          >
            <div class="description">
              <span>{{ column.name }}</span>
              <div class="tool">
                <Button
                  icon="pi pi-pencil"
                  class="p-button-rounded p-button-secondary p-button-text"
                  @click="updateClass(column.classId)"
                />
                <Button
                  icon="pi pi-trash"
                  class="p-button-rounded p-button-secondary p-button-text"
                  @click="btnDeleteClass(column.classId)"
                />
              </div>
            </div>
            <!-- </div> -->
          </router-link>
        </div>
      </div>
    </div>
  </div>
  <PopupDetailClass
    :isShowPopupDetailClass="isShowPopupDetailClass"
    :idClass="classId"
    @goBack="
      () => {
        isShowPopupDetailClass = false;
      }
    "
  />
  <Dialog
    header="Thông tin lớp"
    v-model:visible="displayBasic"
    :breakpoints="{ '960px': '55vw', '640px': '40vw' }"
    :style="{ width: '25vw' }"
  >
    <span class="p-float-label">
      <InputText type="text" v-model="classModel.name" style="width: 100%" />
      <label for="name">Tên lớp</label>
    </span>
    <template #footer>
      <Button
        label="Yes"
        icon="pi pi-check"
        @click="btnAddClassOnClick"
        autofocus
      />
    </template>
  </Dialog>
</template>
<script>
import ClassApi from "../../api/entities/ClassApi";
import PopupDetailClass from "./PopupDetailClass.vue";
import Button from "primevue/button";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
export default {
  components: {
    PopupDetailClass,
    Button,
    Dialog,
    InputText,
  },
  data() {
    return {
      classList: "",
      isShowPopupDetailClass: false,
      displayBasic: false,
      classModel: {},
      classId: "",
      modeAddClass: "",
    };
  },
  methods: {
    getListClass() {
        ClassApi.getClassByUser().then((res) => {
        console.log(res);
        this.classList = res.data;
      });
    },
    addClass() {
      this.displayBasic = true;
      this.modeAddClass = "add";
      this.classModel.name=""
    },
    updateClass(classId) {
      ClassApi.getById(classId).then((res)=>{
        this.classModel.name=res.data.name
      })
      this.classId = classId;
      alert(this.classId);
      this.displayBasic = true;
      this.modeAddClass = "update";

    },
    goTodetail(classId) {
      this.isShowPopupDetailClass = true;
      this.classId = classId;
    },
    btnAddClassOnClick() {
      if (this.modeAddClass == "add") {
        ClassApi.add(this.classModel)
          .then((res) => {
            console.log(res);
            this.$toast.add({
              severity: "success",
              summary: "SUCCESS!",
              detail: "Thêm thành công",
              life: 3000,
            });
            this.displayBasic = false;
            this.getListClass();
          })
          .catch((err) => {
            console.log(err);
            this.$toast.add({
              severity: "error",
              summary: "ERROR!",
              detail: "Thêm thất bại",
              life: 3000,
            });
            this.displayBasic = false;
          });
      } else {
        ClassApi.update(this.classId, this.classModel)
          .then((res) => {
            console.log(res);
            this.$toast.add({
              severity: "success",
              summary: "SUCCESS!",
              detail: "Sửa thành công",
              life: 3000,
            });
            this.displayBasic = false;
            this.getListClass();
          })
          .catch((err) => {
            console.log(err);
            this.$toast.add({
              severity: "error",
              summary: "ERROR!",
              detail: "Sửa thất bại",
              life: 3000,
            });
            this.displayBasic = false;
          });
      }
    },
    btnDeleteClass(classId) {
      this.$confirm.require({
        target: event.currentTarget,
        message: "Do you want to delete this record?",
        icon: "pi pi-info-circle",
        acceptClass: "p-button-danger",
        accept: () => {
          ClassApi.delete(classId).then((res) => {
            console.log(res);
            this.$toast.add({
              severity: "success",
              summary: "SUCCESS",
              detail: "Xóa lớp thành công",
              life: 3000,
            });
            this.getListClass();
          })
          .catch((err)=>{
            console.log(err);
            this.$toast.add({
              severity: "error",
              summary: "ERROR!",
              detail: "Xóa thất bại",
              life: 3000,
            });
          })
        },
        reject: () => {},
      });
    },
  },
  created() {
    this.getListClass();
  },
};
</script>
<style>
.assignment-main-content {
  background-color: #fff;
  width: 100%;
  height: calc(100vh - 154px);
  display: flex;
  flex-direction: column;
  overflow: auto;
}
.tool-header {
  padding: 24px 0 20px 42px;
  position: sticky;
  top: 0;
  left: 0;
  z-index: 2;
  background-color: #fff;
}
.classroom-group {
  display: flex;
  width: 100%;
  flex-wrap: wrap;
  margin-top: 30px;
}

.card-class {
  height: 150px;
  width: 394px;
  margin-left: 24px;
  transform: perspective(1000px) translate3d(0px, 0px, -250px) rotateX(0)
    scale(1.15, 1.1);
  border-radius: 20px;
  border: 4px solid #e6e6e6;
  box-shadow: 0 40px 40px -20px rgba(0, 0, 0, 0.2);
  transition: 0.3s ease-in-out transform;
  cursor: pointer;
  background-color: #b2acf6;
  /* background-image: url("../../assets/imgs/bg-class.png");
  background-size: contain;
  background-repeat: no-repeat;  */
}

.card-class:hover {
  transform: translate3d(0px, 0px, 0px);
}
.description {
  padding: 20px 0 0 20px;
  display: flex;
  justify-content: space-between;
}
.description > span {
  font-size: 18px;
  color: #fff;
}
.description .tool {
  font-size: 18px;
  margin-top: -15px;
  margin-right: 5px;
}
@keyframes mouseOver {
  0% {
    top: 0;
  }
  100% {
    top: -5px;
  }
}

@keyframes mouseOut {
  0% {
    top: -5px;
  }
  100% {
    top: 0;
  }
}

@keyframes imageFadeIn {
  0% {
    opacity: 0;
  }
  50% {
    opacity: 0.1;
  }
  100% {
    opacity: 1;
  }
}
</style>