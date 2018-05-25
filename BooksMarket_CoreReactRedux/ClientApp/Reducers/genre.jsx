import constants from "../Base/Constants";

const genre = (state = [], action) => {
    switch (action.type) {
        case constants.EDIT_GENRE:
            return state.id !== action.id
                ? state
                : {
                      id: action.id,
                      title: action.title
                  };
        default:
            return state;
    }
};

export default genre;
