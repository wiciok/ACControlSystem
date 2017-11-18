import React, { Component } from 'react';
import 'bulma/css/bulma.css';
import './MainMenuCategory.css'

import MainMenuItem from "./MainMenuItem";

class MainMenuCategory extends Component {

    render() {
        return (
            <div className="menu-category">
                <p className="menu-label">{this.props.categoryName}</p>
                <ul className="menu-list">
                    {this.props.menuItems.map((item, index) => (
                        <MainMenuItem
                            text={item.text}
                            link={item.link}
                        />
                    ))}
                </ul>
            </div>
        );
    }
}

export default MainMenuCategory;