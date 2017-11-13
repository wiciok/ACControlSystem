import React, { Component } from 'react';
import 'bulma/css/bulma.css';

import MainMenu from "./MainMenu.js"
import ContentArea from "./ContentArea"

class MainArea extends Component {
    render() {
        return (
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
        );
    }
}

export default MainArea;