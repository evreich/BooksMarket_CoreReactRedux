import constants from "../Base/Constants";
import sideMenuElement from "./sideMenuElement";

const sideMenuElements = (state = [], action) => {
    switch (action.type) {
        case constants.GET_SLIDEMENU.ACTION:
            return [...action.sideMenuElements];
        case constants.ADD_SLIDEMENU_ITEM:
            return [...state, action.menuElement];
        case constants.EDIT_SLIDEMENU_ITEM:
            return state.map(el => sideMenuElement(el, action));
        case constants.DELETE_SLIDEMENU_ITEM:
            return state.filter(el => el.name !== action.nameMenu);
        default:
            return state;
    }
};

export default sideMenuElements;
