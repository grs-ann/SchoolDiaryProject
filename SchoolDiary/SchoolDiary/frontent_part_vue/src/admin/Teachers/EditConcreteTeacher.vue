<template>
    <div>
        <p :style="styles.validationStyle" v-if="applySubmitted">{{ validations.login }}</p>
        <label>Логин</label>
        <input type="text" placeholder="Логин" v-model="teacherData.user.login"><br/>
        <p :style="styles.validationStyle" v-if="applySubmitted">{{ validations.firstname }}</p>
        <label>Имя</label>
        <input type="text" placeholder="Имя" v-model="teacherData.user.firstname"><br/>
        <p :style="styles.validationStyle" v-if="applySubmitted">{{ validations.lastname }}</p>
        <label>Фамилия</label>
        <input type="text" placeholder="Фамилия" v-model="teacherData.user.lastname"><br/>
        <p :style="styles.validationStyle" v-if="applySubmitted">{{ validations.patronymic }}</p>
        <label>Отчество</label>
        <input type="text" placeholder="Отчество" v-model="teacherData.user.patronymic"><br/>
        <p :style="styles.validationStyle" v-if="applySubmitted">{{ validations.phone }}</p>
        <label>Телефон</label>
        <input type="text" placeholder="Номер телефона" v-model="teacherData.user.phone"><br/>
        
        <p :style="styles.validationStyle" v-if="applySubmitted">{{ validations.salary }}</p>
        <label>Зарплата</label>
        <input type="text" placeholder="Зарплата" v-model="teacherData.salary"><br/>
        

        <button @click="applyChanges">Принять изменения</button>
        <button @click="cancelChanges">Отменить изменения</button>
    </div>
</template>

<script>
export default {
    data() {
        return {
            applySubmitted: false,
            validations: {
                login: '',
                firstname: '',
                lastname: '',
                patronymic: '',
                phone: '',
                roleId: '',
                salary: ''
            },
            teacherEditData: {
                firstname: '',
                lastname: '',
                patronymic: '',
                login: '',
                phone: '',
                salary: '',
                id: ''
            },
            styles: {
                validationStyle: {
                    color: 'red'
                }
            }
        }
    },
    props: {
        teacherData: {
            type: Object
        },
        teacherEditValidation: {
            type: Object
        }
    },
    watch: {
        teacherEditValidation(newValue) {
            this.teacherEditValidation = newValue;
            this.getValidations(newValue)
        }
    },
    methods: {
        getValidations(validations) {
            this.teacherEditValidation = validations;
            for(let key in this.teacherEditValidation) {
                let validationKey = key.toLowerCase();
                for(let res of this.teacherEditValidation[key]) {
                    this.validations[validationKey] = res
                }
            }
        },
        cancelChanges() {
            this.$router.go({ name: 'EditTeachers' });
        },
        applyChanges() {
            this.applySubmitted = true;
            // Обновляет валидацию при новой отправке.
            for(let prop in this.validations) {
                this.validations[prop] = '';
            }
            this.teacherEditData.id = this.teacherData.id;
            this.teacherEditData.firstname = this.teacherData.user.firstname;
            this.teacherEditData.lastname = this.teacherData.user.lastname;
            this.teacherEditData.patronymic = this.teacherData.user.patronymic;
            this.teacherEditData.login = this.teacherData.user.login;
            this.teacherEditData.phone = this.teacherData.user.phone;
            this.teacherEditData.salary = this.teacherData.salary;
            if (isNaN(this.teacherEditData.salary)) {
                this.validations.salary = "Недопустимый формат для зарплаты!"
            } else {
                this.$emit('teacherApplyChanges', this.teacherEditData);
            }
            
        }
    }
}
</script>