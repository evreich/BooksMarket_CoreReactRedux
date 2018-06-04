import React from "react";
import { connect } from "react-redux";
import { Route, Switch, Redirect } from "react-router-dom";
import { push } from "react-router-redux";
import PropTypes from "prop-types";
import getStartPageForRole from "../Utils/getStartPageForRole";
import * as components from "../Components";
import constants from "./Constants";

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
        if (routes.length === 0)
            return (
                <div className="d-flex justify-content-center align-content-center mt-4 ">              
                    <div className="loader" style={{ width: "120px", height: "120px"}}></div>
                </div>
            );
        return (
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

const mapStateToProps = state => {
    const location = state.router.location.pathname;
    const rolename = state.user ? state.user.role.name : "";
    const routes = state.user ? state.user.role.routes : [];
    return { routes, location, rolename };
};

const mapDispatchToProps = dispatch => ({
    onNotAuthorized() {
        dispatch(push(constants.LOGIN_COMPONENT_PATH));
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(RouteGenerator);
