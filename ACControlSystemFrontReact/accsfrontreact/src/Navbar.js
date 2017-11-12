import React, { Component } from 'react';
import 'bulma/css/bulma.css';

class Navbar extends Component {
    render() {
        return (
            /*<nav className="nav is-dark has-shadow">
                <div className="container">
                    <div className="nav-left">
                        <a className="nav-item" href="../index.html">
                            <h1>System sterowania klimatyzacją</h1>
                        </a>
                    </div>
                    <span className="nav-toggle">
                        <span></span>
                        <span></span>
                        <span></span>
                    </span>
                    <div className="nav-right nav-menu">
                        <a className="nav-item">Użytkownik: test</a>
                        <a className="nav-item is-tab is-active">
                            Wyloguj
                        </a>
                    </div>
                </div>
            </nav>*/
            <nav className="navbar" role="navigation" aria-label="main navigation">
                <div className="navbar-brand">
                    <a className="navbar-item" href="https://bulma.io">
                        <img src="https://bulma.io/images/bulma-logo.png" alt="Bulma: a modern CSS framework based on Flexbox" width="112" height="28"></img>
                    </a>
                </div>
            </nav>
        );
    }
};

export default Navbar;