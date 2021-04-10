import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const userService = {
    getAll,
    getById,
    getAllStudents,
    registerNewStudent,
    deleteStudentById
};



function getAllStudents() {
    return fetch(`${requestOptions.aspRoute}/api/studentsedit/getallstudents`, requestOptions.get())
        .then(handleResponse);
}
function registerNewStudent(studentData) {
    return fetch(`${requestOptions.aspRoute}/api/account/RegisterStudent`, requestOptions.post(studentData))
        .then(handleResponse);
}
function deleteStudentById(id) {
    return fetch(`${requestOptions.aspRoute}/api/account/DeleteUser`, requestOptions.delete(id))
        .then(handleResponse);
}




function getAll() {
    return fetch(`${config.apiUrl}/users`, requestOptions.get())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/users/${id}`, requestOptions.get())
        .then(handleResponse);
}
