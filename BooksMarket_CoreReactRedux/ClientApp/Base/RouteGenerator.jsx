import React from "react";
import { connect } from "react-redux";
import { Route, Switch, Redirect } from "react-router-dom";
import { push } from "react-router-redux";
import PropTypes from "prop-types";
import getStartPageForRole from "../Utils/getStartPafeForRole";
import * as components from "../Components";

class RouteGenerator extends React.PureComponent {
    static propTypes = {
        routes: PropTypes.arrayOf(PropTypes.object),
        location: PropTypes.string,
        rolename: PropTypes.string
    };

    componentDidMount() {
        if (this.props.routes === []) 
            this.props.onNotAuthorized();
    }

    render() {
        const { routes, rolename } = this.props;
        if (routes === [])
            return (
                <div
                    className="loader"
                    style={{ width: "60px", height: "60px" }}
                />
            );
        else
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
        dispatch(push("/login"));
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(RouteGenerator);
