import React from "react";
import { connect } from "react-redux";
import "../Components/css/Loader.css";
import { push } from "react-router-redux";
import RouteGenerator from "./RouteGenerator";
import constants  from "./Constants";
import Loading from "../Components/Layouts/Loading";

class LoginChecker extends React.PureComponent {

    redirectOnLogin = () => {
        const { isAuthorized, onUnauthorized } = this.props; 
        if (!isAuthorized) 
            onUnauthorized();
    }

    componentDidMount() {
        this.redirectOnLogin();
    }
  
    componentDidUpdate() {
        this.redirectOnLogin();
    }

    render = () => this.props.isAuthorized ? <RouteGenerator /> : <Loading />;
}

const mapStateToProps = (state) => 
    ({
        isAuthorized: state.isAuthorized,
        location : state.router.location.pathname
    });

const mapDispatchToProps = (dispatch) => ({
    onUnauthorized(){
        dispatch(push(constants.LOGIN_COMPONENT_PATH));
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(LoginChecker);