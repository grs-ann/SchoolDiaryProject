import Vue from 'vue';
import Vuelidate from 'vuelidate';

import { router } from './_helpers';
import App from './app/App';
//import TestApp from './_test/TestApp';
//import AppTest from './_test2/AppTest';
Vue.use(Vuelidate);



new Vue({
    el: '#app',
    router,
    render: h => h(App)
});

