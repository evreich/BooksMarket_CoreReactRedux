import "./Menu.css";
import React from "react";
import { NavLink } from "react-router-dom";
import PropTypes from "prop-types";

const MenuList = ({ menuElements }) => (
    menuElements.map((el, i) => (
        <NavLink key={i} to={ el.link } className="menu-item text-center">
            <i className="fas fa-book"></i>  { el.name }
        </NavLink>
    ))
);

MenuList.propTypes = {
    menuElements: PropTypes.arrayOf(PropTypes.object).isRequired
};

export default MenuList;
