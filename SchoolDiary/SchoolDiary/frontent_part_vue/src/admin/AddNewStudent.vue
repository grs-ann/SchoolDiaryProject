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
            <select id="classId" v-model="selectedClassId">
                <option :value="cl.value" v-for="(cl, index) in classes" :key="index">
                    {{ cl.name }}
                </option>
            </select><br/>
            <button>Добавить</button>
        </form>
    </div>
</template>


<script>
import { classService, userService } from '@/_services';


export default {
    name: "AddingNewStudent",
    
    data() {
        return {
            selectedClassId:'',
            classes: [],
            newUserData: {
                login: '',
                password: '',
                firstname: '',
                lastname: '',
                patronymic: '',
                phone: '',
                roleId: 2,
                classId: ''
            }
        }
    },
    created () {
        classService.GetAllClasses().then(res => this.classes = res);
    },
    methods: {
        gg() {
            console.log('gg');
        },
        addNewStudent() {
            console.log(this.newUserData)
            this.newUserData.classId = this.selectedClassId;
            userService.registerNewStudent(this.newUserData);
        }
    }
}

</script>