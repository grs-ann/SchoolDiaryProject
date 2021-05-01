import { handleResponse, requestOptions } from '@/_helpers';

export const teacherService = {
    getAllTeachers,
    registerNewTeacher,
    changeTeacher
};


/**
 * Получает коллекцию всех учителей, хранящихся в бд.
 * @returns Результат запроса к бд.
 */
function getAllTeachers() {
    return fetch(`${requestOptions.aspRoute}/api/TeachersEdit/GetAllTeachers`, requestOptions.get())
        .then(handleResponse);
}
/**
 * Добавляет в бд нового учителя.
 * @param {*} teacherData - данные для создания нового учителя.
 * @returns Результат выполнения.
 */
function registerNewTeacher(teacherData) {
    return fetch(`${requestOptions.aspRoute}/api/account/RegisterTeacher`,
        requestOptions.post(teacherData))
            .then(handleResponse);
}
/**
 * Изменяет данные об учителе в бд.
 * @param {*} teacherData Измененные данные учителя.
 * @returns Результат изменения.
 */
function changeTeacher(teacherData) {
    return fetch(`${requestOptions.aspRoute}/api/TeachersEdit/ChangeTeacher`,
        requestOptions.put(teacherData))
            .then(handleResponse)
}