import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const classService = {
    GetAllClasses,
    GetClass,
    AddNewClass
};

function GetAllClasses() {
    return fetch(`https://localhost:44303/api/class/GetAllClasses`, requestOptions.get())
        .then(handleResponse);
}
function GetClass(classModel) {
    return fetch(`https://localhost:44303/api/class/GetClass`, requestOptions.get(classModel))
        .then(handleResponse);
}
function AddNewClass(name) {
    return fetch(`https://localhost:44303/api/class/GetClass`, requestOptions.post(name))
        .then(handleResponse);
}