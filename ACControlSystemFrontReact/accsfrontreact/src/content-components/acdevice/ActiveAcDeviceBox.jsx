import React, { Component } from 'react';


class ActiveAcDeviceBox extends Component {
    constructor(props) {
        super(props);

        this.getActiveAcDevice = this.getActiveAcDevice.bind(this);

        this.state = {
            activeAcDevice: null
        }
    }

    componentDidMount() {
        this.getActiveAcDevice();
    }

    getActiveAcDevice() {
        let endpointAddress = `${window.apiAddress}/acdevice`;
        let fullAddress = endpointAddress.concat("/123temporaryfaketoken/current");
        console.log(fullAddress);

        fetch(fullAddress)
            .then(response => {
                console.log("get active ac device response: " + response.status);
                let error;

                if (!response.ok) {
                    error = new Error(response.statusText);
                    error.statusCode = response.status;

                    //todo: poprawic to/usunac
                    if (response.bodyUsed) {
                        response.json()
                            .then(x => {
                                console.log(x);
                                error.errorMessage = x;
                                throw error;
                            });
                    };
                    throw error;
                }

                response.json()
                    .then(json => {
                        console.log(json);
                        this.setState({
                            activeAcDevice: json
                        })
                    })
                    .catch(err => {
                        error = new Error("Blad deserializacji odpowiedzi serwera do formatu JSON");
                    });


            })
            .catch(err => {
                //console.log("error in toggle state: "+err);
                this.props.errorCallback(err);
            });

    }

    render() {
        let activeAcDeviceSpan;
        if (this.state.activeAcDevice) {
            activeAcDeviceSpan = <span className="tag is-success is-medium">{`${this.state.activeAcDevice.brand} ${this.state.activeAcDevice.model} Id: ${this.state.activeAcDevice.id}`}</span>
        }
        else {
            activeAcDeviceSpan = <span className="tag is-danger is-medium">Brak</span>
        }


        return (
            <div className="box">
                <span>
                    <span className="title is-4">Aktywny klimatyzator: </span>
                    {activeAcDeviceSpan}
                </span>
            </div>
        );
    }
}

export default ActiveAcDeviceBox;