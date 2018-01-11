import React, { Component } from 'react';
import { CookiesProvider } from 'react-cookie';
import App from './App';

class Root extends Component {
    render() {
        return (
            <CookiesProvider>
                <App />
            </CookiesProvider>
        );
    }
}

export default Root;