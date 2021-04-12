<template>
    <div class="add-new-student">
        <p>Добавление нового ученика</p>
        <form class="add-new-student-form" @submit.prevent="addNewStudent">
            <input type="text" placeholder="Логин" v-model="newUserData.login"><br/>
            <input type="password" placeholder="Пароль" v-model="newUserData.password"><br/>
            <input type="text" placeholder="Имя" v-model="newUserData.firstname"><br/>
            <input type="text" placeholder="Фамилия" v-model="newUserData.lastname"><br/>
            <input type="text" placeholder="Отчество" v-model="newUserData.patronymic"><br/>
            <input type="text" placeholder="Номер телефона" v-model="newUserData.phone"><br/>
            <select v-model="selectedClassId">
                <option disabled value="">Выберите один из вариантов</option>
                <option v-for="option in classes" v-bind:value="option.id">
                    {{ option.name }}
                </option>
            </select>
            <span>Выбрано: {{ this.selectedClassId }}</span><br/>
            <button>Добавить</button>
        </form>
    </div>
</template>


<script>
import { classService, userService } from '@/_services';
import { router } from '@/_helpers';

export default {
    name: "AddingNewStudent",
    data() {
        return {
            selectedClassId: '',
            classes: [],
            newUserData: {
                login: 'g',
                password: '',
                firstname: '',
                lastname: '',
                patronymic: '',
                phone: '',
                roleId: 2,
                classId: '',
                submitted: false
            }
        }
    },
    validations: {
        login: '',
        password: '',
        firstname: '',
        lastname: '',
        patronymic: '',
        phone: '',
        roleId: '',
        classId: '',
    },
    created () {
        classService.GetAllClasses().then(res => this.classes = res);
    },
    /**
     * Registers a new student with inputed data.
     */
    methods: {
        addNewStudent() {
            this.newUserData.submitted = true;
            if (this.selectedClassId !== "") {
                this.newUserData.classId = this.selectedClassId;
                userService.registerNewStudent(this.newUserData)
                    .then(result => router.push('/editstudents'))
                    .catch(err => console.log(err));
            }
        }
    }
}
</script>