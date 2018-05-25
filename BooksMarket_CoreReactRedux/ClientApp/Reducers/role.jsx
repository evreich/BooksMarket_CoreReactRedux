import constants from "../Base/Constants";

const role = (state = {}, action) => {
    switch (action.type) {
        case constants.LOGIN_USER.ACTION:
            return {
                name: action.name,
                headerMenuElements: action.headerMenuElements,
                routes: action.routes
            };
        case constants.RESPONSE_SERVER_ERROR:
            console.log(`Произошла ошибка: ${action.error}`);
            return state;
        default:
            return state;
    }
};

export default role;
