<template>
    <div>
        <p>Редактирование учеников</p>
        <router-link v-if="isAdmin" to="/addnewstudent" class="nav-item nav-link">Добавить нового ученика</router-link>
        <ul v-bind:style="styles.listStyle">
            <li v-for="(student, index) in students" :key="student.id">
                {{ ++index }})
                {{ student.user.lastname }} {{ student.user.firstname }}
                {{ student.user.patronymic }} - {{ student.class.name }}
                <button class="btn-default" v-on:click="editConcreteStudent(student)">Редактировать</button>
                <button class="btn-danger" v-on:click="deleteUser(student.user.id)" >Удалить</button>
                <p>*****</p>
            </li>
        </ul>
    </div>
</template>

<script>
import app from '@/app/App'
import { authenticationService, userService } from '@/_services';
import { router } from '@/_helpers';
import EditConcreteStudent from './EditConcreteStudent';
export default {
    name: 'editStudents',
    components: {
        EditConcreteStudent
    },
    data () {
        return {
            students: [],
            id: '',
            styles: {
                listStyle: {
                    'list-style-type': 'none'
                }
            }
        };
    },
    created () {
        this.getStudents()
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
        },
        editConcreteStudent(studentData) {
            //oldUserData
            router.push({ path: 'editconcretestudent'});
        }
    }
};
</script>