import constants from "../Base/Constants";

const isAuthorized = (state = false, action) => {
    switch (action.type) {
        case constants.AUTHORIZED:
            return true;
        case constants.NOT_AUTHORIZED:
            return false;
        default:
            return state;
    }
};

export default isAuthorized;
