export const isRequired = value =>
    !value ? "Требуется ввести значение" : undefined;

export const isValidUserName = username =>
    /^[\w_.-]+@[\w-_]+\.\w{2,3}$/.test(username)
        ? undefined
        : "Ошибка! Некорректный формат email";

export const isValidFullName = fullname => 
    /^[а-яА-Я]+ [а-яА-Я]+ [а-яА-Я]+$/.test(fullname)
        ? undefined
        : "Ошибка! Некорректный формат ФИО";

export const isValidPhone = phone => 
    /^[0-9]{11}$/.test(phone)
        ? undefined
        : "Ошибка! Некорректный формат телефона";

export const isMinLength5 = value =>
    value && value.length >= 5
        ? undefined
        : "Длина должна быть 5 или более символов";

export const isEqualWithPass = (password, passwordRepeat) =>
    password === passwordRepeat
        ? undefined
        : "Не сопадение с паролем";

export const checkFieldsIsFilled = (inputFields, stateErrorsFields, setState) => {
    const errorsState = [];

    let isFilled = true;
    inputFields.forEach((input, i) => {
        const error = isRequired(input.current.value);
        if (error){
            isFilled = false; 
            errorsState.push({
                stateField : stateErrorsFields[i],
                errortext : error
            }); 
        }
        else 
            errorsState.push({
                stateField : stateErrorsFields[i],
                errortext : ""
            });         
    });

    let result = errorsState.reduce(function(acc, currVal) {
            acc[currVal.stateField] = currVal.errortext;
            return acc;
        }, {});
        
    setState({...result});
    return isFilled;
};