import React from "react";
import PropTypes from "prop-types";

const HeaderLogout = ({ onLogout, username, token }) => {
    function handleClick(e) {
        e.preventDefault();
        onLogout(token);
    }

    return (
        <div className="col-5">
            {username ? (
                <button
                    onClick={handleClick}
                    className="d-inline-block float-right btn"
                >
                    Выход
                </button>
            ) : (
                ""
            )}
            <p className="d-inline-block float-right mt-2 mr-4">{username ? username : ""}</p>
        </div>
    );
};

HeaderLogout.propTypes = {
    user: PropTypes.string,
    onLogout: PropTypes.func
};

export default HeaderLogout;
