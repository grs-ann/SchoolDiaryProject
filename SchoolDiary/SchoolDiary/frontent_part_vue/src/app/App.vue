<template>
    <div>
        <nav v-if="currentUser" class="navbar navbar-expand navbar-dark bg-dark">
            <div class="navbar-nav">
                <router-link to="/" class="nav-item nav-link">Главная</router-link>
                <router-link v-if="isAdmin" to="/editschedule" class="nav-item nav-link">Редактировать расписание</router-link>
                <router-link v-if="isAdmin" to="/admin" class="nav-item nav-link">Admin panel</router-link>
                <router-link v-if="isAdmin" to="/editclasses" class="nav-item nav-link">Редактировать классы</router-link>
                <router-link v-if="isAdmin" to="/editteachers" class="nav-item nav-link">Редактировать учителей</router-link>
                <router-link v-if="isAdmin" to="/editstudents" class="nav-item nav-link">Редактировать учеников</router-link>
                
                <a @click="logout" class="nav-item nav-link">Выйти из аккаунта</a>
            </div>
        </nav>
        <div class="jumbotron">
            <div class="container">
                <div class="row">
                    <div class="col-sm-6 offset-sm-3">
                        <router-view></router-view>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { authenticationService } from '@/_services';
import { router, Role } from '@/_helpers';

export default {
    name: 'app',
    data () {
        return {
            currentUser: null
        };
    },
    computed: {
        isAdmin () {
            return this.currentUser && this.currentUser.role.name === Role.Admin;
        }
    },
    created () {
        authenticationService.currentUser.subscribe(x => this.currentUser = x);
    },
    methods: {
        logout () {
            authenticationService.logout();
            router.push('/login');
        }
    }
};
</script>