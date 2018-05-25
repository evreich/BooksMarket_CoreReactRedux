import React from "react";
import "../../css/CommonStyle.css";

const Footer = () => {
    const currYear = new Date().getFullYear();
    return(<div className="navbar navbar-custom fixed-bottom">
        <footer>
            <p>{currYear} - BooksMarket</p>
        </footer>
    </div>
    );
};

export default Footer;
