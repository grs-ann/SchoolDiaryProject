<template>
    <div>
        <p1>Редактирование учеников</p1>
        <router-link v-if="isAdmin" to="/addnewstudent" class="nav-item nav-link">Добавить нового ученика</router-link>
    </div>
</template>

<script>
import app from '@/app/App'
export default {
    name: 'editStudents',
    data () {
        return {
            currentUser: null
        };
    },
    methods: {
        isAdmin() {
            return app.isAdmin();
        }
    }
};
</script>