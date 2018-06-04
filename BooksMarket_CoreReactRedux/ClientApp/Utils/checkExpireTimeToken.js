const isExpireTimeOutToken = (expireTimeToken) => {
    const currTime = Date.parse(new Date());
    return expireTimeToken - currTime <= 0;
};

export default isExpireTimeOutToken;