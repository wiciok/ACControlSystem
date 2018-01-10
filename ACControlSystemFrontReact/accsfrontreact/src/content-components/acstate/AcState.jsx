import React, { Component, Fragment } from 'react';
import 'bulma/css/bulma.css';

import CurrentStateTag from './current-state-tag/CurrentStateTag';
import ToggleStateButton from './ToggleStateButton';
import ErrorMessageComponent from '../../ErrorMessageComponent';

class AcState extends Component {
    constructor(props) {
        super(props);

        this.getCurrentAcState = this.getCurrentAcState.bind(this);
        this.checkTurnOffSetting = this.checkTurnOffSetting.bind(this);
        this.checkTurnOnSetting = this.checkTurnOnSetting.bind(this);
        this.setApiFetchError = this.setApiFetchError.bind(this);

        this.state = {
            isTurnOffSettingSet: true,
            isTurnOnSettingSet: true,
            currentAcState: "unknown",
            error: {
                isError: false,
                errorMessage: null
            }
        };
    };

    componentDidMount() {
        this.getCurrentAcState();
        this.checkTurnOffSetting();
        this.checkTurnOnSetting();
    }


    checkTurnOffSetting() {
        let endpointAddress = `${window.apiAddress}/acsetting`;
        let fullAddress = endpointAddress.concat("/123temporaryfaketoken").concat("/defaultOff");

        fetch(fullAddress)
            .then(response => {
                console.log("check turn off response: " + response.status);

                switch (response.status) {
                    case 200:
                        this.setState({
                            isTurnOffSettingSet: true
                        })
                        break;
                    case 404:
                        response.json().then(
                            json => {
                                this.setState({
                                    isTurnOffSettingSet: false
                                })
                            }
                        )
                        break;
                    default:
                        break;
                }
            }).catch(err => {
                this.setApiFetchError(err);
            })
    }

    checkTurnOnSetting() {
        let endpointAddress = `${window.apiAddress}/acsetting`;
        let fullAddress = endpointAddress.concat("/123temporaryfaketoken").concat("/defaultOn");

        fetch(fullAddress)
            .then(response => {
                console.log("check turn on response: " + response.status);

                switch (response.status) {
                    case 200:
                        this.setState({
                            isTurnOnSettingSet: true
                        })
                        break;
                    case 404:
                        response.json().then(
                            json => {
                                this.setState({
                                    isTurnOnSettingSet: false
                                })
                            }
                        )
                        break;
                    default:
                        break;
                }
            }).catch(err => {
                this.setApiFetchError(err);
            })
    }


    getCurrentAcState() {
        let endpointAddress = `${window.apiAddress}/acstate`;
        console.log("get " + endpointAddress);

        fetch(endpointAddress).then(response => {
            console.log(response.status);

            switch (response.status) {
                case 204:
                    this.setState({ currentAcState: "unknown" });
                    break;
                case 200:
                    response
                        .json()
                        .then(json => {
                            console.log(json);
                            if (json.isTurnOff === true) {
                                this.setState({ currentAcState: "off" });
                            } else {
                                this.setState({ currentAcState: "on" });
                            }
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
                        throw error;
                    });

                    throw error;
            }
        }).catch(err => {
            this.setApiFetchError(err);
        });
    }

    setApiFetchError(error) {
        let errorMessage = `${error.message}`;

        if (error.statusCode)
            errorMessage = `Błąd ${error.statusCode}: `.concat(errorMessage);

        if (error.errorMessge)
            errorMessage += "Dodatkowe informacje: " + error.errorMessage;

        this.setState({
            error: {
                isError: true,
                errorMessage: errorMessage
            }
        });
    }

    render() {
        let manualControl =
            <Fragment>
                <ToggleStateButton
                    actionType="on"
                    stateRefreshCallback={this.getCurrentAcState}
                    setErrorCallback={this.setApiFetchError} 
                    />
                &emsp;
                <ToggleStateButton
                    actionType="off"
                    stateRefreshCallback={this.getCurrentAcState}
                    setErrorCallback={this.setApiFetchError} 
                    />
            </Fragment>

        let turnOnOffError =
            <article className="message is-danger">
                <div className="message-header">
                    <p>Błąd!</p>
                </div>
                <div className="message-body">
                    Nie można sterować ręcznie klimatyzatorem!<br />
                    Ustawienie 
                    {!this.state.isTurnOffSettingSet ? ' wyłączające ' : null}
                    {!this.state.isTurnOffSettingSet && !this.state.isTurnOnSettingSet ? ' i ' : null}
                    {!this.state.isTurnOnSettingSet ? ' włączające ' : null}
                    klimatyzator nie zostało zdefiniowane w ustawieniach!
                </div>
            </article>

        return (
            <Fragment>
                <h2 className="title is-2">Stan klimatyzatora</h2>
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

                <div className="columns">
                    <div className="column">
                        <div className="box">
                            <h4 className="title is-4">
                                Obecny stan klimatyzatora: &emsp;
                                <br /><br />
                                <CurrentStateTag
                                    tagState={this.state.currentAcState}
                                    onClick={this.getCurrentAcState} />
                            </h4>
                        </div>
                    </div>
                    <div className="column">
                        <div className="box">
                            <h4 className="title is-4">
                                Sterowanie ręczne: &emsp;
                            </h4 >
                            {!this.state.isTurnOffSettingSet || !this.state.isTurnOnSettingSet ? turnOnOffError : manualControl}
                        </div>
                    </div>
                </div>
            </Fragment >
        );
    }
};

export default AcState;