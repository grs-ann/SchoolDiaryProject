<template>
    <div>
        <h1>Добавление нового учителя</h1>
        <div>
            <p v-if="submitted" :style="validationStyle">{{ validations.login }}</p>
            <input type="text" placeholder="Логин" v-model="newUserData.login"><br/>
            <p v-if="submitted" :style="validationStyle">{{ validations.password }}</p>
            <input type="password" placeholder="Пароль" v-model="newUserData.password"><br/>
            <p v-if="submitted" :style="validationStyle">{{ validations.firstname }}</p>
            <input type="text" placeholder="Имя" v-model="newUserData.firstname"><br/>
            <p v-if="submitted" :style="validationStyle">{{ validations.lastname }}</p>
            <input type="text" placeholder="Фамилия" v-model="newUserData.lastname"><br/>
            <p v-if="submitted" :style="validationStyle">{{ validations.patronymic }}</p>
            <input type="text" placeholder="Отчество" v-model="newUserData.patronymic"><br/>
            <p v-if="submitted" :style="validationStyle">{{ validations.phone }}</p>
            <input type="text" placeholder="Номер телефона" v-model="newUserData.phone"><br/>
            <p v-if="submitted" :style="validationStyle">{{ validations.salary }}</p>
            <input type="text" placeholder="Зарплата" v-model="newUserData.salary"><br/>

            <button @click="addNewTeacher">Добавить</button>
        </div>
    </div>
</template>
<script>
import { teacherService } from '@/_services';


export default {
    data() {
        return {
            submitted: false,
            newUserData: {
                login: '',
                password: '',
                firstname: '',
                lastname: '',
                patronymic: '',
                phone: '',
                roleId: 3,
                salary: ''
            },
            validations: {
                login: '',
                password: '',
                firstname: '',
                lastname: '',
                patronymic: '',
                phone: '',
                roleId: '',
                salary: ''
            },
            validationStyle: {
                color: 'red'
            }
        }
    },
    methods: {
        addNewTeacher() {
            // Обновляет все предыдущие сообщения валидации.
            for(let prop in this.validations) {
                this.validations[prop] = '';
            }
            this.submitted = true;
            if (isNaN(this.newUserData.salary)) {
                this.validations.salary = "Недопустимый формат для зарплаты!"
            } else {
                teacherService.registerNewTeacher(this.newUserData)
                .then(result => this.$router.push('/editteachers'))
                .catch(err => {
                    for(let key in err) {
                        let validationKey = key.toLowerCase();
                        for(let res of err[key]) {
                            this.validations[validationKey] = res;
                        }
                    }
                })
            }
        }
    }
}
</script>