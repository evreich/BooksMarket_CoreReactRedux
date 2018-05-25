import constants from "../Base/Constants";

const sideMenuElement = (state = [], action) => {
    switch (action.type) {
        case constants.EDIT_SLIDEMENU_ITEM:
            return state.name !== action.nameMenu
                ? state
                : {
                      ...state,
                      name: action.nameMenu
                  };
        default:
            return state;
    }
};

export default sideMenuElement;
