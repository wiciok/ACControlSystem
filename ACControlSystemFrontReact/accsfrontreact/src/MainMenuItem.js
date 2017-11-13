import React, { Component } from 'react';
import 'bulma/css/bulma.css';

class MainMenuItem extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <li>
                <a href={this.props.link}>
                    {this.props.text}
                </a>
            </li>
        );
    }
}

export default MainMenuItem;