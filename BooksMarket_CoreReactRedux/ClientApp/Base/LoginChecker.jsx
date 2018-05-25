import React from "react";
import { connect } from "react-redux";
import "../Components/css/Loader.css";
import { push } from "react-router-redux";
import RouteGenerator from "./RouteGenerator";

class LoginChecker extends React.Component {
    componentDidMount() {
        const { isAuthorized, onUnauthorized } = this.props; 
        if (!isAuthorized) 
            onUnauthorized();
            //save curr url for redirect after user login
    }
  
    render() {
        const { isAuthorized } = this.props;

        if (isAuthorized) {
            return <RouteGenerator />;
        }       
        else {
            return <div className="loader" style={{ width: "60px", height: "60px"}} />;
        }
    }
}

const mapStateToProps = (state) => 
    ({
        isAuthorized: state.isAuthorized
    });

const mapDispatchToProps = (dispatch) => ({
    onUnauthorized(){
        dispatch(push("/login"));
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(LoginChecker);