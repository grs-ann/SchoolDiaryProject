<template>
    <div>
        <div>
            <select v-model="selectedSubject">
                <option v-for="(subject, index) in pinnedSubjects" v-bind:value="subject">{{ subject.title }}</option>
            </select>
        </div>
        {{ selectedSubject }}
        <div v-for="(cl, index) in this.concreteClassData.students">
            {{ index + 1 }})
            {{ cl.user.lastname }}
            {{ cl.user.firstname }}
            <button @click="showMarks(cl.id)">Показать оценки</button>
        </div>
    </div>
</template>

<script>
import { teacherManagementService, authenticationService } from "@/_services";
import axios from 'axios';


export default {
    data() {
        return {
            students: [],
            selectedSubject: {},

            studentMarks: {}
        }
    },
    props: {
        concreteClassData: {
            type: Object
        },
        pinnedSubjects: {
            type: Array
        }
    },
    created() {
    },
    methods: {
        showMarks(studentId) {
            const currentUser = authenticationService.currentUserValue || {};
            const authHeader = currentUser.token ? { 'Authorization': 'Bearer ' + currentUser.token } : {}

            console.log(this.studentMarksModel);
            axios.get('https://localhost:44303/api/Teacher/GetStudentMarks', {
                params: {
                    studentId: studentId,
                    subjectId: this.selectedSubject.id
                },
                headers: {
                    ...authHeader
                }
            })
            .then(response => {
                console.log(response.data)
            })},
    }   
}
</script>