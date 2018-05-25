import constants from "../Base/Constants";
import fetch from "isomorphic-fetch";
import MenuElement from "../Models/MenuElement";

const getMenuItems = menuItems => ({
    type: constants.GET_SLIDEMENU.ACTION,
    sideMenuElements: menuItems
});

export const addMenuItem = nameMenu => ({
    type: constants.ADD_SLIDEMENU_ITEM,
    menuItem: new MenuElement(nameMenu, `/books/${nameMenu}/1`)
});

export const editMenuItem = nameMenu => ({
    type: constants.EDIT_SLIDEMENU_ITEM,
    nameMenu
});

export const deleteMenuItem = nameMenu => ({
    type: constants.DELETE_SLIDEMENU_ITEM,
    nameMenu
});

const errorReceive = err => ({
    type: constants.RESPONSE_SERVER_ERROR,
    error: err
});

export const getMenuItemsAction = () => dispatch =>
    fetch(constants.BASE_API + constants.GET_SLIDEMENU.API)
        .then(response => response.json())
        .then(data => {
            const menuElements = data.map(el => new MenuElement(el.title, `/books/${el.title}/1`));
            dispatch(getMenuItems(menuElements));
        })
        .catch(err => dispatch(errorReceive(err)));
