import React, { Component } from 'react';
import Navbar from "./Navbar.js";
import Footer from "./Footer.js";
import MainContent from "./MainContent.js";
import '../node_modules/bulma/css/bulma.css';

class App extends Component {
    render() {
        return (
            <div>
                <Navbar />
                <MainContent />
                <Footer />
            </div>
        );
    }
}

export default App;