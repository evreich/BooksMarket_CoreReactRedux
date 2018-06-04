import fetch from "isomorphic-fetch";
import { logoutAfterExpireTimeOut } from "../Actions/UsersActions";
import isExpireTimeOutToken from "../Utils/checkExpireTimeToken";
/*
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == " ") {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
*/

export const fetchGet = (path, authInfo = undefined, data = undefined) => {
    if(authInfo && isExpireTimeOutToken(authInfo.expireTime)){
        authInfo.dispatch(logoutAfterExpireTimeOut());
        return Promise.reject("Окончилось время сессии аутентифицированного пользователя");
    }

    let url = new URL(path);
    if (data) {
        const params = {...data};
        Object.keys(params).forEach(key =>
            url.searchParams.append(key, params[key])
        );
    }
    const auth = authInfo ? { Authorization: `Bearer ${authInfo.authToken}` } : {};

    return fetch(url, {
        method: "GET",
        headers: new Headers({
            "Content-Type": "application/json",
            //"RequestVerificationToken": getCookie("RequestVerificationToken"),
            ...auth
        }),
        credentials: "include"
    });
};

export const fetchPost = (path, data = undefined, authInfo = undefined) => {
    if(authInfo && isExpireTimeOutToken(authInfo.expireTime)){
        authInfo.dispatch(logoutAfterExpireTimeOut());
        return Promise.reject();
    }

    const auth = authInfo ? { Authorization: `Bearer ${authInfo.authToken}` } : {};

    return fetch(path, {
        method: "POST",
        body: JSON.stringify(data),
        headers: new Headers({
            "Content-Type": "application/json",
            //"RequestVerificationToken": getCookie("RequestVerificationToken"),
            ...auth
        }),
        credentials: "include"
    });
};
