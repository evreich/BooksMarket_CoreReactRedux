import HeaderLogout from "./HeaderLogout";
import { logoutAction } from "../../../Actions/UsersActions";
import { connect } from "react-redux";

const mapStateToProps = state => {
    const username = state.user ? state.user.name : undefined; 
    const token = state.user ? state.user.token : undefined;
    return { username, token };
};

const mapDispatchProps = dispatch => ({
    onLogout(token) {
        dispatch(logoutAction(token));
    }
});

const HeaderLogoutContainer = connect(mapStateToProps, mapDispatchProps)(
    HeaderLogout
);

export default HeaderLogoutContainer;
