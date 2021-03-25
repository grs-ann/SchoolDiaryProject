<template>
    <div class="about">
        <form>
            <label>Firstname</label>
            <input type="text" v-model="firstname" placeholder="Enter your firstname"><br>
            <label>Lastname</label>
            <input type="text" v-model="lastname" placeholder="Enter your lastname"><br>

            <button @click="onSend">Send</button>
        </form>
    </div>
</template>


<script>
import axios from 'axios';
import query from 'querystring';

export default {
    name: "about",
    data: () => ({
        entryTask: {
            firstname: "",
            lastname: ""
        }
    }),
    methods: {
        onSend() {
            const _self = this;
            let config = {
                headers: {"Content-Type" : "application/json" }
            };
            this.entryTask.firstname = _self.firstname;
            this.entryTask.lastname = _self.lastname;
            var i = query.stringify(_self.entryTask);
            const _api = axios.post("https://localhost:44303/api/vuetest/userinfo", i, config);
            _api
            .then(response => {
                console.log(response.data);
                alert("Success");
            })
            .catch(err => {
                alert("Error! " + err);
            })
        }
    }
};
</script>