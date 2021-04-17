<template>
    <div>
        <p>Изменение ученика</p>
        {{ oldUserData }}
    </div>
</template>

<script>
export default {
    data() {
        return {
        }
    },
    props: {
        oldUserData: {
            type: Object,
        }
    }
}
</script>