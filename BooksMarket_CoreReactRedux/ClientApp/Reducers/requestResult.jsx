import constants from "../Base/Constants";

const requestResult = (state = "", action) => {
    switch (action.type) {
        case constants.SET_REQUEST_RESULT:
            return action.result;
        case constants.CLEAR_REQUEST_RESULT:
            return "";
        default:
            return state;
    }
};

export default requestResult;
