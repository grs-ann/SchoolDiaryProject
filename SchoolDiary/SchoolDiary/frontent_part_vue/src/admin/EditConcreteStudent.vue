<template>
    <div>
        <h3>Изменение ученика</h3>
        <div>
            <label>Логин</label>
            <input type="text" placeholder="Логин" v-model="studentData.user.login"><br/>
            <label>Имя</label>
            <input type="text" placeholder="Имя" v-model="studentData.user.firstname"><br/>
            <label>Фамилия</label>
            <input type="text" placeholder="Фамилия" v-model="studentData.user.lastname"><br/>
            <label>Отчество</label>
            <input type="text" placeholder="Отчество" v-model="studentData.user.patronymic"><br/>
            <label>Телефон</label>
            <input type="text" placeholder="Номер телефона" v-model="studentData.user.phone"><br/>
             
            <button @click="applyChanges">Принять изменения</button>
            <button>Отменить изменения</button>

        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
        }
    },
    props: {
        studentData: {
            type: Object
        }
    },
    methods: {
        applyChanges() {
            this.$emit('studentApplyChanges', this.studentData);
        }
    }
}
</script>