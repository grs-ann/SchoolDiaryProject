<template>
    <div class="add-new-student">
        <p>Добавление нового ученика</p>
        <form class="add-new-student-form" @submit.prevent="addNewStudent">
            <p v-if="newUserData.submitted">{{ validations.login }}</p>
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
                login: '',
                password: '',
                firstname: '',
                lastname: '',
                patronymic: '',
                phone: '',
                roleId: 2,
                classId: '',
                submitted: false
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
            validationErrors: ''
        }
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
            let error = [];
            if (this.selectedClassId !== "") {
                this.newUserData.classId = this.selectedClassId;
                userService.registerNewStudent(this.newUserData)
                    .then(result => router.push('/editstudents'))
                    // .catch(function(err) {
                    //     console.log(this.newUserData);
                    //     for(let key in err) {
                    //         let validationKey = key.toLowerCase();
                    //         for(let res of err[key]) {

                    //         }
                    //     }
                    // });
                    .catch(err => {
                        for(let key in err) {
                            let validationKey = key.toLowerCase();
                            for(let res of err[key]) {
                                this.validations[validationKey] = res;
                            }
                        }
                    })
            }
            console.log(this.validations);
        }
    }
}
</script>