import { handleResponse, requestOptions } from '@/_helpers';

export const teacherManagementService = {
    getPinnedClasses,
    GetAllSubjects,
    GetStudentMarks
};


/**
 * Получает коллекцию всех учителей, хранящихся в бд.
 * @returns Результат запроса к бд.
 */
function getPinnedClasses() {
    return fetch(`${requestOptions.aspRoute}/api/TeachersEdit/GetAllTeachers`, requestOptions.get())
        .then(handleResponse);
}
/**
 * Получает коллекцию всех предметов, хранящихся в бд.
 * @returns Результат запроса к бд.
 */
function GetAllSubjects() {
    return fetch(`${requestOptions.aspRoute}/api/Subject/GetAll`, requestOptions.get())
        .then(handleResponse);
}
/**
 * Получает коллекцию оценок ученика, хранящихся в бд.
 * @param {*} studentMarkModel Содержит id ученика и id предмета.
 * @returns Результат запроса к бд.
 */
function GetStudentMarks(studentMarkModel) {
    return fetch(`${requestOptions.aspRoute}/api/Teacher/GetStudentMarks`, requestOptions.get(studentMarkModel))
        .then(handleResponse);
}