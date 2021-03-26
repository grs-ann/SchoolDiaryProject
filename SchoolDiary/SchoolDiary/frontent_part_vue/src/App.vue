<template>
  <img alt="Vue logo" src="./assets/logo.png">
  <div class="App">
    <form class="test__ok" @submit.prevent="onSave">
      <label>Firstname</label>
      <input type="text" v-model="firstname" placeholder="enter firstname"/><br>
      <label>Lastname</label>
      <input type="text" v-model="lastname" placeholder="enter lastname"/><br>
 
      <button>Save</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'App',
  data() {
    return {
      entryTask: {
        firstname: "",
        lastname: ""
      }
    }
  },
  methods: {
    gg() {
      console.log("gg!!");
    },
    onSave() {
      const _self = this;
      let config = {
        headers: {"Content-Type" : "application/json"}
      };
      this.entryTask.firstname = _self.firstname;
      this.entryTask.lastname = _self.lastname;
      //var i = query.stringfy(_self.entryTask);
      const _api = axios.post("https://localhost:44303/api/vuetest/userinfo", _self.entryTask, config);
      _api
      .then(response => {
        console.log(response.data);
        alert("Оно пришло!!");
      })
      .catch(err => {
        alert("Error: " + err);
      })
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
