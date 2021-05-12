<template>
    <div>
        <!-- <button @click="gg">gg</button> -->
        <div v-for="(cl, index) in this.concreteClassData.students">
            {{ index + 1 }})
            {{ cl.user.lastname }}
            {{ cl.user.firstname }}
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            students: []
        }
    },
    props: {
        concreteClassData: {
            type: Object
        }
    },
    created() {
    },
    methods: {
    }
}
</script>