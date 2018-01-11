import React, { Component, Fragment } from 'react';
import { BrowserRouter } from 'react-router-dom';

import { instanceOf } from 'prop-types';
import { withCookies, Cookies } from 'react-cookie';

import '../node_modules/bulma/css/bulma.css';

import Navbar from "./navbar/Navbar";
import Footer from "./Footer";
import ContentArea from "./ContentArea";
import MainMenu from "./main-menu/MainMenu";
import MainMenuCategoriesAndItems from './main-menu/MainMenuCategoriesAndItems';
import LoginPage from './login/LoginPage';

class App extends Component {
    constructor(props) {
        super(props);

        this.onLogin = this.onLogin.bind(this);
        this.onLogout = this.onLogout.bind(this);

        this.state = {
            userEmail: null,
            userPassword: null
        };
    }

    static propTypes = {
        cookies: instanceOf(Cookies).isRequired
    };

    componentWillMount() {
        const { cookies } = this.props;

        this.setState({
            userEmail: cookies.get('userEmail') || null,
            userPassword: cookies.get('userPassword') || null
        });
    }


    onLogin(email, pass) {
        const { cookies } = this.props;

        let cookieOptions = {
            path: '/',
            maxAge: 3600
        }

        cookies.set('userEmail', email, cookieOptions);
        cookies.set('userPassword', pass, cookieOptions);

        this.setState({
            userEmail: email,
            userPassword: pass
        });
    }

    onLogout() {
        const { cookies } = this.props;

        cookies.remove('userEmail');
        cookies.remove('userPassword');

        this.setState({
            userEmail: null,
            userPassword: null
        });
    }

    render() {
        let content;
        if (!this.state.userEmail && !this.state.userPassword)
            content = <LoginPage onLogin={this.onLogin} />;
        else
            content =
                <div className="columns">
                    <div className="column is-3">
                        <MainMenu categories={(new MainMenuCategoriesAndItems()).menuCategoriesAndItems} />
                    </div>
                    <div className="column is-9">
                        <ContentArea componentList={(new MainMenuCategoriesAndItems()).menuCategoriesAndItems.map((item) => item.contentComponentInfoList)} />
                    </div>
                </div>

        return (
            <BrowserRouter>
                <Fragment>
                    <Navbar
                        userEmail={this.state.userEmail}
                        onLogoutClick={this.onLogout} />
                    <section className="section" id="main-content-section">
                        <div className="container">
                            {content}
                        </div>
                    </section>
                    <Footer />
                </Fragment>
            </BrowserRouter>
        );
    }
}

export default withCookies(App);