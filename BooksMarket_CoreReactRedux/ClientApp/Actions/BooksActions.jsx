import constants from "../Base/Constants";
import { fetchGet, fetchPost } from "../Utils/CommonFetches";
import { setRequestResult } from "../Actions/CommonActions";
import AuthInfo from "../Utils/AuthInfo";

const setBooks = books => ({
    type: constants.SET_BOOKS,
    books
});

const errorReceive = err => ({
    type: constants.RESPONSE_SERVER_ERROR,
    error: err
});

export const getAllBooksAction = () => (dispatch, getState) => {
    const { expireTimeToken, token } = getState().user;
    const queryToAPI = `${constants.BASE_API}${constants.GET_BOOKS.API}`;

    return fetchGet(queryToAPI,new AuthInfo(token, expireTimeToken, dispatch))
        .then(response => response.json())
        .then(data => dispatch(setBooks(data)))
        .catch(err => dispatch(errorReceive(err)));
};

export const getBooksByFilterAction = (searchExpr = "", page = 1) => (dispatch, getState) => {
    let data = { page };
    
    if(searchExpr)
        data = {...data, searchExpr};
    
    const { expireTimeToken, token } = getState().user;
    const queryToAPI = `${constants.BASE_API}${constants.GET_BOOKS_BY_FILTER.API}`;

    return fetchGet(queryToAPI,new AuthInfo(token, expireTimeToken, dispatch), data)
        .then(response => response.json())
        .then(data => dispatch(setBooks(data)))
        .catch(err => dispatch(errorReceive(err)));
};

export const addBookAction = (book, token) => (dispatch, getStore) =>
    fetchPost(constants.BASE_API + constants.ADD_BOOK.API, book, new AuthInfo(token, getStore().user.expireTimeToken, dispatch) )
        .then(response => {
            if (response.ok) return true;
            else throw new Error(response.statusText);
        })
        .then(() => {
            dispatch(getAllBooksAction());
            dispatch(setRequestResult("Книга успешно добавлена!"));
        })
        .catch(err => {
            dispatch(errorReceive(err));
            dispatch(setRequestResult("Произошла ошибка при добавлении книги."));
        });

export const editBookAction = (book, searchExpr, page) => dispatch =>
    fetch(constants.BASE_API + constants.EDIT_BOOK.API, {
        method: "POST",
        body: JSON.stringify(book)
    })
        .then(response => response.json())
        .then(() => getAllBooksAction(searchExpr, page))
        .catch(err => dispatch(errorReceive(err)));

export const deleteBookAction = (bookId, searchExpr, page) => dispatch =>
    fetch(constants.BASE_API + constants.DELETE_BOOK.API, {
        method: "POST",
        body: JSON.stringify(bookId)
    })
        .then(response => response.json())
        .then(() => getAllBooksAction(searchExpr, page))
        .catch(err => dispatch(errorReceive(err)));
