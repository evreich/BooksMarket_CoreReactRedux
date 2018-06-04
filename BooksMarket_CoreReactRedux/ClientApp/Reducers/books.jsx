import constants from "../Base/Constants";

const books = (state = [], action) => {
    switch (action.type) {
        case constants.SET_BOOKS.ACTION:
            return action.books;
        default:
            return state;
    }
};

export default books;
