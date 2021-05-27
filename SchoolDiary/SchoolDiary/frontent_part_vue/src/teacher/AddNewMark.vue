<template>
  <div>
    <h3>Выберите оценку и дату выставления:</h3>
    <select v-model="addingMarkData.selectedMarkId">
      <option v-for="(mark, index) in markTypes" :value="mark.id" :key="index">
        {{ mark.name }}
      </option>
    </select>
    <date-picker v-model="addingMarkData.markTime" datetime></date-picker>
    <button @click="addMark">Добавить оценку</button>
  </div>
</template>

<script>
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";
import { markService } from "@/_services";
export default {
  components: {
    DatePicker,
  },
  data() {
    return {
      addingMarkData: {
        markTime: new Date(),
        selectedMarkId: null,
        subjectId: this.marks[0].subjectId,
        studentId: this.marks[0].studentId,
      },
      markTypes: null,
    };
  },
  props: ["marks"],
  created() {
    this.getMarkTypes();
  },
  methods: {
    getMarkTypes() {
      markService.getAllMarks().then((res) => (this.markTypes = res));
    },
    addMark() {
      markService
        .addNewMark(this.addingMarkData)
        .catch((err) => console.log(err));
    },
  },
};
</script>
