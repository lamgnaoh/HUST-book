<template>
  <div class="main-content">
    <div class="header-content">
      <div class="title-content">Assignment</div>
    </div>
    <div class="manage-content">
      <div class="teacher-toolbar">
        <div class="choose-class">
          <span>Chọn lớp</span>
          <Dropdown
            v-model="selectedClass"
            :options="classes"
            optionLabel="name"
            placeholder="Select a class"
            @change="showAssignment"
          />
        </div>
      </div>
      <div class="popup-assgiment-body">
        <div
          class="assginment-list"
          v-for="assignment in assignmentList"
          :key="assignment.id"
          @click="goToAssignment(assignment.id)"
        >
          <div class="wrapper-asignment">
            <div class="assignment-popup-title">
              {{ assignment.title }}
            </div>
            <div class="assignment-popup-due">
              Due to: {{ assignment.dueTo }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <Dialog
    header="Chi tiết assignment"
    v-model:visible="displayResponsive"
    :breakpoints="{ '960px': '75vw', '640px': '90vw' }"
    :style="{ width: '40vw' }"
  >
    <div class="header">
      <div class="header-left">
        <div class="title mb-5">{{ this.assignmentModel.title }}</div>
        <div class="due-to mb-10">Due to: {{ this.assignmentModel.dueTo }}</div>
      </div>
      <div class="header-right">
        <div>Điểm:{{ this.reviewModel.grade }}</div>
        <div>Đánh giá:{{ this.reviewModel.feedback }}</div>
      </div>
    </div>

    <span class="span-style mb-5">Instructions</span>
    <div class="content mb-10">{{ this.assignmentModel.content }}</div>
    <div class="my-work">
      <span class="span-style mb-5">My work </span>
      <input type="file" multiple ref="upload" class="i-file" />
    </div>
    <div class="footer">
      <Button
        label="Turn in"
        icon="pi pi-paperclip"
        @click="turnInAssignment"
        autofocus
        class="p-button-lg"
      />
    </div>
  </Dialog>
</template>
<script>
import Dropdown from "primevue/dropdown";
import ClassApi from "@/api/entities/ClassApi";
import FileApi from "@/api/entities/FileApi";
import AssigmentApi from "@/api/entities/AssigmentApi";
import AssignmentApi from "@/api/entities/AssigmentApi";
import Dialog from "primevue/dialog";
import Button from "primevue/button";
import StudentAssignmentApi from "@/api/entities/StudentAssignmentApi";
export default {
  components: {
    Dropdown,
    Dialog,
    Button,
  },
  data() {
    return {
      selectedClass: null,
      classes: "",
      assignmentList: "",
      assignmentId: "",
      displayBasic: false,
      assignmentModel: {},
      displayResponsive: false,
      headers: {
        Authorization: sessionStorage.getItem("token"),
      },
      reviewModel: {},
    };
  },
  methods: {
    onUpload() {
      FileApi.upload().then((res) => {
        console.log(res);
        this.$toast.add({
          severity: "info",
          summary: "Success",
          detail: "File Uploaded",
          life: 3000,
        });
      });
    },
    loadClassByUser() {
      ClassApi.getClassByUser().then((res) => {
        this.classes = res.data;
      });
    },
    showAssignment() {
      AssignmentApi.getAssignmentByClassId(this.selectedClass.classId).then(
        (res) => {
          console.log(res);
          this.assignmentList = res.data;
        }
      );
    },
    goToAssignment(assignmentId) {
      this.assignmentId = assignmentId;
      console.log(assignmentId);
      this.displayResponsive = true;
      AssignmentApi.getById(assignmentId).then((res) => {
        this.assignmentModel = res.data;
      });
      StudentAssignmentApi.getStudentAssignment(assignmentId).then((res) => {
        this.reviewModel = res;
      });
    },
    turnInAssignment() {
      let files = this.$refs.upload.files;
      console.log(typeof files);
      AssigmentApi.submit(this.assignmentId, files)
        .then((res) => {
          console.log(res);
          this.$toast.add({
            severity: "success",
            summary: "SUCCESS",
            detail: "Nộp bài thành công!",
            life: 3000,
          });
        })
        .catch((err) => {
          console.log(err);
          this.$toast.add({
            severity: "error",
            summary: "ERROR",
            detail: "Nộp bài thất bại!",
            life: 3000,
          });
        });
      this.displayResponsive = false;
    },
  },
  beforeCreate() {},
  created() {
    this.loadClassByUser();
  },
};
</script>
<style>
.header {
  display: flex;
  justify-content: space-between;
}
.header-right{
  width: 150px;
}
.title {
  font-size: 18px;
  font-weight: 600;
  letter-spacing: 1px;
}
.due-to {
  font-style: italic;
  font-weight: 100;
}
.my-work {
  display: flex;
  flex-direction: column;
}
.span-style {
  font-weight: 600;
}
.mb-10 {
  margin-bottom: 10px;
}
.mb-5 {
  margin-bottom: 5px;
}
.footer {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
</style>