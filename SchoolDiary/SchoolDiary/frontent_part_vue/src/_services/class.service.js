import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const classService = {
    GetAllClasses,
    GetClass,
    AddNewClass
};

function GetAllClasses() {
    return fetch(`${requestOptions.aspRoute}/api/class/GetAllClasses`, requestOptions.get())
        .then(handleResponse);
}
function GetClass(classModel) {
    return fetch(`${requestOptions.aspRoute}/api/class/GetClass`, requestOptions.get(classModel))
        .then(handleResponse);
}
function AddNewClass(name) {
    return fetch(`${requestOptions.aspRoute}/api/class/GetClass`, requestOptions.post(name))
        .then(handleResponse);
}