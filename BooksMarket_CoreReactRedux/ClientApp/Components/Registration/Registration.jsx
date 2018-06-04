import React from "react";
import { connect } from "react-redux";
import { push } from "react-router-redux";
import { registrationAction } from "../../Actions/UsersActions";
import { clearValidServerError } from "../../Actions/CommonActions";
import * as validators from "../../Utils/Validators";
import "../css/CommonStyle.css";

class RegistrationComponent extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            disabledEnter: true,
            username: "",
            usernameValidError: "",
            fullname: "",
            fullnameValidError: "",
            phone: "",
            phoneValidError: "",
            password: "",
            passwordValidError: "",
            passwordRepeat: "",
            passwordRepeatValidError: "",
        };
    }

    componentDidUpdate = () => {
        this.setState({ disabledEnter : this.isNotAccessBtn() });
    };

    isNotAccessBtn = () => {
         return !((this.state.usernameValidError.length === 0 && this.state.username.length !== 0 ) &&
            (this.state.fullnameValidError.length === 0 && this.state.fullname.length !== 0) &&
            (this.state.phoneValidError.length === 0 && this.state.phone.length !== 0) &&
            (this.state.passwordValidError.length === 0 && this.state.password.length !== 0) &&
            (this.state.passwordRepeatValidError.length === 0 && this.state.passwordRepeat.length !== 0));
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

    handleChangeFullName = event => {
        const fullname = event.target.value;
        let fullnameValidError = "";

        [validators.isRequired, validators.isValidFullName].every(validator => {
            const error = validator(fullname);
            if (!error) return true;
            else {
                fullnameValidError = error;
                return false;
            }
        });

        this.setState({
            fullnameValidError,
            fullname
        });
    };

    handleChangePhone = event => {
        const phone = event.target.value;
        let phoneValidError = "";

        [validators.isRequired, validators.isValidPhone].every(validator => {
            const error = validator(phone);
            if (!error) return true;
            else {
                phoneValidError = error;
                return false;
            }
        });

        this.setState({
            phoneValidError,
            phone
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

    handleChangeRepeatPassword = event => {
        const password = this.state.password;
        const passwordRepeat = event.target.value;
        let passwordRepeatValidError = "";

        const error = validators.isEqualWithPass(password,passwordRepeat);
        if (error) passwordRepeatValidError = error;
        
        this.setState({
            passwordRepeatValidError,
            passwordRepeat
        });
    };

    handleSubmit = event => {
        event.preventDefault();
        const { username, password, passwordRepeat, fullname, phone } = this.state;
        this.props.onRegistration({
            Email: username,
            FullName: fullname,
            Phone: phone,
            Password: password,
            PasswordConfirm: passwordRepeat
        });
    };

    componentDidMount() {
        const { isAuthorized, onAuthorized } = this.props;
        if (isAuthorized) onAuthorized();
    }

    render() {
        const { isAuthorized, validServerErrors } = this.props;
        const {
            disabledEnter,
            username,
            usernameValidError,
            fullname,
            fullnameValidError,
            phone,
            phoneValidError,
            password,
            passwordValidError,
            passwordRepeat,
            passwordRepeatValidError
        } = this.state;
        return isAuthorized ? (
            <div className="d-flex justify-content-center align-content-center mt-4 ">
                <div
                    className="loader"
                    style={{ width: "120px", height: "120px" }}
                />
            </div>
        ) : (
            <div className="container">
                <div className="row">
                    <div className="offset-2 col-7">
                        <br />
                        <h2>Регистрация</h2>
                        <div className="errors" name="validServerErrors">
                            {validServerErrors}
                        </div>
                        <form method="post" onSubmit={this.handleSubmit}>
                            <div className="form-group">
                                <label className="control-label">
                                    Email:
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
                                <label className="control-label">
                                    ФИО пользователя:
                                </label>
                                <br />
                                <input
                                    value={fullname}
                                    type="text"
                                    className="form-control"
                                    onChange={this.handleChangeFullName}
                                />
                                <span className="text-danger">
                                    {fullnameValidError}
                                </span>
                            </div>
                            <div className="form-group">
                                <label className="control-label">
                                    Телефон:
                                </label>
                                <br />
                                <input
                                    value={phone}
                                    type="text"
                                    className="form-control"
                                    onChange={this.handleChangePhone}
                                />
                                <span className="text-danger">
                                    {phoneValidError}
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
                                <label className="control-label">Повторите пароль:</label>
                                <br />
                                <input
                                    value={passwordRepeat}
                                    type="password"
                                    className="form-control"
                                    onChange={this.handleChangeRepeatPassword}
                                />
                                <span className="text-danger">
                                    {passwordRepeatValidError}
                                </span>
                            </div>
                            <input
                                disabled={disabledEnter}
                                type="submit"
                                className="btn btn-success btn-block"
                                value="Зарегистрироваться"
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
    onRegistration(registerInfo) {
        dispatch(registrationAction(registerInfo));
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(RegistrationComponent);
