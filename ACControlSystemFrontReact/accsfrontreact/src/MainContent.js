import React, { Component } from 'react';
import 'bulma/css/bulma.css';

class MainContent extends Component {
    render() {
        return (
            <section className="section" id="main-content-section">
                <div className="container">
                    <div className="columns">
                        <div className="column is-3">
                            <aside className="menu">
                                <p className="menu-label">System</p>
                                <ul className="menu-list">
                                    <li><a className="is-active" href="../index.html">Strona główna</a></li>
                                </ul>
                                <p className="menu-label">Użytkownicy</p>
                                <ul className="menu-list">
                                    <li><a>Ustawienia konta</a></li>
                                </ul>
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
                        </div>
                        <div className="column is-9">
                            <a className="has-text-black">
                            </a>
                        </div>
                    </div>
                </div>
            </section>
        );
    }
}

export default MainContent;