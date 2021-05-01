<template>
    <div>
        <ul :style="styles.listStyle">
            <li>
                {{ index + 1 }})
                {{ teacherData.user.firstname }}
                {{ teacherData.user.lastname }}
                <button @click="editTeacher">Редактировать</button>
                <button class="btn-danger" @click='del'>Удалить</button>
            </li>
        </ul>
    </div>
</template>

<script>
export default {
    data() {
        return {
            styles: {
                listStyle: {
                    'list-style-type': 'none'
                }
            }
        }
    },
    props: {
        teacherData: {
            type: Object
        },
        index: {
            type: Number
        }
    },
    methods: {
        del() {
            let userId = this.teacherData.user.id;
            this.$emit('onDelete', userId);
        },
        editTeacher() {
            this.$emit('concreteTeacherEdit', {
                teacherEditData: this.teacherData,
                clickedStatus: true
            });
        }
    }
    
}
</script>