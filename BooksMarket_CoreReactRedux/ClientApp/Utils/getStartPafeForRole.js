import constants from "../Base/Constants";

const getStartPageForRole = role => {
    switch (role) {
        case constants.USER_ROLE:
            return constants.USER_STARTPAGE;
        case constants.ADMIN_ROLE:
            return constants.ADMIN_STARTPAGE;
        case constants.BOOKKEEPER_ROLE:
            return constants.BOOKKEEPER_STARTPAGE;
        case constants.STOREKEEPER_ROLE:
            return constants.STOREKEEPER_STARTPAGE;
        default:
            throw new Error("Некорректная роль");
    }
};

export default getStartPageForRole;

