import HeaderMenuContainer from "./HeaderMenuContainer";
import HeaderLogoutContainer from "./HeaderLogoutContainer";
import "./Header.css";
import React from "react";

const Header = () => (
    <nav className="navbar navbar-toggleable-md navbar-inverse bg-inverse navbar-custom fixed-top">
        <div className="navbar-collapse">
            <div className="row">
                <HeaderMenuContainer />
                <HeaderLogoutContainer />
            </div>
        </div>
    </nav>
);

export default Header;
