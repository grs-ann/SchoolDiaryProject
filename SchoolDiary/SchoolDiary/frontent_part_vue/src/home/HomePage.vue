<template>
    <div>
        <p>Добро пожаловать, {{ currentUser.firstname }} {{ currentUser.lastname }}</p>
        <p>Статус: <strong>{{currentUser.role.name}}</strong>.</p>
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