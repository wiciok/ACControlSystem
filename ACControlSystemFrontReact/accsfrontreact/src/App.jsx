import React, { Component, Fragment } from 'react';
import { BrowserRouter } from 'react-router-dom';
import Cookies from 'js-cookie';
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
            userPasswordHash: null
        };
    }

    componentWillMount() {
        this.setState({
            userEmail: Cookies.get('userEmail') || null,
            userPasswordHash: Cookies.get('userPasswordHash') || null
        });
    }


    onLogin(email, passHash, token) {
        let inHour = 1/24;
        let cookieOptions = {
            path: '/',
            expires: inHour
        }

        Cookies.set('userEmail', email, cookieOptions);
        Cookies.set('userPasswordHash', passHash, cookieOptions);
        Cookies.set('token', token);

        this.setState({
            userEmail: email,
            userPasswordHash: passHash
        });
    }

    onLogout() {
        Cookies.remove('userEmail');
        Cookies.remove('userPasswordHash');
        Cookies.remove('token');

        this.setState({
            userEmail: null,
            userPasswordHash: null
        });
    }

    render() {
        let content;
        if (!this.state.userEmail && !this.state.userPasswordHash)
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

export default App;