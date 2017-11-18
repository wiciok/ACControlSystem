import React, { Component } from 'react';
import { Route } from 'react-router-dom';

import Home from './content-components/Home';

import 'bulma/css/bulma.css';

class ContentArea extends Component {
    render() {
        return (
            <div>
                <Route exact path="/" component={Home} />
            </div>
        );
    }
}

export default ContentArea;