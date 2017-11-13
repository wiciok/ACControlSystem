import React, { Component } from 'react';
import 'bulma/css/bulma.css';

import MainMenuItem from "./MainMenuItem";

class MainMenuCategory extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <p className="menu-label">{this.props.categoryName}</p>
                <ul className="menu-list">
                    {this.props.menuItems.map((item, index) => (
                        <MainMenuItem
                            text={item.text}
                            link={item.link}
                        />
                    ))}
                </ul>
                <br />
            </div>
        );
    }
}

export default MainMenuCategory;