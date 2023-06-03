import { createRouter, createWebHashHistory } from "vue-router"
import Login from './components/layout/TheLogin'
import StudentContainer from './view/student/StudentContainer'
import StudentDetail from './view/student/StudentDetail'
import HomeAssignmentStudent from './view/student/HomeAssignmentStudent'

import TeacherContainer from './view/teacher/TeacherContainer'
import TeacherDetail from './view/teacher/TeacherDetail'
import ClassManage from './view/teacher/ClassManage'
import HomeAssignmentTeacher from './view/teacher/HomeAssignmentTeacher'

import AdminContainer from './view/admin/AdminContainer'
import AccountManage from './view/admin/AccountManage'

const routes = [
  {
    path: "/",
    component: Login
  },
  {
    path: "/student",
    name: 'student',
    component: StudentContainer,
    children: [
      {
        path: '/student/detail',
        component: StudentDetail
      },
      {
        path: '/student/assignment',
        component: HomeAssignmentStudent
      },
    ]
  },
  {
    path: "/teacher",
    name: 'teacher',
    component: TeacherContainer,
    children: [
      {
        path: '/teacher/detailteacher',
        component: TeacherDetail
      },
      {
        path: '/teacher/homeassignment/',
        component: HomeAssignmentTeacher,
      },
      {
        path: '/teacher/classmanage',
        component: ClassManage
      },
    ]
  },
  {
    path: "/admin",
    name: 'admin',
    component: AdminContainer,
    children: [
      {
        path: '/admin/accountmanage',
        component: AccountManage
      },
    ]
  },

];
const router = createRouter({
  history: createWebHashHistory(process.env.BASE_URL),
  routes,
})
export default router