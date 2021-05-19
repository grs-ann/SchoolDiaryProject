<template>
    <div>
        <date-picker v-model="time" range></date-picker>
    </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

export default {
    components: {
        DatePicker
    },
    data() {
        return {
            time: null
        }
    },
    created() {
        this.getInitialDataForCalendar();
    },
    watch: {
        time(newV) {
            this.getTime();
        }
    },
    methods: {
        getTime() {
            this.firstDate = this.time[0];
            this.lastDate = this.time[1];
            this.$emit('dateChange', this.time);
        },
        getInitialDataForCalendar() {
            let now = new Date();
            let day = now.getDate();
            let month = now.getMonth();
            let year = now.getFullYear();
            let result = day + '/' + month + '/' + year;
            //this.initial = result;
        }
    }
}
</script>