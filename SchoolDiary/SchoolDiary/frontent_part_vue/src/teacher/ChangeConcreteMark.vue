<template>
    <div>
        <h3>Изменение оценки</h3>
        <p>Пожалуйста, выберите новую оценку:</p>
        <select v-model="changingMarkData.selectedMarkId">
            <option v-for="(mark, index) in markTypes" :value="mark.id" :key="index">{{ mark.name }}</option>
        </select>
        <p>Пожалуйста, выберите дату изменяемой оценки:</p>
        <div>
            <date-picker v-model="changingMarkData.dateTime" datetime></date-picker>
        </div>
        <button @click="applyChanges">Подтвердить изменения</button>
        {{ changingMarkData.dateTime }}
    </div>
</template>

<script>
import { markService } from "@/_services";
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

export default {
    components: {
        DatePicker
    },
    data() {
        return {
            changingMarkData: {
                selectedMarkId: null,
                dateTime: null,
                concreteMarkId: this.concreteMark.id
            },
            markTypes: null,
        }
    },
    props: {
        concreteMark: {
            type: Object
        }
    },
    created() {
        this.getAllMarkTypes();
        this.getOldMarkDate();
    },
    methods: {
        getAllMarkTypes() {
            markService.getAllMarks()
                .then(res => this.markTypes = res);
        },
        applyChanges() {
            this.$emit('markChanging', this.changingMarkData);
        },
        getOldMarkDate() {
            let oldDate = this.concreteMark.gradeDate;
            this.changingMarkData.dateTime = new Date(oldDate.split('T')[0]);
        }
    }
}
</script>