import React, { Component, Fragment } from 'react';
import 'bulma/css/bulma.css';

import ErrorMessageComponent from '../../ErrorMessageComponent';
import AcDevicesTable from './AcDevicesTable';
import AcDeviceAddEditForm from './AcDeviceAddEditForm';
import ActiveAcDeviceBox from './ActiveAcDeviceBox';

class AcDevice extends Component {
    constructor(props) {
        super(props);

        this.onRowSelected = this.onRowSelected.bind(this);
        this.getAllAcDevices = this.getAllAcDevices.bind(this);
        this.setApiFetchError = this.setApiFetchError.bind(this);
        this.refresh = this.refresh.bind(this);

        this.endpointAddress = `${window.apiAddress}/acdevice`;

        this.state = {
            allDevicesData: null,
            activeDevice: null,
            selectedRow: null,
            error: {
                isError: false,
                errorMessage: null
            }
        };
    }

    componentDidMount() {
        this.refresh();
    }

    refresh() {
        this.getAllAcDevices();
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
                    if (response.status === 404)
                        return;

                    error = new Error(response.statusText);
                    error.statusCode = response.status;
                    response.json().then(data => {
                        console.log(data);
                        error.errorMessage = data;
                        this.setApiFetchError(error);
                    });
                    this.setApiFetchError(error);
                }

                response.json()
                    .then(json => {
                        console.log(json);
                        this.setState({
                            activeDevice: json
                        })
                    })
                    .catch(err => {
                        error = new Error("Blad deserializacji odpowiedzi serwera do formatu JSON");
                    });
            })
            .catch(err => {
                //console.log("error in toggle state: "+err);
                this.setApiFetchError(err);
            });

    }

    getAllAcDevices() {
        //let fullAddress = this.endpointAddress.concat("/all");
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken/all");
        console.log("get " + fullAddress);

        fetch(fullAddress).then(response => {
            console.log(response.status);
            switch (response.status) {
                case 200:
                    response
                        .json()
                        .then(json => {
                            console.log(json);
                            this.setState({
                                allDevicesData: json,
                                selectedRow: 0
                            })
                        })
                        .catch(err => {
                            this.setState({
                                error: {
                                    isError: true,
                                    errorMessage: "Blad deserializacji odpowiedzi serwera do formatu JSON"
                                }
                            });
                        });
                    break;
                default:
                    let error = new Error(response.statusText);
                    error.statusCode = response.status;

                    response.json().then(data => {
                        console.log(data);
                        error.errorMessage = data;
                        this.setApiFetchError(error);
                    });
                    this.setApiFetchError(error);
            }
        }).catch(err => {
            console.log(err);
            this.setApiFetchError(err);
        });
    }

    setApiFetchError(error) {
        let errorMessage = `${error.message}`;

        if (error.statusCode) 
            errorMessage = `Błąd ${error.statusCode}: `.concat(errorMessage);

        if (error.errorMessage) 
            errorMessage += "Dodatkowe informacje: " + error.errorMessage;

        this.setState({
            error: {
                isError: true,
                errorMessage: errorMessage
            }
        });
    }

    onRowSelected(rowId) {
        this.setState({ selectedRow: rowId });
    }

    render() {
        return (
            <Fragment>
                <h2 className="title is-2">Klimatyzatory</h2>

                <ErrorMessageComponent
                    isVisible={this.state.error.isError}
                    bodyText={this.state.error.errorMessage}
                    onChangeErrorState={e => {
                        this.setState({
                            error: {
                                isError: false,
                                errorMessage: null
                            }
                        })
                    }} />

                <ActiveAcDeviceBox activeAcDevice={this.state.activeDevice} />

                <div className="box">
                    <div className="columns">
                        <div className="column">
                            <h4 className="title is-4">
                                Lista klimatyzatorów:
                            </h4>
                            <AcDevicesTable
                                data={this.state.allDevicesData}
                                selectedRow={this.state.selectedRow}
                                onRowClicked={this.onRowSelected}
                            />
                            <div className="control">
                                <button className="button is-link is-success" onClick={e => this.onRowSelected(0)}>Dodaj nowy</button>
                            </div>
                        </div>
                        <div className="column">
                            <AcDeviceAddEditForm
                                isEdit={this.state.selectedRow}
                                editedDeviceData={this.state.selectedRow ? this.state.allDevicesData[this.state.selectedRow - 1] : null}
                                refreshCallback={this.refresh}
                                errorCallback={this.setApiFetchError}
                            />
                        </div>
                    </div>
                </div>
            </Fragment>
        );
    }
}

export default AcDevice;