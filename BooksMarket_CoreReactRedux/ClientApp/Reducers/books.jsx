import constants from "../Base/Constants";

const books = (state = [], action) => {
    switch (action.type) {
        case constants.SET_BOOKS:
            return action.books;
        case constants.ADD_BOOK:
            return [...state, action.book];
        default:
            return state;
    }
};

export default books;
