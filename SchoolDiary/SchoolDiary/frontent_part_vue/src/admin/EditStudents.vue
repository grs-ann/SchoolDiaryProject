<template>
  <div>
    <h1>Редактирование учеников</h1>
    <editConcreteStudent v-if="studentEditClicked"
      :studentData="studentDataForEdit"
      @studentApplyChanges="concreteStudentChange"
    />
    <div v-if="!studentEditClicked">
      <router-link v-if="isAdmin" to="/addnewstudent" class="nav-item nav-link"
      >Добавить нового ученика</router-link>
      <div>
        <concreteStudent
          v-for="(student, index) in students"
          :index="Number(index + 1)"
          :studentData="student"
          @onDelete="deleteUser"
          @concreteStudentEdit="studentEdit"
          :key="student.user.firstname"
        />
      </div>
    </div>
  </div>
</template>

<script>
import app from "@/app/App";
import { authenticationService, userService } from "@/_services";
import { router } from "@/_helpers";
import concreteStudent from "./ConcreteStudent";
import editConcreteStudent from "./EditConcreteStudent";
export default {
  name: "editStudents",
  components: {
      concreteStudent,
      editConcreteStudent
  },
  
  data() {
    return {
      studentDataForEdit: null,
      students: [],
      id: "",
      styles: {
        listStyle: {
          "list-style-type": "none",
        },
      },
      studentEditClicked: false,
    };
  },
  created() {
    this.getStudents();
  },
  methods: {
    isAdmin() {
      return app.isAdmin();
    },
    /**
     * Удаляет ученика(пользователя) по id.
     * @param {id} Id пользователя, хранящегося в базе данных.
     */
    deleteUser(id) {
      console.log(`Родительский помпонент принял и удалил юзера с айди: ${id}`);
      this.id = id;
      let indexForDelete = this.students.findIndex(
        (item) => item.userId == this.id
      );
      this.students.splice(indexForDelete, 1);
      userService.deleteStudentById(this.id);
    },
    /**
     * Изменяет данные об ученике.
     */
    concreteStudentChange(studentData) {
      console.log(studentData);
    },
    getStudents() {
      userService.getAllStudents().then((res) => (this.students = res));
    },
    studentEdit(data) {
      this.studentEditClicked = data.clickedStatus;
      this.studentDataForEdit = data.data;
    }
  },
};
</script>