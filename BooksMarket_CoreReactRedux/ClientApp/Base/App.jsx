import React from "react";
import { Switch, Route } from "react-router-dom";
import CommonLayout from "../Components/Layouts/CommonLayout";
import LoginChecker from "./LoginChecker";
import { Login, AccessDenied } from "../Components";
import { hot } from "react-hot-loader";

const App = () => (
    <CommonLayout>
        <Switch>
            <Route exact path="/login" component={Login} />
            <Route exact path="/accessDenied" component={AccessDenied} />
            <Route path="/" component={LoginChecker} />
        </Switch>
    </CommonLayout>
);

export default hot(module)(App);
