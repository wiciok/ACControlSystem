import React, { Component } from 'react';

class AcState extends Component {
    constructor(props) {
        super(props);
        this.name = "AcState";
        this.menuName = "Stan klimatyzatora";
        this.link = "/acstate";

        this.rstatus = "";
    };

    fetchInfo() {
        alert("test");
        fetch('http://localhost:54060/api/acstate')
            .then(function (response) {
                if (response.status === 204) {
                    alert(response.status);
                    this.rstatus = "NoContent";
                }
            })
            .catch(err => {
                alert(err);
                alert('error');
            })
    };


    render() {
        return (
            <div>
                AcState: {this.rstatus}
                <br />
                <button onClick={this.fetchInfo}>
                    test</button>
            </div>
        );
    }
};

export default AcState;