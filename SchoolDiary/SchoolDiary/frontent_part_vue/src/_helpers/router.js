import Vue from 'vue';
import Router from 'vue-router';

import { authenticationService } from '@/_services';
import { Role } from '@/_helpers';
import HomePage from '@/home/HomePage';
import AdminPage from '@/admin/AdminPage';
import LoginPage from '@/login/LoginPage';
import EditStudents from '@/admin/EditStudents';
import EditTeachers from '@/admin/EditTeachers';
import EditSchedule from '@/admin/EditSchedule';
import EditClasses from '@/admin/EditClasses';
import AddNewStudent from '@/admin/AddNewStudent';
import EditConcreteStudent from '@/admin/EditConcreteStudent';

Vue.use(Router);

export const router = new Router({
    mode: 'history',
    routes: [
        { 
            path: '/', 
            component: HomePage, 
            meta: { authorize: [] } 
        },
        { 
            path: '/admin', 
            component: AdminPage, 
            meta: { authorize: [Role.Admin] } 
        },
        { 
            path: '/login', 
            component: LoginPage 
        },
        {
            path: '/editteachers',
            component: EditTeachers,
            meta: { authorize: [Role.Admin] }
        },
        {
            path: '/editstudents',
            component: EditStudents,
            meta: { authorize: [Role.Admin] }
        },
        {
            path: '/editschedule',
            component: EditSchedule,
            meta: { authorize: [Role.Admin] }
        },
        {
            path: '/editclasses',
            component: EditClasses,
            meta: { authorize: [Role.Admin] }
        },
        {
            path: '/addnewstudent',
            component: AddNewStudent,
            meta: { authorize: [Role.Admin] }
        },
        {
            path: '/editconcretestudent',
            name: 'TEST',
            component: EditConcreteStudent,
            meta: { authorize: [Role.Admin] }
        },

        // otherwise redirect to home
        { path: '*', redirect: '/' }
    ]
});

router.beforeEach((to, from, next) => {
    // redirect to login page if not logged in and trying to access a restricted page
    const { authorize } = to.meta;
    const currentUser = authenticationService.currentUserValue;

    if (authorize) {
        if (!currentUser) {
            // not logged in so redirect to login page with the return url
            return next({ path: '/login', query: { returnUrl: to.path } });
        }

        // check if route is restricted by role
        if (authorize.length && !authorize.includes(currentUser.role.name)) {
            // role not authorised so redirect to home page
            return next({ path: '/' });
        }
    }

    next();
})