import React, {Component} from 'react';
import 'bulma/css/bulma.css';
import NavbarItem from "./NavbarItem"

class Navbar extends Component {
    render() {
        return (
            <nav className="navbar has-shadow" aria-label="main navigation">
                <div className="container">
                    <div className="navbar-brand">
                        <NavbarItem link="index.html" text="System sterowania klimatyzacją"/>
                    </div>

                    <div className="navbar-menu">
                        <div className="navbar-end">
                            <NavbarItem text="Użytkownik: test"/>

                            <NavbarItem text="Wyloguj"/>
                        </div>
                    </div>
                </div>
            </nav >
        );
    }
};

export default Navbar;