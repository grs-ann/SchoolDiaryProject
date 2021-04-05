import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const userService = {
    getAll,
    getById,
    getAllStudents,
    addNewStudent
};



function getAllStudents() {
    return fetch(`${requestOptions.aspRoute}/api/studentsedit/getallstudents`, requestOptions.get())
        .then(handleResponse);
}
function addNewStudent(registerModel) {
    return fetch(`${requestOptions.aspRoute}/api/account/RegisterStudent`, requestOptions.post(registerModel))
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
