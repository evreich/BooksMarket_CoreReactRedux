import React from "react";
import { connect } from "react-redux";
import { hot } from "react-hot-loader";
import { lifecycle } from "recompose";
import { Switch, Route, withRouter } from "react-router-dom";

import CommonLayout from "../Components/Layouts/CommonLayout";
import LoginChecker from "./LoginChecker";
import { Login, AccessDenied, Registration } from "../Components";
import isExpireTimeOutToken from "../Utils/checkExpireTimeToken";
import { logoutAction } from "../Actions/UsersActions";

const RootComponent = () => (
    <CommonLayout>
        <Switch>
            <Route exact path="/login" component={Login} />
            <Route exact path="/registration" component={Registration} />
            <Route exact path="/accessDenied" component={AccessDenied} />
            <Route path="/" component={LoginChecker} />
        </Switch>
    </CommonLayout>
);

const App = lifecycle({
    componentDidMount() {
        const { token, redirectToLogin, expireTime } = this.props;
        if (expireTime && isExpireTimeOutToken(expireTime)) 
            redirectToLogin(token);
    }
})(RootComponent);

const mapStateToProps = state => ({
    location: state.router.location.pathname,
    token: state.user ? state.user.token : "",
    expireTime: state.user ? state.user.expireTimeToken : undefined
});

const mapDispatchToProps = dispatch => ({
    redirectToLogin(token) {
        dispatch(logoutAction(token));
    }
});

export default hot(module)(withRouter(connect(mapStateToProps, mapDispatchToProps)(App)));
