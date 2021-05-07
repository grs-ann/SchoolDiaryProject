<template>
  <div>
    <p :style="styles.validationStyle" v-if="applySubmitted">
      {{ validations.login }}
    </p><br />
    <label>Логин</label>
    <input
      type="text"
      placeholder="Логин"
      v-model="teacherData.user.login"
    /><br />
    <p :style="styles.validationStyle" v-if="applySubmitted">
      {{ validations.firstname }}
    </p>
    <label>Имя</label>
    <input
      type="text"
      placeholder="Имя"
      v-model="teacherData.user.firstname"
    /><br />
    <p :style="styles.validationStyle" v-if="applySubmitted">
      {{ validations.lastname }}
    </p>
    <label>Фамилия</label>
    <input
      type="text"
      placeholder="Фамилия"
      v-model="teacherData.user.lastname"
    /><br />
    <p :style="styles.validationStyle" v-if="applySubmitted">
      {{ validations.patronymic }}
    </p>
    <label>Отчество</label>
    <input
      type="text"
      placeholder="Отчество"
      v-model="teacherData.user.patronymic"
    /><br />
    <p :style="styles.validationStyle" v-if="applySubmitted">
      {{ validations.phone }}
    </p>
    <label>Телефон</label>
    <input
      type="text"
      placeholder="Номер телефона"
      v-model="teacherData.user.phone"
    /><br />

    <p :style="styles.validationStyle" v-if="applySubmitted">
      {{ validations.salary }}
    </p>
    <label>Зарплата</label>
    <input
      type="text"
      placeholder="Зарплата"
      v-model="teacherData.salary"
    /><br />
    <button @click="changePinnedClasses">Изменить закрепленные классы</button><br/>
    <template v-for="cl in classes" v-if="classChangeClicked">
        <input type="checkbox" v-bind:value="cl.id" v-model="selectedClasses">
        <label>{{ cl.name }}</label><br/>
    </template>

    <button @click="applyChanges">Принять изменения</button>
    <button @click="cancelChanges">Отменить изменения</button><br/>

  </div>
</template>

<script>
import axios from 'axios';
import { teacherService, classService } from "@/_services";
import { authenticationService } from '@/_services';

export default {
  data() {
    return {
      classChangeClicked: false,
      applySubmitted: false,
      validations: {
        login: "",
        firstname: "",
        lastname: "",
        patronymic: "",
        phone: "",
        roleId: "",
        salary: "",
      },
      teacherEditData: {
        firstname: "",
        lastname: "",
        patronymic: "",
        login: "",
        phone: "",
        salary: "",
        id: "",
        classIds: ""
      },
      classes: {},
      // Хранит в себе список Id классов, закрепленных за учителем. 
      classIds: [],
      // Содержит список тех Id классов, которые выбраны в чекбоксах.
      // Так же, если какие то классы уже закреплены за учителем,
      // они будут выделены изначально.
      selectedClasses: [],
      styles: {
        validationStyle: {
          color: "red",
        },
      },
    };
  },
  props: {
    teacherData: {
      type: Object,
    },
    teacherEditValidation: {
      type: Object,
    },
  },
  watch: {
    teacherEditValidation(newValue) {
      this.teacherEditValidation = newValue;
      this.getValidations(newValue);
    },
  },
  methods: {
    getValidations(validations) {
      this.teacherEditValidation = validations;
      for (let key in this.teacherEditValidation) {
        let validationKey = key.toLowerCase();
        for (let res of this.teacherEditValidation[key]) {
          this.validations[validationKey] = res;
        }
      }
    },
    cancelChanges() {
      this.$router.go({ name: "EditTeachers" });
    },
    applyChanges() {
      this.applySubmitted = true;
      // Обновляет валидацию при новой отправке.
      for (let prop in this.validations) {
        this.validations[prop] = "";
      }
      this.teacherEditData.id = this.teacherData.id;
      this.teacherEditData.firstname = this.teacherData.user.firstname;
      this.teacherEditData.lastname = this.teacherData.user.lastname;
      this.teacherEditData.patronymic = this.teacherData.user.patronymic;
      this.teacherEditData.login = this.teacherData.user.login;
      this.teacherEditData.phone = this.teacherData.user.phone;
      this.teacherEditData.salary = this.teacherData.salary;
      this.teacherEditData.classIds = this.selectedClasses;
      if (isNaN(this.teacherEditData.salary)) {
        this.validations.salary = "Недопустимый формат для зарплаты!";
      } else {
        this.$emit("teacherApplyChanges", this.teacherEditData);
      }
    },
    changePinnedClasses() {
        this.classChangeClicked = this.classChangeClicked == false ? true : false
        this.getClassesByTeacherId();
    },
    getClassesByTeacherId() {
        const currentUser = authenticationService.currentUserValue || {};
        const authHeader = currentUser.token ? { 'Authorization': 'Bearer ' + currentUser.token } : {}
        axios.get('https://localhost:44303/api/TeachersEdit/GetPinnedClassesByTeacherId', {
            params: {
                teacherId: this.teacherData.id
            },
            headers: {
                ...authHeader
            }
        })
        .then(response => {
            let data = response.data;
            for(let prop in data) {
                this.selectedClasses.push(data[prop].classId);
            }
        })
    }
  },
  created() {
    classService.GetAllClasses().then((classes) => (this.classes = classes));
  },
};
</script>