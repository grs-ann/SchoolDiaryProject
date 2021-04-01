<template>
    <div class="home-page">
        <h1>School diary home page</h1>
        <p>Добро пожаловать, <strong>{{ currentUser.firstname }}</strong></p>
        <p>Your role is: <strong>{{currentUser.role.name}}</strong>.</p>
        <p>This page can be accessed by all authenticated users.</p>
    </div>
</template>

<script>
import { userService, authenticationService } from '@/_services';

export default {
    data () {
        return {
            currentUser: authenticationService.currentUserValue,
            userFromApi: null
        };
    },
    created () {
        userService.getById(this.currentUser.id).then(user => this.userFromApi = user);
    }
};
</script>

