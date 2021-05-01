<template>
    <div>
        <h1>Редактирование учителей</h1>
        <editConcreteTeacher v-if="clickedStatus"
            :teacherData="teacherData"
            :teacherEditClicked="clickedStatus"
            :teacherEditValidation="teacherEditValidation"
            @teacherApplyChanges="concreteTeacherChange"
        />
        <div v-if="!clickedStatus">
            <div>
            <router-link to="/addnewteacher" class="nav-item nav-link">
            Добавить нового учителя</router-link>
            </div>
            <div>
                <concreteTeacher v-for="(teacher, index) in teachers"
                    :index="index"
                    :teacherData="teacher"
                    @onDelete="deleteUser"
                    @concreteTeacherEdit="teacherEdit"
                    :key="teacher.user.id"
                />
            </div>
        </div>
    </div>
    
</template>

<script>
import { teacherService, userService} from '@/_services';

import concreteTeacher from './ConcreteTeacher';
import editConcreteTeacher from './EditConcreteTeacher';
export default {
    components: {
        concreteTeacher,
        editConcreteTeacher
    },
    data() {
        return {
            teachers: [],
            // Если нажата клавиша редактирования учителя - примет статус true.
            clickedStatus: false,
            teacherData: {},
            teacherEditValidation: {}
        };
    },
    created() {
        this.getTeachers();
    },
    methods: {
        getTeachers() {
            teacherService.getAllTeachers()
                .then(res => this.teachers = res);
        },
        deleteUser(id) {
            let indexForDelete = this.teachers.findIndex(
                item => item.userId == id
            );
            userService.deleteStudentById(id)
                .then(() => this.teachers.splice(indexForDelete, 1));
        },
        concreteTeacherChange(teacherData) {
            teacherService.changeTeacher(teacherData)
                .then(result => this.$router.go({ name: 'EditTeachers' }))
                .catch(err => this.teacherEditValidation = err)
        },
        teacherEdit(data) {
            this.clickedStatus = data.clickedStatus;
            this.teacherData = data.teacherEditData;
        }
    }
}
</script>