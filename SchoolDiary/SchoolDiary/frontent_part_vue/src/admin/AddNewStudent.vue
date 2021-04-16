<template>
    <div v-bind:style="styleForm">
        <p>Добавление нового ученика</p>
        <form @submit.prevent="addNewStudent">
            <div v-bind:style="styleObject">
                <p v-if="newUserData.submitted">{{ validations.login }}</p>
                <input type="text" placeholder="Логин" v-model="newUserData.login"><br/>
                <p v-if="newUserData.submitted">{{ validations.password }}</p>
                <input type="password" placeholder="Пароль" v-model="newUserData.password"><br/>
                <p v-if="newUserData.submitted">{{ validations.firstname }}</p>
                <input type="text" placeholder="Имя" v-model="newUserData.firstname"><br/>
                <p v-if="newUserData.submitted">{{ validations.lastname }}</p>
                <input type="text" placeholder="Фамилия" v-model="newUserData.lastname"><br/>
                <p v-if="newUserData.submitted">{{ validations.patronymic }}</p>
                <input type="text" placeholder="Отчество" v-model="newUserData.patronymic"><br/>
                <p v-if="newUserData.submitted">{{ validations.phone }}</p>
                <input type="text" placeholder="Номер телефона" v-model="newUserData.phone"><br/>
                <p v-if="newUserData.submitted && newUserData.classId < 1">{{ validations.classId }}</p>
                <select v-model="newUserData.classId">
                    <option disabled value=''>Выберите один из вариантов</option>
                    <option v-for="option in classes" v-bind:value="option.id">
                        {{ option.name }}
                    </option>
                </select>
            </div>
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
            classes: [],
            newUserData: {
                login: '',
                password: '',
                firstname: '',
                lastname: '',
                patronymic: '',
                phone: '',
                roleId: 2,
                classId: 0,
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
            styleObject: {
                color: 'red'
            },
            styleForm: {
                'text-align': 'center'
            }
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
            // Refresh validation properties.
            for(let prop in this.validations) {
                this.validations[prop] = '';
            }
            this.newUserData.submitted = true;
            userService.registerNewStudent(this.newUserData)
                .then(result => router.push('/editstudents'))
                .catch(err => {
                    for(let key in err) {
                        let validationKey = key.toLowerCase();
                        for(let res of err[key]) {
                            this.validations[validationKey] = res;
                        }
                        if (this.newUserData.classId == 0) {
                            this.validations['classId'] = "Пожалуйста, выберите класс для ученика."
                        }
                    }
                })
        }
    }
}
</script>

