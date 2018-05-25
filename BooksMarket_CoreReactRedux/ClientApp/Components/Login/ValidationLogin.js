export const isValidUserName = username =>
    /^[\w_.-]+@[\w-_]+\.\w{2,3}$/.test(username)
        ? undefined
        : "Ошибка! Некорректный формат email";

export const required = value =>
    !value ? "Требуется ввести значение" : undefined;

export const isMinLength5 = value =>
    value && value.length >= 5
        ? undefined
        : "Длина должна быть 5 или более символов";
