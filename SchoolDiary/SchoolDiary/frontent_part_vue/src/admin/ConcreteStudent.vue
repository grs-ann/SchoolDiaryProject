<template>
    <div>
        <ul v-bind:style="styles.listStyle">
            <li>
                {{ userIndex }})
                {{ studentData.user.firstname }}
                {{ studentData.user.lastname }} - 
                {{ studentData.class.name }}
                <button @click="editStudent(studentData)">Редактировать</button>
                <button class="btn-danger" @click='del'>Удалить</button>
            </li>
        </ul>
    </div>
</template>

<script>
import editConcreteStudent from './EditConcreteStudent'
export default {
    components: {
        editConcreteStudent
    },
    data() {
        return {
            //studentData: null,
            userIndex: this.index,
            userId: '',
            styles: {
                listStyle: {
                    'list-style-type': 'none'
                }
            }
        }
    },
    props: {
        studentData: {
            type: Object
        },
        index: {
            type: Number,
        }
    },
    methods: {
        del() {
            this.userId = this.studentData.user.id
            console.log('deleting!', this.userId);
            // Создаем эмит(событие), далее его может слушать родительский компонент.
            this.$emit('onDelete', this.userId)
        },
        editStudent(data) {
            this.$emit('concreteStudentEdit', {
                data: this.studentData,
                clickedStatus: true
            });
        }
    }
}
</script>

