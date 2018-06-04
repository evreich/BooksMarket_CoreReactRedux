import { applyMiddleware, combineReducers, createStore } from "redux";
import rootReducer from "../Reducers";
import { routerReducer, routerMiddleware } from "react-router-redux";
import constants from  "../Base/Constants";
import thunk from "redux-thunk";

const user = localStorage.getItem(constants.USER_KEY_ON_LOCALSTORAGE);

export const stateData = {
    books: [],
    genres: [],
    sideMenuElements: [],
    user : user ? JSON.parse(user) : user,
    isAuthorized : user ? true : false,
    validServerError : "",
    requestResult: ""
};

const clientLogger = store => next => action => {
    let result = "";
    console.groupCollapsed("dispatching", action.type);
    console.log("prev state", store.getState());
    console.log("action", action);
    result = next(action);
    console.log("next state", store.getState());
    console.groupEnd();
    return result;
};

export const stateCreator = (history, initialState = stateData) =>
    applyMiddleware(
        clientLogger, 
        routerMiddleware(history),
        thunk
    )(createStore)(
        combineReducers({
            ...rootReducer,
            router: routerReducer
        }),
        initialState,
        window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
    );