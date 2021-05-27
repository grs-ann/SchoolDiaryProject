<template>
    <div>
        <div v-if="!addNewMarkClicked">
            <button @click="addNewMarkClick">Добавить новую оценку</button>
        </div>
        <div v-if="!changeConcreteMarkClicked && !addNewMarkClicked">
            <table>
                <tr>
                    <th>Номер</th>
                    <th>Оценка</th>
                    <th>Дата выставления</th>
                </tr>
                <tr v-for="(grade, index) in grades" :key="index">
                    <th>{{ index + 1 }}.</th>
                    <th>
                        {{ grade.mark.name }}
                        <button @click="changeConcreteMark(grade)">Изменить</button>
                    </th>
                    <th>{{ grade.gradeDate }}</th>
                </tr>
            </table>
        </div>
        <div v-if="changeConcreteMarkClicked && !addNewMarkClicked">
            <ChangeConcreteMark
                :concreteMark="concreteMark"
                @markChanging="changeMarkInDB"
            />
        </div>
    </div>
</template>

<script>
import ChangeConcreteMark from './ChangeConcreteMark';
import { markService } from '@/_services';

export default {
    components: {
        ChangeConcreteMark
    },
    data() {
        return {
            marks: null,
            changeConcreteMarkClicked: false,
            addNewMarkClicked: false,
            concreteMark: null,
        }
    },
    props: {
        grades: {
            type: Array
        }
    },
    methods: {
        changeConcreteMark(mark) {
            this.changeConcreteMarkClicked = true;
            this.concreteMark = mark;
        },
        changeMarkInDB(changingMarkData) {
            this.changeConcreteMarkClicked = false;
            markService.changeMark(changingMarkData)
                .catch(err => console.log(err))
        },
        addNewMarkClick() {
            this.addNewMarkClicked = this.addNewMarkClicked == false ? true : false;
        }
    }
}
</script>

<style>
table {
    border-collapse: collapse;
    border: 2px solid rgb(200,200,200);
    letter-spacing: 1px;
    font-size: 0.8rem;
    text-align: center;
}
td, th {
    border: 1px solid rgb(190,190,190);
    padding: 10px 20px;
}
</style>