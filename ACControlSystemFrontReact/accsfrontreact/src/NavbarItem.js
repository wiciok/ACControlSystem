import React, { Component } from 'react';
import 'bulma/css/bulma.css';

class NavbarItem extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <a
                    className="navbar-item is-tab"
                    href={this.props.link}
                >
                    {this.props.text}
                </a>
            </div>
        );
    }
}

export default NavbarItem;