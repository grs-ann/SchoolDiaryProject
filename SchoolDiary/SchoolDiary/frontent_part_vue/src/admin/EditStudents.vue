<template>
    <div>
        <p>Редактирование учеников</p>
        <router-link v-if="isAdmin" to="/addnewstudent" class="nav-item nav-link">Добавить нового ученика</router-link>
        <ul>
            <li v-for="(student, index) in students" :key="student.id">
                {{ student.user.lastname }} {{ student.user.firstname }} {{ student.user.patronymic }} - {{ student.class.name }}
                <button class="btn-default">Редактировать</button>
                <button class="btn-danger" v-on:click="deleteUser(student.user.id)">Удалить</button>
                <p> {{ student }} </p>
            </li>
        </ul>
    </div>
</template>

<script>
import app from '@/app/App'
import { authenticationService, userService } from '@/_services';
export default {
    name: 'editStudents',
    data () {
        return {
            students: [],
            id: '',
        };
    },
    created () {
        this.getStudents()
    },
    watch: {
        
    },
    methods: {
        isAdmin() {
            return app.isAdmin();
        },
        deleteUser(id) {
            this.id = id;
            let indexForDelete = this.students.findIndex(item => item.userId == this.id)
            this.students.splice(indexForDelete, 1);
            userService.deleteStudentById(this.id);
        },
        getStudents() {
            userService.getAllStudents().then(res => this.students = res);
        }
    }
};
</script>