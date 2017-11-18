import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import 'bulma/css/bulma.css';

class MainMenuItem extends Component {
    render() {
        return (
            <li>
                <Link to={this.props.link}>
                    {this.props.text}
                </Link>
            </li>
        );
    }
}

export default MainMenuItem;