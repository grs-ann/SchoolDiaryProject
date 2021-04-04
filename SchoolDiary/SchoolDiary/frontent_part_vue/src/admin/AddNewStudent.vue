<template>
    <div>
        <p1>Добавление нового ученика</p1>
        <form @submit.prevent="onSubmit">
            <div class="form-group">
                <label for="username">Username</label>
                <input type="text" v-model.trim="$v.username.$model" name="username" class="form-control" :class="{ 'is-invalid': submitted && $v.username.$error }" />
                <div v-if="submitted && !$v.username.required" class="invalid-feedback">Username is required</div>
            </div>
            <div class="form-group">
                <label htmlFor="password">Password</label>
                <input type="password" v-model.trim="$v.password.$model" name="password" class="form-control" :class="{ 'is-invalid': submitted && $v.password.$error }" />
                <div v-if="submitted && !$v.password.required" class="invalid-feedback">Password is required</div>
            </div>
            <div class="form-group">
                <button class="btn btn-primary" :disabled="loading">
                    <span class="spinner-border spinner-border-sm" v-show="loading"></span>
                    <span>Login</span>
                </button>
            </div>
            <div v-if="error" class="alert alert-danger">{{error}}</div>
        </form>
    </div>
</template>

<script>
export default {
    data () {
        return {
            login: '',
            password: '',
            firstname: '',
            lastname: '',
            patronymic: '',
            phone: '',
            roleid: ''
        };
    },
    validations: {
      username: { required },
      password: { required }
    },
}
</script>
