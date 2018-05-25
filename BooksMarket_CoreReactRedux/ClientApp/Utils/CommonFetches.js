import fetch from "isomorphic-fetch";

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

export const fetchPost = (data, path) =>
    fetch(path, {
        method: "POST",
        body: JSON.stringify(data),
        headers: new Headers({
            "Content-Type": "application/json",
            "X-XSRF-TOKEN": getCookie("XSRF-TOKEN")
        }),
        credentials: "include"
    });

export const fetchPostWithAutorization = (data, path, token) =>
    fetch(path, {
        method: "POST",
        body: JSON.stringify(data),
        headers: new Headers({
            "Content-Type": "application/json",
            "X-XSRF-TOKEN": getCookie("XSRF-TOKEN"),
            "Authorization": `Bearer ${token}`
        }),
        credentials: "include"
    });

export const commonFetchWithoutParams = (path, method) =>
    fetch(path, {
        method: method,
        headers: new Headers({
            "Content-Type": "application/json"
        }),
        credentials: "include"
    });

export const commonFetchWithParams = (data, path, method) => {
    const url = new URL(path);
    const params = {...data};
    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));
    fetch(url, {
        method: method,
        headers: new Headers({
            "Content-Type": "application/json"
        }),
        credentials: "include"
    });
};


