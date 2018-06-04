import constants from "../Base/Constants";
import { push } from "react-router-redux";
import { fetchPost, fetchGet } from "../Utils/CommonFetches";
import { validServerError, clearValidServerError } from "./CommonActions";
import getStartPageForRole from "../Utils/getStartPageForRole";
import AuthInfo from "../Utils/AuthInfo";

const logoutUser = user => ({
    type: constants.LOGOUT_USER.ACTION,
    user
});

const loginUser = user => ({
    type: constants.LOGIN_USER.ACTION,
    user
});

const authorizeUser = () => ({
    type: constants.AUTHORIZED
});

const notAuthorizeUser = () => ({
    type: constants.NOT_AUTHORIZED
});

const errorReceive = err => ({
    type: constants.RESPONSE_SERVER_ERROR,
    error: err
});

export const logoutAfterExpireTimeOut = () => dispatch => 
    new Promise((resolve, reject) => {
        try{
            dispatch(notAuthorizeUser());
            dispatch(logoutUser());
            localStorage.removeItem(constants.USER_KEY_ON_LOCALSTORAGE);
            dispatch({
                type: constants.VALID_SERVER_ERROR,
                errors: "Окончилось время сессии аутентифицированного пользователя!\nПожалуйста, повторите вход."
            });
            resolve();
        }
        catch(error){
            reject(error);
        }
    }).catch(error =>
        dispatch(errorReceive(error))
    );

export const logoutAction = (token) => (dispatch, getState) => {
    if (getState().user)
        return fetchGet(constants.BASE_API + constants.LOGOUT_USER.API, new AuthInfo(token, getState().user.expireTimeToken, dispatch))
            .then(response => response.text())
            .then(() => {
                dispatch(notAuthorizeUser());
                dispatch(logoutUser());
                localStorage.removeItem(constants.USER_KEY_ON_LOCALSTORAGE);
            })
            .catch(err => {
                dispatch(errorReceive(err));
            });
};

export const loginAction = user => dispatch =>
    fetchPost(constants.BASE_API + constants.LOGIN_USER.API, user)
        .then(async response => {
            if (response.ok) 
                return response.json();
            else {
                const errors = await response.json();
                if (response.status === 400)
                {
                    dispatch(validServerError({
                        statusCode: response.status,
                        errors
                    }));
                }
                throw errors;              
            }
        })
        .then(userParams => {
            dispatch(loginUser(userParams));
            return userParams;
        })
        .then(user => {
            localStorage.setItem(constants.USER_KEY_ON_LOCALSTORAGE, JSON.stringify(user));
            dispatch(authorizeUser());
            dispatch(clearValidServerError());
            dispatch(push(getStartPageForRole(user.role.name)));  
        })
        .catch(err => {
            dispatch(errorReceive(err));
        });

export const registrationAction = user => dispatch =>
    fetchPost(constants.BASE_API + constants.REGISTRATION_USER.API, user)
        .then(async response => {
            if (response.ok) 
                return response.json();
            else {
                const errors = await response.json();
                if (response.status === 400)
                {
                    dispatch(validServerError({
                        statusCode: response.status,
                        errors
                    }));
                }
                throw errors;              
            }
        })
        .then(userParams => {
            dispatch(loginUser(userParams));
            return userParams;
        })
        .then(user => {
            localStorage.setItem(constants.USER_KEY_ON_LOCALSTORAGE, JSON.stringify(user));
            dispatch(authorizeUser());
            dispatch(clearValidServerError());
            dispatch(push(getStartPageForRole(user.role.name)));  
        })
        .catch(err => {
            dispatch(errorReceive(err));
        });
