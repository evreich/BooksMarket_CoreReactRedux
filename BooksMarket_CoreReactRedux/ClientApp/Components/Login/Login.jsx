import React from "react";
import { connect } from "react-redux";
import { push } from "react-router-redux";
import { Link } from "react-router-dom";
import { loginAction } from "../../Actions/UsersActions";
import { clearValidServerError } from "../../Actions/CommonActions";
import * as validators from "../../Utils/Validators";
import "../css/CommonStyle.css";
import Loading from "../Layouts/Loading";

class LoginComponent extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            disabledEnter: true,
            username: "",
            usernameValidError: "",
            password: "",
            passwordValidError: "",
            isRememberMe: false
        };
    }

    componentDidUpdate = () => {
        this.setState({ disabledEnter : this.isNotAccessBtn() });
    };

    isNotAccessBtn = () => {
         return !((this.state.usernameValidError.length === 0 && this.state.username.length !== 0 ) &&
            (this.state.passwordValidError.length === 0 && this.state.password.length !== 0));
    };

    handleChangeUserName = event => {
        const username = event.target.value;
        let usernameValidError = "";

        [validators.isRequired, validators.isValidUserName].every(validator => {
            const error = validator(username);
            if (!error) return true;
            else {
                usernameValidError = error;
                return false;
            }
        });

        this.setState({
            usernameValidError,
            username
        });
    };

    handleChangePassword = event => {
        const password = event.target.value;
        let passwordValidError = "";

        [validators.isRequired, validators.isMinLength5].every(validator => {
            const error = validator(password);
            if (!error) return true;
            else {
                passwordValidError = error;
                return false;
            }
        });

        this.setState({
            passwordValidError,
            password
        });
    };

    handleChangeIsRememberMe = event => {
        this.setState({ isRememberMe: event.target.checked });
    };

    handleSubmit = event => {
        event.preventDefault();
        const { username, password, isRememberMe } = this.state;
        this.props.onLogin({
            Email: username,
            Password: password,
            RememberMe: isRememberMe
        });
    };

    componentDidMount() {
        const { isAuthorized, onAuthorized } = this.props;
        if (isAuthorized) onAuthorized();
    }

    render() {
        const { isAuthorized, validServerErrors } = this.props;
        const {
            username,
            usernameValidError,
            password,
            passwordValidError,
            isRememberMe,
            disabledEnter
        } = this.state;
        return isAuthorized ? <Loading /> : (
            <div className="container">
                <div className="row">
                    <div className="offset-2 col-7">
                        <br />
                        <h2>Вход</h2>
                        <div className="errors" name="validServerErrors">
                            {validServerErrors}
                        </div>
                        <form method="post" onSubmit={this.handleSubmit}>
                            <div className="form-group">
                                <label className="control-label">
                                    Имя пользователя:
                                </label>
                                <br />
                                <input
                                    value={username}
                                    type="text"
                                    className="form-control"
                                    onChange={this.handleChangeUserName}
                                />
                                <span className="text-danger">
                                    {usernameValidError}
                                </span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">Пароль:</label>
                                <br />
                                <input
                                    value={password}
                                    type="password"
                                    className="form-control"
                                    onChange={this.handleChangePassword}
                                />
                                <span className="text-danger">
                                    {passwordValidError}
                                </span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">
                                    Запомнить:{" "}
                                </label>
                                <input
                                    style={{ margin: "0 0 0 5px" }}
                                    type="checkbox"
                                    value={isRememberMe}
                                    onChange={this.handleChangeIsRememberMe}
                                />
                            </div>
                            <Link to={"/registration"} className="btn btn-info">
                                Регистрация
                            </Link>
                            <input
                                disabled={disabledEnter}
                                type="submit"
                                className="btn btn-success float-right"
                                value="Войти"
                            />
                        </form>
                    </div>
                </div>
            </div>
        );
    }
}
const mapStateToProps = state => ({
    isAuthorized: state.isAuthorized,
    validServerErrors: state.validServerError
});

const mapDispatchToProps = dispatch => ({
    onAuthorized() {
        dispatch(push("/"));
        dispatch(clearValidServerError());
    },
    onLogin(loginInfo) {
        dispatch(loginAction(loginInfo));
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(LoginComponent);
