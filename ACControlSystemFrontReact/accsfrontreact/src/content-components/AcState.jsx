import React, { Component } from 'react';

class AcState extends Component {
    constructor(props) {
        super(props);
        this.name = "AcState";
        this.menuName = "Stan klimatyzatora";
        this.link = "/acstate";
    }

    render() {
        return (
            <div>
                AcState
            </div>
        );
    }
}

export default AcState;