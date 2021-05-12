<template>
    <div>
        <h1>Управление закрепленными классами.</h1>
        <concretePinnedClass
            v-show="concretePinnedClassesVisibility"

            v-for="(cl, index) in classes"
            :key="index"
            :index="index"
            :classData="cl"
            @concreteClassManageClickedStatusChange='concretePinnedClassesVisibilityChange'
        />
        <concreteClassWithStudents
            v-show="!concretePinnedClassesVisibility"
            :concreteClassData="concreteClassDataToManage"
        />
    </div>
</template>

<script>
import axios from 'axios';
import { teacherService, classService } from "@/_services";
import { authenticationService } from '@/_services';

// Импорт дочерних компонентов.
import concretePinnedClass from './ConcretePinnedClasses';
import concreteClassWithStudents from './ConcreteClassWithStudents';

export default {
    data() {
        return {
            classes: [],
            concretePinnedClassesVisibility: true,
            concreteClassDataToManage: {}
        }
    },
    components: {
        concretePinnedClass,
        concreteClassWithStudents
    },
    created() {
        this.getPinnedClasses();
    },
    methods: {
        getPinnedClasses() {
        const currentUser = authenticationService.currentUserValue || {};
        const authHeader = currentUser.token ? { 'Authorization': 'Bearer ' + currentUser.token } : {}
        axios.get('https://localhost:44303/api/Teacher/GetPinnedClasses', {
            params: {
                teacherId: currentUser.id
            },
            headers: {
                ...authHeader
            }
        })
        .then(response => {
            this.classes = response.data;
        })},
        concretePinnedClassesVisibilityChange(data) {
            this.concretePinnedClassesVisibility = this.concretePinnedClassesVisibility == false ? true : false;
            this.concreteClassDataToManage = data;
        }
    }
}
</script>