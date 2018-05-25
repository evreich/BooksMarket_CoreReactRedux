import { connect } from "react-redux";
import Menu from "./Menu";
import { getMenuItemsAction } from "../../../Actions/SideMenuActions";

const mapStateToProps = state => ({
    menuElements : state.sideMenuElements
});

const mapDispatchToProps = dispatch => ({
    onLoadedMenu(changeLoadState) {
        getMenuItemsAction()(dispatch).then(() => changeLoadState());
    }
});

const MenuContainer = connect(mapStateToProps, mapDispatchToProps)(Menu);

export default MenuContainer;