import constants from "../Base/Constants";
import genre from "./genre";

const genres = (state = [], action) => {
    switch (action.type) {
        case constants.GET_GENRES.ACTION:
            return action.genres;
        case constants.ADD_GENRE:
            return [...state, action.genre];
        case constants.EDIT_GENRE:
            return state.map(el => genre(el, action));
        case constants.DELETE_GENRE:
            return state.filter(el => el.id !== action.id);
        case constants.RESPONSE_SERVER_ERROR:
            console.log(`Произошла ошибка: ${action.error}`);
            return state;
        default:
            return state;
    }
};

export default genres;
