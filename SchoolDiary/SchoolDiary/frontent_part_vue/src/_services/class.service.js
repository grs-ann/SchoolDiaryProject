import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const classService = {
    GetAllClasses
};

function GetAllClasses() {
    return fetch(`https://localhost:44303/api/class/GetAllClasses`, requestOptions.get())
        .then(handleResponse);
}
