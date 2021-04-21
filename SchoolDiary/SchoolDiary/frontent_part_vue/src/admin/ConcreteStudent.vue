<template>
    <div class="gg">
        <ul v-bind:style="styles.listStyle">
            <li>
                {{ userIndex }})
                {{ studentData.user.firstname }}
                {{ studentData.user.lastname }} - 
                {{ studentData.class.name }}
                <button @click="editStudent">Редактировать</button>
                <button class="btn-danger" @click='del'>Удалить</button>
            </li>
        </ul>
    </div>
</template>

<script>
import EditStudents from './EditStudents.vue'
export default {
    data() {
        return {
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
    // props: [
    //     'studentData',
    //     'index'
    // ],
    methods: {
        del() {
            this.userId = this.studentData.user.id
            console.log('deleting!', this.userId);
            // Создаем эмит(событие), далее его может слушать родительский компонент.
            this.$emit('onDelete', this.userId)
        },
        editStudent() {
            console.log(this.studentData);
            //this.$emit('onEdit', this.studentData);
            this.$router.push({ name: 'TEST' })
        }
    }
}
</script>

