import App from "./Base/App";
import React from "react";
import { render } from "react-dom";
import { stateCreator } from "./State/StateCreator";
import { Provider } from "react-redux";
import { ConnectedRouter } from "react-router-redux";
import createHistory from "history/createBrowserHistory";
import "../node_modules/bootstrap/dist/css/bootstrap.css";
import "../node_modules/bootstrap/dist/js/bootstrap";
import "./index.css";

const history = createHistory();

const store = stateCreator(history);

render(
    <Provider store={store}>
        <ConnectedRouter history={history} >
            <App />
        </ConnectedRouter>
    </Provider>,
    document.getElementById("react-container")
);
