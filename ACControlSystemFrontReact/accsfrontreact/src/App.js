import React, { Component } from 'react';
import '../node_modules/bulma/css/bulma.css';

import Navbar from "./Navbar.js";
import Footer from "./Footer.js";
import MainArea from "./MainArea"


class App extends Component {
    render() {
        return (
            <div>
                <Navbar />
                <MainArea />
                <Footer />
            </div>
        );
    }
}

export default App;