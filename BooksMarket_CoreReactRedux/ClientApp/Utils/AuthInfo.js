class AuthInfo {
    constructor(token, expireTime, dispatch){
        this.authToken = token;
        this.expireTime = expireTime;
        this.dispatch = dispatch;
    }
}

export default AuthInfo;