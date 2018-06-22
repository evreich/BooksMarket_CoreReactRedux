import React from "react";
import { connect } from "react-redux";
import { Route, Switch, Redirect } from "react-router-dom";
import { push } from "react-router-redux";
import PropTypes from "prop-types";
import getStartPageForRole from "../Utils/getStartPageForRole";
import * as components from "../Components";
import constants from "./Constants";
import Loading from "../Components/Layouts/Loading";

class RouteGenerator extends React.PureComponent {
    constructor(props){
        super(props);
        this.checkRedirectOnLogin = this.checkRedirectOnLogin.bind(this);
    }

    static propTypes = {
        routes: PropTypes.arrayOf(PropTypes.object),
        location: PropTypes.string,
        rolename: PropTypes.string
    };

    checkRedirectOnLogin() {
        const { routes, onNotAuthorized } = this.props;
        if (routes.length === 0) 
            onNotAuthorized();
    }

    componentDidMount() {
        this.checkRedirectOnLogin();
    }

    componentDidUpdate() {
        this.checkRedirectOnLogin();
    }

    render() {
        const { routes, rolename } = this.props;
        return !routes.length ? <Loading /> : (
            <Switch>
                <Redirect exact key="base" from="/" to={getStartPageForRole(rolename)} />
                {routes.map(el => 
                    (el.isAccessable) 
                    ? <Route
                        key={`${el.id}_${el.component}`}
                        path={el.path}
                        component={components[el.component]}
                        />
                    : <Redirect 
                        key={`${el.id}_${el.component}`}
                        from={el.path} 
                        to="/accessDenied"
                        />
                )}
            </Switch>
        );
    }
}

const mapStateToProps = state => ({
    location : state.router.location.pathname,
    rolename : state.user && state.user.role ? state.user.role.name : "",
    routes : state.user && state.user.role ? state.user.role.routes : []
});

const mapDispatchToProps = dispatch => ({
    onNotAuthorized() {
        dispatch(push(constants.LOGIN_COMPONENT_PATH));
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(RouteGenerator);
