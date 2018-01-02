import React, {Component, Fragment} from 'react';
import 'bulma/css/bulma.css';

import MainMenuItem from "./MainMenuItem";

class MainMenuCategory extends Component {
    constructor(props) {
        super(props);
        this.name = props.name;
        this.items = props.items;
    }

    render() {
        return (
            <Fragment>
                <p className="menu-label">{this.name}</p>
                <ul className="menu-list">
                    {this
                        .items
                        .map((item, index) => (<MainMenuItem key={index} text={item.name} link={item.link}/>))}
                </ul>
            </Fragment>
        );
    }
}

export default MainMenuCategory;