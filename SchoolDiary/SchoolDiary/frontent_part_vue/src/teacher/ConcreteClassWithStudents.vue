<template>
    <div>
        <div v-show="!studentMarksShowing">
            <div>
                <div>
                    <p v-bind:style="styles" v-show="clientValidations.dateErrorMessage">{{ clientValidations.date }}</p>
                    <Calendar
                        @dateChange="getChangedDate"
                    />
                </div>
                <div>
                    <p v-bind:style="styles" v-show="clientValidations.subjectErrorMessage">{{ clientValidations.subject }}</p>
                    <select v-model="selectedSubject">
                        <option v-for="(subject, index) in pinnedSubjects" v-bind:value="subject">{{ subject.title }}</option>
                    </select>
                </div>
            </div>
            <div v-for="(cl, index) in this.concreteClassData.students">
                {{ index + 1 }})
                {{ cl.user.lastname }}
                {{ cl.user.firstname }}
                <button @click="showMarks(cl.id)">Показать оценки</button>
            </div>
        </div>
        <div v-show="studentMarksShowing">
            <ConcreteStudentMarks
                :grades="studentMarks"
            />
        </div>
        <div v-if="studentMarksShowing">
            <AddNewMark
                :marks="studentMarks"
            />
        </div>
    </div>
</template>

<script>
import { teacherManagementService, authenticationService } from "@/_services";
import axios from 'axios';
import Calendar from './Calendar';
import ConcreteStudentMarks from './ConcreteStudentMarks';
import AddNewMark from './AddNewMark';
export default {
    components: {
        Calendar,
        ConcreteStudentMarks,
        AddNewMark
    },
    data() {
        return {
            students: [],
            selectedSubject: null,
            // Содержит в себе коллекцию оценок для выбранного студента.
            studentMarks: null,
            firstDate: null,
            lastDate: null,

            studentMarksShowing: false,
            clientValidations: {
                date: 'Необходимо выбрад период дат!',
                dateErrorMessage: false,
                subject: 'Необходимо выбрать предмет!',
                subjectErrorMessage: false
            },
            styles: {
                'color': 'red'
            }
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
    methods: {
        showMarks(studentId) {
            // todo: когда будет время, переписать это на нормальный лад.
            if (this.selectedSubject == null) {
                this.clientValidations.subjectErrorMessage = true;
            } else {
                this.clientValidations.subjectErrorMessage = false;
            }
            if (this.firstDate == null || this.lastDate == null) {
                this.clientValidations.dateErrorMessage = true;
            } else {
                this.clientValidations.dateErrorMessage = false;
            }
            if (this.clientValidations.dateErrorMessage == true ||
                this.clientValidations.subjectErrorMessage == true) {
                return;
            }
            const currentUser = authenticationService.currentUserValue || {};
            const authHeader = currentUser.token ? { 'Authorization': 'Bearer ' + currentUser.token } : {}
            axios.get('https://localhost:44303/api/Teacher/GetStudentMarks', {
                params: {
                    studentId: studentId,
                    subjectId: this.selectedSubject.id,
                    firstDate: this.firstDate,
                    lastDate: this.lastDate
                },
                headers: {
                    ...authHeader
                }
            })
            .then(response => {
                this.studentMarks = response.data
                this.studentMarksShowing = true;
            })},
        getChangedDate(data) {
            this.firstDate = data[0];
            this.lastDate = data[1];
        }
    }   
}
</script>