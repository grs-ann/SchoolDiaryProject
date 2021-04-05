<template>
    <div>
        <p>Редактирование учеников</p>
        <router-link v-if="isAdmin" to="/addnewstudent" class="nav-item nav-link">Добавить нового ученика</router-link>
        <ul>
            <li v-for="student in students">
                {{ student.user.lastname }} {{ student.user.firstname }} {{ student.user.patronymic }} - {{ student.class.name }}
                <button class="btn-default">Редактировать</button>
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
            students: []
        };
    },
    created () {
        userService.getAllStudents().then(res => this.students = res);
    },
    methods: {
        isAdmin() {
            return app.isAdmin();
        }
    }
};
</script>