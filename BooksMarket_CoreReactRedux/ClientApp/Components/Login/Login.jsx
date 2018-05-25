import React from "react";
import { connect } from "react-redux";
import { push } from "react-router-redux";
import { Link } from "react-router-dom";
import { loginAction } from "../../Actions/UsersActions";
import { clearValidServerError } from "../../Actions/CommonActions";
import * as validators from "./ValidationLogin";
import "./Login.css";

class LoginComponent extends React.PureComponent { 
    constructor(props){
        super(props);  
        this.state = {
            username: "",
            usernameValidError: "",
            password: "",
            passwordValidError: "",
            isRememberMe: false
        };  
        this.handleChangeUserName = this.handleChangeUserName.bind(this);
        this.handleChangePassword = this.handleChangePassword.bind(this);
        this.handleChangeIsRememberMe = this.handleChangeIsRememberMe.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChangeUserName(event) {
        const username = event.target.value;
        [validators.required, validators.isValidUserName].every(validator => {
            const error = validator(username);
            if (!error)
                return true;
            else {
                this.setState({usernameValidError : error});
                return false;
            }
        })
        ? this.setState({usernameValidError: ""}) 
        : undefined;
        this.setState({username: username});
    }

    handleChangePassword(event) {
        const password = event.target.value;
        [validators.required, validators.isMinLength5].every(validator => {
            const error = validator(password);
            if (!error)
                return true;
            else {
                this.setState({passwordValidError: error});
                return false;
            }
        }) 
        ? this.setState({passwordValidError: ""}) 
        : undefined;
        this.setState({password: password}); 
    }

    
    handleChangeIsRememberMe(event) {
        this.setState({isRememberMe: event.target.checked});
    }
    
    handleSubmit(event) {
        event.preventDefault();
        this.props.onLogin({
            Email : this.state.username,
            Password : this.state.password,
            RememberMe : this.state.isRememberMe
        });
    }

    componentDidMount(){
        const { isAuthorized, onAuthorized } = this.props;
        if (isAuthorized)
            onAuthorized();
    }

    render(){
        const { isAuthorized } = this.props;
        return(
            isAuthorized ?
            <div className="d-flex justify-content-center align-content-center ">
                
                <div className="loader" style={{ width: "120px", height: "120px"}}></div>
            </div> :
            <div className="container">
                <div className="row">
                    <div className="offset-2 col-7">
                        <br />
                        <h2>Вход</h2>
                        <div className="errors" name="validServerErrors">
                            {this.props.validServerErrors}
                        </div>
                        <form method="post" onSubmit={this.handleSubmit}>
                            <div className="form-group">
                                <label className="control-label">Имя пользователя:</label><br />
                                <input value={this.state.username} type="text" className="form-control" onChange={this.handleChangeUserName} />
                                <span className="text-danger">{this.state.usernameValidError}</span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">Пароль:</label><br />
                                <input value={this.state.password} type="password" className="form-control" onChange={this.handleChangePassword} />
                                <span className="text-danger">{this.state.passwordValidError}</span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">Запомнить: </label>
                                <input style={{margin: "0px 0px 0px 5px"}} type="checkbox" value={this.state.isRememberMe} onChange={this.handleChangeIsRememberMe} />
                            </div>
                            <Link to={"/registration"} className="btn btn-info">Регистрация</Link>
                            <input type="submit" className="btn btn-success float-right" value="Войти" />
                        </form>
                    </div>
                </div>
            </div>);
    }
}
const mapStateToProps = (state) => ({
    isAuthorized : state.isAuthorized,
    validServerErrors : state.validServerError
});

const mapDispatchToProps = (dispatch) => ({
    onAuthorized() {
        dispatch(push("/"));
        dispatch(clearValidServerError());
    },
    onLogin(loginInfo){
        dispatch(loginAction(loginInfo));
    }
});
  
export default connect(mapStateToProps, mapDispatchToProps)(LoginComponent);