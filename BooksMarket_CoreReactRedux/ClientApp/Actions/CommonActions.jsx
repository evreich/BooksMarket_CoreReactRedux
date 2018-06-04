import constants from "../Base/Constants";

export const validServerError = errors => ({
    type: constants.VALID_SERVER_ERROR,
    errors:"Серверная ошибка:\n" + Object.values(errors.errors)
        .map(el =>
            el.reduce(
                (prevValue, currValue) => (prevValue += `${currValue}\n`),
                ""
            )
        )
        .reduce((prevValue, currValue) => (prevValue += currValue), "")
        
});

export const setRequestResult = result => ({
    type: constants.SET_REQUEST_RESULT,
    result
});

export const clearRequestResult = () => ({
    type: constants.CLEAR_REQUEST_RESULT
});

export const clearValidServerError = () => ({
    type: constants.CLEAR_VALID_SERVER_ERROR
});