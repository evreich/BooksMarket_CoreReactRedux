import React from "react";
import { Link, NavLink } from "react-router-dom";
import "./Header.css";
import PropTypes from "prop-types";

const HeaderMenu = ({ menuElements }) => {
    const logoItem = menuElements.find(elem => elem.title === "BookMarket");
    const activeStyleElem = {
        color: "white"
    };
    return (
        <div className="col-7">
            <ul>
                <li className="navbar-brand">
                    <Link to={logoItem.path}>{logoItem.title}</Link>
                </li>
                {menuElements.filter((el) => el.title !== logoItem.title)
                    .map((menuItem) => 
                        <li key={menuItem.id} className="navbar-item">
                            <NavLink activeStyle={activeStyleElem} to={menuItem.path}>{menuItem.title}</NavLink>
                        </li>
                )}
            </ul>
        </div>
    );
};

HeaderMenu.propTypes = {
    menuElements: PropTypes.arrayOf(PropTypes.object).isRequired
};

export default HeaderMenu;
