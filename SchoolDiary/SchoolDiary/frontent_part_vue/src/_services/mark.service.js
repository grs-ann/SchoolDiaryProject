import { handleResponse, requestOptions } from '@/_helpers';

export const markService = {
    getAllMarks,
    changeMark,
    addNewMark
};
/**
 * Получает коллекцию всех видов оценок, хранящихся в бд.
 * @returns Результат запроса к бд.
 */
function getAllMarks() {
    return fetch(`${requestOptions.aspRoute}/api/Teacher/GetAllMarks`, requestOptions.get())
        .then(handleResponse);
}
/**
 * Изменяет оценку в бд.
 * @param {*} markToChangeModel 
 * @returns 
 */
function changeMark(markToChangeModel) {
    return fetch(`${requestOptions.aspRoute}/api/Teacher/ChangeMark`, requestOptions.put(markToChangeModel))
        .then(handleResponse);
}
/**
 * Добавляет новую оценку в бд.
 * @param {*} addingMarkData 
 * @returns 
 */
function addNewMark(addingMarkData) {
    return fetch(`${requestOptions.aspRoute}/api/Teacher/AddNewMark`, requestOptions.post(addingMarkData))
    .then(handleResponse);
}