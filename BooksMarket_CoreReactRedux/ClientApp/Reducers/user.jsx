import constants from "../Base/Constants";
import role from "./role";

const user = (state = {}, action) => {
    switch (action.type) {
        case constants.LOGOUT_USER.ACTION:
            return {};
        case constants.LOGIN_USER.ACTION:
            return {
                name: action.user.name,
                token: action.user.token,
                role: role({}, {
                    type: action.type,
                    name: action.user.role.name,
                    headerMenuElements: action.user.role.headerMenuElements,
                    routes: action.user.role.routes
                })
            };
        case constants.RESPONSE_SERVER_ERROR:
            console.log(`Произошла ошибка: ${action.error}`);
            return state;
        default:
            return state;
    }
};

export default user;
