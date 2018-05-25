const books_const = {
    GET_BOOKS: {
        ACTION: "GET_BOOKS",
        API: "/books"
    },
    ADD_BOOK: {
        ACTION: "ADD_BOOK",
        API: "/books/add"
    },
    EDIT_BOOK: {
        ACTION: "EDIT_BOOK",
        API: "/books/edit"
    },
    DELETE_BOOK: {
        ACTION: "DELETE_BOOK",
        API: "/books/delete"
    }
};

const genres_const = {
    GET_GENRES: {
        ACTION: "GET_GENRES",
        API: "/genres/getGenres"
    },
    ADD_GENRE: {
        ACTION: "ADD_GENRE",
        API: "/genres/add"
    },
    EDIT_GENRE: {
        ACTION: "EDIT_GENRE",
        API: "/genres/edit"
    },
    DELETE_GENRE: {
        ACTION: "DELETE_GENRE",
        API: "/genres/delete"
    }
};

const users_const = {
    REGISTRATION_USER: {
        ACTION: "REGISTRATION_USER",
        API: "/account/registration"
    },
    LOGOUT_USER: {
        ACTION: "LOGOUT_USER",
        API: "/account/logOff"
    },
    LOGIN_USER: {
        ACTION: "LOGIN_USER",
        API: "/account/login"
    }
};

const sidebar_const = {
    GET_SLIDEMENU: {
        ACTION: "GET_SLIDEMENU",
        API: "/genres/getGenres"
    },
    ADD_SLIDEMENU_ITEM: "ADD_SLIDEMENU_ITEM",
    EDIT_SLIDEMENU_ITEM: "EDIT_SLIDEMENU_ITEM",
    DELETE_SLIDEMENU_ITEM: "DELETE_SLIDEMENU_ITEM"
};

const roleStartPage = {
    USER_STARTPAGE : "/market/books",
    BOOKKEEPER_STARTPAGE : "/orders",
    STOREKEEPER_STARTPAGE : "/books",
    ADMIN_STARTPAGE : "/books"
};

const roles = {
    ADMIN_ROLE : "admin",
    USER_ROLE : "user",
    BOOKKEEPER_ROLE : "bookkeeper",
    STOREKEEPER_ROLE : "storekeeper"
};

const constants = {
    BASE_API: "http://localhost:53050/api",
    RESPONSE_SERVER_ERROR: "RESPONSE_SERVER_ERROR",
    VALID_SERVER_ERROR: "VALID_SERVER_ERROR",
    CLEAR_VALID_SERVER_ERROR: "CLEAR_VALID_SERVER_ERROR",
    AUTHORIZED: "AUTHORIZED",
    NOT_AUTHORIZED: "NOT_AUTHORIZED",
    ...users_const,
    ...sidebar_const,
    ...books_const,
    ...genres_const,
    ...roleStartPage,
    ...roles,
    USER_KEY_ON_LOCALSTORAGE : "user"
};

export default constants;
