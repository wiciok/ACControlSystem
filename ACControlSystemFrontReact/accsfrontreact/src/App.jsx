import React, { Component } from 'react';
import { BrowserRouter } from 'react-router-dom';
import '../node_modules/bulma/css/bulma.css';

import Navbar from "./Navbar";
import Footer from "./Footer";
import ContentArea from "./ContentArea";
import MainMenu from "./MainMenu";


class App extends Component {
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
                                        <MainMenu />
                                    </div>
                                    <div className="column is-9">
                                        <ContentArea />
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