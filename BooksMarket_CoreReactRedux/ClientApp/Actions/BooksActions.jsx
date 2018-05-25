import constants from "../Base/Constants";
import fetch from "isomorphic-fetch";
import Book from "../Models/Book";

const getBooks = books => ({
    type: constants.GET_BOOKS.ACTION,
    books
});

const errorReceive = err => ({
    type: constants.RESPONSE_SERVER_ERROR,
    error: err
});

export const getBooksAction = (
    searchExpr = undefined,
    page = 1
) => dispatch => {
    const searchParam = searchExpr ? `searchExpr=${searchExpr}&` : "";
    const pageParam = `page=${page}`;
    const queryToAPI = `${constants.BASE_API}${constants.GET_BOOKS.API}?${searchParam}${pageParam}`;
    return fetch(queryToAPI)
        .then(response => response.json())
        .then(data => {
            const books = data.map(el => new Book(...el));
            dispatch(getBooks(books));
        })
        .catch(err => dispatch(errorReceive(err)));
};

export const addBookAction = (book, searchExpr, page) => dispatch =>
    fetch(constants.BASE_API + constants.ADD_BOOK.API, {
        method: "POST",
        body: JSON.stringify(book)
    })
        .then(response => response.json())
        .then(() => getBooksAction(searchExpr, page))
        .catch(err => dispatch(errorReceive(err)));

export const editBookAction = (book, searchExpr, page) => dispatch =>
    fetch(constants.BASE_API + constants.EDIT_BOOK.API, {
        method: "POST",
        body: JSON.stringify(book)
    })
        .then(response => response.json())
        .then(() => getBooksAction(searchExpr, page))
        .catch(err => dispatch(errorReceive(err)));

export const deleteBookAction = (bookId, searchExpr, page) => dispatch =>
    fetch(constants.BASE_API + constants.DELETE_BOOK.API, {
        method: "POST",
        body: JSON.stringify(bookId)
    })
        .then(response => response.json())
        .then(() => getBooksAction(searchExpr, page))
        .catch(err => dispatch(errorReceive(err)));
