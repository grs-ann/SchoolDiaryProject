import { handleResponse, requestOptions } from '@/_helpers';

export const teacherService = {
    getPinnedClasses
};


/**
 * Получает коллекцию всех учителей, хранящихся в бд.
 * @returns Результат запроса к бд.
 */
function getPinnedClasses() {
    return fetch(`${requestOptions.aspRoute}/api/TeachersEdit/GetAllTeachers`, requestOptions.get())
        .then(handleResponse);
}