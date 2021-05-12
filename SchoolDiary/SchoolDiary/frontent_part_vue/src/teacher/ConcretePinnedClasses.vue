<template>
    <div>
        <div>
            {{ index + 1 }})
            {{ classData.name }}
            <button @click="concreteClassManageClickedStatusChange">Управление</button>
        </div>
    </div>
</template>

<script>

export default {
    data() {
        return {
            concreteClassManageClicked: false
        }
    },
    props: {
        classData: {
            type: Object
        },
        index: ''
    },
    methods: {
        concreteClassManageClickedStatusChange() {
            this.$emit('concreteClassManageClickedStatusChange', this.classData)
        }
    }
}
</script>