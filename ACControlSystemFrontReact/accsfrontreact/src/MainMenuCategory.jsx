import React, { Component } from 'react';
import 'bulma/css/bulma.css';
import './MainMenuCategory.css'

import MainMenuItem from "./MainMenuItem";

class MainMenuCategory extends Component {
    constructor(props) {
        super(props);
        this.name = props.name;
        this.items = props.items;
    }


    render() {
        return (
            <div className="menu-category">
                <p className="menu-label">{this.name}</p>
                <ul className="menu-list">
                    {this.items.map((item) => (
                        <MainMenuItem
                            text={item.name}
                            link={item.link}
                        />
                    ))}
                </ul>
            </div>
        );
    }
}

export default MainMenuCategory;