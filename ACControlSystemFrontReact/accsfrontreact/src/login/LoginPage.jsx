import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import EmailInput from '../content-components/user-manage/EmailInput';
import PasswordInput from '../content-components/user-manage/PasswordInput';

class LoginPage extends Component {
    render() {
        return (
            <div className="columns">
                <div className="column is-3">
                    <aside className="menu">
                        <p className="menu-label">Logowanie</p>
                        <ul className="menu-list">
                            <NavLink to='/login' activeClassName="is-active">
                                Logowanie
                            </NavLink>
                        </ul>
                    </aside>
                </div>
                <div className="column is-9">
                    <h2 className="title is-2">Logowanie</h2>
                    <div className="box">
                        <EmailInput/>
                        <PasswordInput/>
                    </div>
                </div>
            </div>
        );
    }
}

export default LoginPage;