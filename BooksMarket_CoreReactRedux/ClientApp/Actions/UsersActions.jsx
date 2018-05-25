import constants from "../Base/Constants";
import { push } from "react-router-redux";
import { fetchPost, fetchPostWithAutorization } from "../Utils/CommonFetches";
import { validServerError } from "./CommonActions";
import getStartPageForRole from "../Utils/getStartPafeForRole";

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

export const logoutAction = (token) => dispatch =>
    fetchPostWithAutorization({}, constants.BASE_API + constants.LOGOUT_USER.API, token)
        .then(response => response.json())
        .then(data => {
            dispatch(logoutUser(data));
            dispatch(notAuthorizeUser());
        })
        .then(() => {
            localStorage.removeItem(constants.USER_KEY_ON_LOCALSTORAGE);
        })
        .catch(err => {
            dispatch(errorReceive(err));
        });

export const loginAction = user => dispatch =>
    fetchPost(user, constants.BASE_API + constants.LOGIN_USER.API)
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
                    throw errors;
                }
                else 
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
            dispatch(push(getStartPageForRole(user.role.name)));  
        })
        .catch(err => {
            dispatch(errorReceive(err));
        });

export const registrationAction = user => dispatch =>
    fetchPost(user, constants.BASE_API + constants.REGISTRATION_USER.API)
        .then(response => response.json())
        .then(userParams => {
            dispatch(loginUser(userParams));
            //combine user object
            return userParams;
        })
        .then(user => {
            localStorage.setItem(constants.USER_KEY_ON_LOCALSTORAGE, user);
            dispatch(push(constants.USER_STARTPAGE));
        })
        .catch(err => {
            dispatch(errorReceive(err));
        });
