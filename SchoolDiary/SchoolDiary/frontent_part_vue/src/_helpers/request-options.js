import { authenticationService } from '@/_services';

export const requestOptions = {
    aspRoute: 'https://localhost:44303',
    get() {
        return {
            method: 'GET',
            ...headers(),
        };
    },
    post(body) {
        return {
            method: 'POST',
            ...headers(),
            body: JSON.stringify(body)
        };
    },
    patch(body) {
        return {
            method: 'PATCH',
            ...headers(),
            body: JSON.stringify(body)
        };
    },
    put(body) {
        return {
            method: 'PUT',
            ...headers(),
            body: JSON.stringify(body)
        };
    },
    delete(body) {
        return {
            method: 'DELETE',
            ...headers(),
            body: JSON.stringify(body)
        };
    }
}

function headers() {
    const currentUser = authenticationService.currentUserValue || {};
    const authHeader = currentUser.token ? { 'Authorization': 'Bearer ' + currentUser.token } : {}
    return {
        headers: {
            ...authHeader,
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE',
            'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
        }
    }
}