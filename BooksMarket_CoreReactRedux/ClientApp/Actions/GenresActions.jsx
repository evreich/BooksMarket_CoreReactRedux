import constants from "../Base/Constants";
import { fetchGet } from "../Utils/CommonFetches";
import { addMenuItem, editMenuItem, deleteMenuItem } from "./SideMenuActions";
import AuthInfo from "../Utils/AuthInfo";

const getGenres = genres => ({
    type: constants.GET_GENRES.ACTION,
    genres
});

const addGenre = genre => ({
    type: constants.ADD_GENRE.ACTION,
    genre
});

const editGenre = genre => ({
    type: constants.EDIT_GENRE.ACTION,
    genre
});

const deleteGenre = id => ({
    type: constants.DELETE_GENRE.ACTION,
    id
});

const errorReceive = err => ({
    type: constants.RESPONSE_SERVER_ERROR,
    error: err
});

export const getGenresAction = (token) => (dispatch, getState) => {
    return fetchGet(constants.BASE_API + constants.GET_GENRES.API, new AuthInfo(token, getState().user.expireTimeToken, dispatch))
        .then(response => response.json())
        .then(data => dispatch(getGenres(data)))
        .catch(err => 
            dispatch(errorReceive(err)));
};

export const addGenreAction = genre => dispatch =>
    fetch(constants.BASE_API + constants.ADD_GENRE.API, {
        method: "POST",
        body: JSON.stringify(genre)
    })
        .then(response => response.json())
        .then(data => dispatch(addGenre(data)))
        .then(() => dispatch(addMenuItem(genre.title)))
        .catch(err => dispatch(errorReceive(err)));

export const editGenreAction = genre => dispatch =>
    fetch(constants.BASE_API + constants.EDIT_GENRE.API, {
        method: "POST",
        body: JSON.stringify(genre)
    })
        .then(response => response.json())
        .then(data => dispatch(editGenre(data)))
        .then(() => dispatch(editMenuItem(genre.title)))
        .catch(err => dispatch(errorReceive(err)));

export const deleteGenreAction = genre => dispatch =>
    fetch(constants.BASE_API + constants.DELETE_GENRE.API, {
        method: "POST",
        body: JSON.stringify(genre.id)
    })
        .then(response => response.json())
        .then(data => dispatch(deleteGenre(data)))
        .then(() => dispatch(deleteMenuItem(genre.title)))
        .catch(err => dispatch(errorReceive(err)));
