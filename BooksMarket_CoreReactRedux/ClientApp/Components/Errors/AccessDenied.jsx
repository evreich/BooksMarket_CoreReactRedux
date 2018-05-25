import React from "react";
import "./Errors.css";

const AccessDenied = () => (
    <div className="errorContainer">
        {`К сожалению, доступ к данный странице невозможен. 
          Вы не обладаете достаточными правами. 
          Обратитесь в службу поддержки.`}
    </div>
);

export default AccessDenied;
