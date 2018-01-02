import React, {Component, Fragment} from 'react';
import {BrowserRouter} from 'react-router-dom';
import '../node_modules/bulma/css/bulma.css';

import Navbar from "./navbar/Navbar";
import Footer from "./Footer";
import ContentArea from "./ContentArea";
import MainMenu from "./main-menu/MainMenu";
import MainMenuCategoriesAndItems from './main-menu/MainMenuCategoriesAndItems';

class App extends Component {
    render() {
        return (
            <BrowserRouter>
                <Fragment>
                    <Navbar/>
                    <section className="section" id="main-content-section">
                        <div className="container">
                            <div className="columns">
                                <div className="column is-3">
                                    <MainMenu
                                        categories={(new MainMenuCategoriesAndItems()).menuCategoriesAndItems}/>
                                </div>
                                <div className="column is-9">
                                    <ContentArea
                                        componentList={(new MainMenuCategoriesAndItems()).menuCategoriesAndItems.map((item) => item.contentComponentInfoList)}/>
                                </div>
                            </div>
                        </div>
                    </section>
                    <Footer/>
                </Fragment>
            </BrowserRouter>
        );
    }
}

export default App;