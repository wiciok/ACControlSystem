import React from 'react';
import { NavLink } from 'react-router-dom';
import 'bulma/css/bulma.css';

const Navbar = (props) => {
    let menu = props.userEmail
        ? <div className="navbar-menu is-active">
            <div className="navbar-end">
                <a className="navbar-item is-tab" href={null}>{`Użytkownik: ${props.userEmail}`}</a>
                <a className="navbar-item is-tab" href={null} onClick={props.onLogoutClick}>Wyloguj</a>
            </div>
        </div>
        : null;

    return (
        <nav className="navbar has-shadow" aria-label="main navigation">
            <div className="container">
                <div className="navbar-brand">
                    <a className="navbar-item is-tab" href="index.html" >
                        
                    </a>
                    <NavLink to="/index" className="navbar-item is-tab">
                        System sterowania klimatyzacją
                    </NavLink>
                </div>
                {menu}
            </div>
        </nav >
    );
}

export default Navbar;