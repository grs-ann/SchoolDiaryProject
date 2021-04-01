<template>
    <div class="class-page">
        <p>Редактирование классов</p>
        <div>
            <ul v-if="this.classes.length">
                <li v-for="cl in classes" :key="cl.id">
                    {{cl.name}}
                </li>
            </ul>
        </div>
    </div>
    
</template>


<script>
import { authenticationService, classService } from '@/_services';

export default {
    data () {
        return {
            user: authenticationService.currentUserValue,
            classes: []
        };
    },
    created () {
        //userService.getAll().then(users => this.users = users);
        classService.GetAllClasses().then(classes => this.classes = classes);
    },
    methods: {
        logout () {
            authenticationService.logout();
            router.push('/login');
        }
    }
};
</script>
