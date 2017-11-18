import React, { Component } from 'react';
import { Route } from 'react-router-dom';

import 'bulma/css/bulma.css';

class ContentArea extends Component {
    constructor(props) {
        super(props);
        this.componentList = this.props.componentList;
    }

    render() {
        return (
            <div>
                {this.componentList.map((item) => (
                    item.map((innerItem) => (
                        <Route exact path={innerItem.link} component={innerItem.componentType} />
                    ))
                ))}
            </div>
        );
    }
}

export default ContentArea;