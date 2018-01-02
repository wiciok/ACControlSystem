import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import 'bulma/css/bulma.css';

class MainMenuItem extends Component {
    render() {
        return (
            <li>
                <NavLink
                    to={this.props.link}
                    activeClassName="is-active"
                >
                    {this.props.text}
                </NavLink>
            </li>
        );
    }
}

export default MainMenuItem;