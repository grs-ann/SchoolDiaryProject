import config from 'config';
import { handleResponse, requestOptions } from '@/_helpers';

export const userService = {
    getAll,
    getById,
    getAllStudents,
    registerNewStudent,
    deleteStudentById,
    changeStudent
};


/**
 * Получает коллекцию всех учеников, хранящихся в бд.
 * @returns Результат запроса к бд.
 */
function getAllStudents() {
    return fetch(`${requestOptions.aspRoute}/api/studentsedit/getallstudents`, requestOptions.get())
        .then(handleResponse);
}
/**
 * Добавляет в бд нового ученика.
 * @param {Object} studentData - заполненные данные по новому ученику.
 * @returns Результат выполнения.
 */
function registerNewStudent(studentData) {
    return fetch(`${requestOptions.aspRoute}/api/account/RegisterStudent`, requestOptions.post(studentData))
        .then(handleResponse);
}
/**
 * Удаляет из базы данных ученика(пользователя).
 * @param {Number} id ученика(пользователя) в бд.
 * @returns Результат удаления.
 */
function deleteStudentById(id) {
    return fetch(`${requestOptions.aspRoute}/api/account/DeleteUser`, requestOptions.delete(id))
        .then(handleResponse)
}
/**
 * Изменяет данные ученика в бд.
 * @param {Object} studentData 
 * @returns Результат изменения.
 */
function changeStudent(studentData) {
    return fetch(`${requestOptions.aspRoute}/api/studentsedit/changestudent`, requestOptions.put(studentData))
}



function getAll() {
    return fetch(`${config.apiUrl}/users`, requestOptions.get())
        .then(handleResponse);
}

function getById(id) {
    return fetch(`${config.apiUrl}/users/${id}`, requestOptions.get())
        .then(handleResponse);
}
