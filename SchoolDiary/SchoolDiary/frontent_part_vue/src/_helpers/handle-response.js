import { authenticationService } from '@/_services';

export function handleResponse(response) {
    return response.text().then(text => {
        try {
            const data = text && JSON.parse(text);
            if (!response.ok) {
                if ([401, 403].indexOf(response.status) !== -1) {
                    // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                    authenticationService.logout();
                    location.reload(true);
                }
                // Обработка ошибок валидации моделей с бекенда.
                if ([400].indexOf(response.status) !== -1 && data.title == "One or more validation errors occurred.") {
                    const error = data.errors;
                    return Promise.reject(error);
                }
                const error = (data && data.message) || response.statusText;
                return Promise.reject(error);
            }

            return data;
        } catch(err) {
        }
        
    });
}