<template>
  <div>
    <h1>Редактирование учеников</h1>
    <router-link v-if="isAdmin" to="/addnewstudent" class="nav-item nav-link"
      >Добавить нового ученика</router-link
    >
    <div>
      <concreteStudent
        v-for="(student, index) in students"
        :index="Number(index + 1)"
        :studentData="student"
        @onDelete="deleteUser"
        :key="student.user.firstname"
      />
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
      students: [],
      id: "",
      styles: {
        listStyle: {
          "list-style-type": "none",
        },
      },
      editClicked: false,
    };
  },
  created() {
    this.getStudents();
  },
  methods: {
    isAdmin() {
      return app.isAdmin();
    },
    deleteUser(id) {
      console.log(`Родительский помпонент принял и удалил юзера с айди: ${id}`);
      this.id = id;
      let indexForDelete = this.students.findIndex(
        (item) => item.userId == this.id
      );
      this.students.splice(indexForDelete, 1);
      userService.deleteStudentById(this.id);
    },
    getStudents() {
      userService.getAllStudents().then((res) => (this.students = res));
    },
    // editConcreteStudent(studentData) {
    //   this.editClicked = true;
    //   console.log(studentData.id);
    //   this.$emit("gg", studentData.id);
    //   router.push({ name: 'TEST', $props: { userId:'gg' }});
    // },
  },
};
</script>