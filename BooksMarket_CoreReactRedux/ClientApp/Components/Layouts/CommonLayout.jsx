import Header from "./Header";
import Footer from "./Footer";
import React from "react";
import "../css/CommonStyle.css";

const CommonTemplate = ({ children }) => (
    <div id="main-container">
        <Header />
        <div className="container-fluid main-container">
            {children}
        </div>
        <Footer />
    </div>
);

export default CommonTemplate;
