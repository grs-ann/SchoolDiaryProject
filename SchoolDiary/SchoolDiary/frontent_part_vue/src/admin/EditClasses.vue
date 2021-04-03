<template>
    <div class="class-page">
        <p>Редактирование классов</p>
        <div>
            <div>
                <input type="text"/>
                <button v-on:click="">Добавить</button>
            </div>
            <ul v-if="this.classes.length">
                <li v-for="(cl, index) in classes" :key="cl.id">
                    {{cl.name}}
                    <button v-on:click="getClass(index, cl)">Изменить</button>
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
            classes: [],
            concreteClass: {
                id: "",
                name: ""
            },
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
        },
        getClass(index, cl) {
            this.concreteClass.id = index + 1;
            this.concreteClass.name = cl.name;
            console.log(this.concreteClass.id + " " + this.concreteClass.name);
            classService.GetClass(this.concreteClass)
                .then(concreteClassResult => this.concreteClassResult = this.concreteClassResult);
            
        }
        addNewClass(res) {
            //classService.AddNewClass(res)
        }
    }
};
</script>