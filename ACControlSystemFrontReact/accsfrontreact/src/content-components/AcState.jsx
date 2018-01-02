import React, { Component } from 'react';
import 'bulma/css/bulma.css';

import CurrentStateTag from './CurrentStateTag';
import ToggeStateButton from './ToggleStateButton';

class AcState extends Component {
    constructor(props) {
        super(props);

        this.state = { currentAcState: "unknown" };

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
                <h2 className="title is-2">Stan klimatyzatora</h2>
                <div className="box">
                    <div className="columns">
                        <div className="column">
                            <h4 className="title is-4">
                                Obecny stan klimatyzatora: &emsp;
                                <br /><br />
                                <CurrentStateTag tagState={this.state.currentAcState} />
                            </h4>
                        </div>
                        <div className="column">
                            <h4 className="title is-4">
                                Sterowanie ręczne: &emsp;
                                <br /><br />
                                <ToggeStateButton actionType="on" />
                                <br /><br />
                                <ToggeStateButton actionType="off" />
                            </h4>
                        </div>
                    </div>
                </div>


            </div>
        );
    }
};

export default AcState;