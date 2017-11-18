import React, { Component } from 'react';

class Home extends Component {
    constructor(props) {
        super(props);
        this.name = "Home";
        this.menuName = "Strona główna";
        this.link = "/";
    }

    render() {
        return (
            <div>
                Home component
            </div>
        );
    }
}

export default Home;