import React, { Component } from 'react';
import 'bulma/css/bulma.css';

class Footer extends Component {
    render() {
        return (
            <footer className="footer">
                <div className="container">
                    <div className="content has-text-centered">
                        <p>
                            Witold Karaś, 2017
                        </p>
                    </div>
                </div>
            </footer>
        );
    }
};

export default Footer;