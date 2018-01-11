import React, { Component, Fragment } from 'react';
import { BrowserRouter } from 'react-router-dom';
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

        this.onLogin=this.onLogin.bind(this);

        this.state = {
            userEmail: null,
            userPassword: null
        };
    }

    componentWillUpdate(nextProps, nextState) {
        if (window['userEmail'] !== nextState.userEmail)
            window['userEmail'] = nextState.userEmail;
        if (window['userPassword'] !== nextState.userPassword)
            window['userPassword'] = nextState.userPassword;
    }

    onLogin(email, pass){
        alert(`${email} : ${pass}`)

        this.setState({
            userEmail: email,
            userPassword: pass
        });
    }

    render() {
        let content;
        if (window['userEmail'] === "" || window['userPassword'] === "")
            content = <LoginPage onLogin={this.onLogin}/>;
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
                    <Navbar userEmail={this.state.userEmail} />
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