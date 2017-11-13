import React, { Component } from 'react';
import 'bulma/css/bulma.css';

import MainMenuCategory from './MainMenuCategory'

class MainMenu extends Component {
    constructor(props) {
        super(props);
    }

    categories =
        [
            {
                name: "System",
                menuItems: [
                    { text: "Strona główna", link: "../index.html" }
                ]
            },

            {
                name: "Użytkownicy",
                menuItems: [
                    { text: "Ustawienia konta", link: "../index.html" }
                ]
            }
        ];


    render() {
        return (
            <aside className="menu">
                <MainMenuCategory
                    categoryName={this.categories[0].name}
                    menuItems={this.categories[0].menuItems}
                />

                <MainMenuCategory
                    categoryName={this.categories[1].name}
                    menuItems={this.categories[1].menuItems}
                />

                <p className="menu-label">Klimatyzator</p>
                <ul className="menu-list">
                    <li><a>Planowanie</a></li>
                    <li>
                        <a>Profile ustawień</a>
                        <ul>
                            <li><a>Dodaj nowy</a></li>
                            <li><a>Zarządzaj</a></li>
                        </ul>
                    </li>
                </ul>
                <p className="menu-label">Administracja</p>
                <ul className="menu-list">
                    <li><a>Konta użytkowników</a></li>
                </ul>
            </aside>
        );
    }
}

export default MainMenu;