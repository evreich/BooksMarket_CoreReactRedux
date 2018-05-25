import constants from "../Base/Constants";

const validServerError = (state = "", action) => {
    switch (action.type) {
        case constants.VALID_SERVER_ERROR:
            return action.errors;
        case constants.CLEAR_VALID_SERVER_ERROR:
            return "";
        case constants.RESPONSE_SERVER_ERROR:
            console.log(`Произошла ошибка: ${action.error}`);
            return state;
        default:
            return state;
    }
};

export default validServerError;
