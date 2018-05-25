import constants from "../Base/Constants";

const books = (state = [], action) => {
    switch (action.type) {
        case constants.GET_BOOKS.ACTION:
            return action.books;
        case constants.RESPONSE_SERVER_ERROR:
            console.log(`Произошла ошибка: ${action.error}`);
            return state;
        default:
            return state;
    }
};

export default books;
