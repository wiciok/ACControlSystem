import React, { Component } from 'react';
import 'bulma/css/bulma.css';

class Navbar extends Component {
    render() {
        return (
            <nav className="navbar has-shadow" aria-label="main navigation">
                <div className="container">
                    <div className="navbar-brand">
                        <a className="navbar-item is-tab" href="index.html" >
                            System sterowania klimatyzacją
                            </a>
                    </div>
                    <div className="navbar-menu is-active">
                        <div className="navbar-end">
                            <a className="navbar-item is-tab" href={null}>
                                {`Użytkownik: ${this.props.userEmail}`}
                            </a>
                            <a className="navbar-item is-tab" href={null}>
                                Wyloguj
                            </a>
                        </div>
                    </div>
                </div>
            </nav >
        );
    }
};

export default Navbar;