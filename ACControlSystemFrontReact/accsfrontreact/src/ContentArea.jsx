import React, {Component, Fragment} from 'react';
import {Route} from 'react-router-dom';

import 'bulma/css/bulma.css';

class ContentArea extends Component {
    constructor(props) {
        super(props);
        this.componentList = this.props.componentList;
    }

    render() {
        return (
            <Fragment>
                {this
                    .componentList
                    .map(item => (item.map((innerItem, index) => (<Route
                        key={index}
                        exact
                        path={innerItem.link}
                        component={innerItem.componentType}/>))))}
            </Fragment>
        );
    }
}

export default ContentArea;