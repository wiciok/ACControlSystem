import React, { Component } from 'react';
import { BrowserRouter } from 'react-router-dom';
import '../node_modules/bulma/css/bulma.css';

import Navbar from "./Navbar";
import Footer from "./Footer";
import ContentArea from "./ContentArea";
import MainMenu from "./MainMenu";

import Home from './content-components/Home';

class ContentComponentInfo {
    constructor(menuName, link, categoryId) {
        this.name = menuName;
        this.link = link;
        this.categoryId = categoryId;
    }
}


class App extends Component {
    //todo: wydzielic do osobnego pliku

    menuCategories =
        [
            {
                id: 1,
                name: "System",
                contentComponentInfoList: [new ContentComponentInfo("Home", "/", 1)]
            },
        ];


    render() {
        return (
            <BrowserRouter>
                <div>
                    <Navbar />
                    <div>
                        <section className="section" id="main-content-section">
                            <div className="container">
                                <div className="columns">
                                    <div className="column is-3">
                                        <MainMenu categories={this.menuCategories} />
                                    </div>
                                    <div className="column is-9">
                                        <ContentArea categories={this.menuCategories} />
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <Footer />
                </div>
            </BrowserRouter>
        );
    }
}

export default App;