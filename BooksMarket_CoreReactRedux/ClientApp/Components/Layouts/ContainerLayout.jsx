import React from "react";
import MenuContainer from "./Menu";

const ContainerLayout = ({ isHaveMenu, children }) =>
    <div>
        <div className="row">
            {isHaveMenu ? <MenuContainer /> : ""}
            <div className="container body-content col-sm-9 col-md-10">
                <br/>
                {children}
            </div>
         </div>
    </div>;

export default ContainerLayout;