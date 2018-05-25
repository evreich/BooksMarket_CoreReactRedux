import HeaderMenu from "./HeaderMenu";
import { connect } from "react-redux";

const mapStateToProps = state => {
    const menuElements = state.user 
        ? state.user.role.headerMenuElements 
        : [{title: "BookMarket", path: "/login"}]; 
    const location = state.router.location.pathname;    
    return { menuElements, location };
};

const HeaderMenuContainer = connect(mapStateToProps)(HeaderMenu);

export default HeaderMenuContainer;
