import React, { Component, Fragment } from 'react';
import 'bulma/css/bulma.css';

import CurrentStateTag from './current-state-tag/CurrentStateTag';
import ToggleStateButton from './ToggleStateButton';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import sendAuth from './../../utils/sendAuth.js';
import setApiFetchError from './../../utils/setApiFetchError.js';
import { headerAuth } from './../../utils/authenticationHeaders.js';

class AcState extends Component {
    constructor(props) {
        super(props);

        this.getCurrentAcState = this.getCurrentAcState.bind(this);
        this.checkTurnOffSetting = this.checkTurnOffSetting.bind(this);
        this.checkTurnOnSetting = this.checkTurnOnSetting.bind(this);
        this.setApiFetchError = setApiFetchError.bind(this);

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

        let fetchObj = {
            method: 'get',
            headers: headerAuth
        }

        fetch(fullAddress, fetchObj)
            .then(response => {

                switch (response.status) {
                    case 200:
                        this.setState({ isTurnOffSettingSet: true })
                        break;
                    case 401:
                        sendAuth(this.checkTurnOffSetting);
                        break;
                    case 404:
                        //response.json().then(json => { this.setState({ isTurnOffSettingSet: false }) })
                        this.setState({ isTurnOffSettingSet: false });
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

        let fetchObj = {
            method: 'get',
            headers: headerAuth
        }

        fetch(fullAddress, fetchObj)
            .then(response => {

                switch (response.status) {
                    case 200:
                        this.setState({ isTurnOnSettingSet: true })
                        break;
                    case 401:
                        sendAuth(this.checkTurnOnSetting);
                        break;
                    case 404:
                        //response.json().then(json => { this.setState({ isTurnOnSettingSet: false }) })
                        this.setState({ isTurnOnSettingSet: false });
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

        let fetchObj = {
            method: 'get',
            headers: headerAuth
        }

        fetch(endpointAddress, fetchObj).then(response => {

            switch (response.status) {
                case 204:
                    this.setState({ currentAcState: "unknown" });
                    break;
                case 200:
                    response.json()
                        .then(json => {
                            if (json.isTurnOff === true)
                                this.setState({ currentAcState: "off" });
                            else
                                this.setState({ currentAcState: "on" });
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
                case 401:
                    sendAuth(this.getCurrentAcState);
                    break;
                default:
                    let error = new Error(response.statusText);
                    error.statusCode = response.status;

                    response.json().then(data => {
                        error.errorMessage = data;
                        this.setApiFetchError(error);
                    });
                    this.setApiFetchError(error);
            }
        }).catch(err => {
            this.setApiFetchError(err);
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