import MenuList from "./MenuList";
import React from "react";
import "./Menu.css";
import "../../css/Loader.css";

class Menu extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            menuLoaded : false
        };
    }

    componentDidMount(){
        this.props.onLoadedMenu(() =>
            this.setState({menuLoaded : true})
        );
    }

    render() {
        return(
            <div className="col-sm-3 col-md-2 menu-leftside">
                <p className="menu-title">Выборка книг</p>
                {
                    (this.state.menuLoaded && this.props.menuElements) ?
                        <MenuList menuElements={this.props.menuElements} /> :
                    <div className="d-flex justify-content-center">
                        <br />
                        <div className="loader" style={{ width: "60px", height: "60px"}}></div>
                    </div>
                }
            </div>
        );
    }
}

export default Menu;