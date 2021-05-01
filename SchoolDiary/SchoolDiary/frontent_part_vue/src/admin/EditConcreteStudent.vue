<template>
    <div>
        <h3>Изменение ученика</h3>
        <div>
            {{ studentEditValidation.login }}</br>
            <p :style="styleObject" v-if="applySubmitted">{{ validations.login }}</p>
            <label>Логин</label>
            <input type="text" placeholder="Логин" v-model="studentData.user.login"><br/>
            <p :style="styleObject" v-if="applySubmitted">{{ validations.firstname }}</p>
            <label>Имя</label>
            <input type="text" placeholder="Имя" v-model="studentData.user.firstname"><br/>
            <p :style="styleObject" v-if="applySubmitted">{{ validations.lastname }}</p>
            <label>Фамилия</label>
            <input type="text" placeholder="Фамилия" v-model="studentData.user.lastname"><br/>
            <p :style="styleObject" v-if="applySubmitted">{{ validations.patronymic }}</p>
            <label>Отчество</label>
            <input type="text" placeholder="Отчество" v-model="studentData.user.patronymic"><br/>
            <p :style="styleObject" v-if="applySubmitted">{{ validations.phone }}</p>
            <label>Телефон</label>
            <input type="text" placeholder="Номер телефона" v-model="studentData.user.phone"><br/>

            <button @click="applyChanges">Принять изменения</button>
            <button @click="cancelChanges">Отменить изменения</button>

        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            applySubmitted: false,
            studentEditData: {
                firstname: '',
                lastname: '',
                patronymic: '',
                login: '',
                phone: '',
                classId: '',
                id: ''
            },
            validations: {
                login: '',
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
        }
    },
    props: {
        studentData: {
            type: Object
        },
        studentEditValidation: {
            type: Object
        }
    },
    watch: {
        studentEditValidation(newV) {
            this.studentEditValidation = newV;
            this.getValidations(newV);
        }
    },
    methods: {
        /**
         * Изменяет данные об ученике.
         */
        applyChanges() {
            this.applySubmitted = true;
            // Refresh validation properties.
            for(let prop in this.studentEditValidation) {
                this.studentEditValidation[prop] = '';
            }
            this.studentEditData.id = this.studentData.id;
            this.studentEditData.firstname = this.studentData.user.firstname;
            this.studentEditData.lastname = this.studentData.user.lastname;
            this.studentEditData.patronymic = this.studentData.user.patronymic;
            this.studentEditData.login = this.studentData.user.login;
            this.studentEditData.phone = this.studentData.user.phone;
            this.studentEditData.classId = this.studentData.classId;
            this.$emit('studentApplyChanges', this.studentEditData);
        },
        cancelChanges() {
            this.$router.go({ name: 'EditStudents' });
        },
        getValidations(validations) {
            this.studentEditValidation = validations;
            for(let key in this.studentEditValidation) {
                let validationKey = key.toLowerCase();
                for(let res of this.studentEditValidation[key]) {
                    this.validations[validationKey] = res
                }
            }
        }
    }
}
</script>