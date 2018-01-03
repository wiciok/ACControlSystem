import React, {Component, Fragment} from 'react';
import 'bulma/css/bulma.css';

import CurrentStateTag from './current-state-tag/CurrentStateTag';
import ToggleStateButton from './ToggleStateButton';
import ErrorMessageComponent from '../../ErrorMessageComponent';

class AcState extends Component {
    constructor(props) {
        super(props);

        this.state = {
            currentAcState: "unknown",
            error: {
                isError: false,
                errorMessage: null
            }
        };
    };

    componentDidMount() {
        this.getCurrentAcState();
    }

    getCurrentAcState() {
        let endpointAddress = `${window.apiAddress}/acstate`;
        console.log("get " + endpointAddress);

        fetch(endpointAddress).then(response => {
            console.log(response.status);
            switch (response.status) {
                case 204:
                    this.setState({currentAcState: "unknown"});
                    break;
                case 200:
                    response
                        .json()
                        .then(json => {
                            console.log(json);
                            if (json.isTurnOff === true) {
                                this.setState({currentAcState: "off"});
                            } else {
                                this.setState({currentAcState: "on"});
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

                    //todo: poprawic to/usunac
                    if (response.bodyUsed) {
                        response
                            .json()
                            .then(x => {
                                console.log(x);
                                error.errorMessage = x;
                                throw error;
                            });
                    };

                    throw error;
                    break;
            }
        }).catch(err => {
            console.log(err);
            this.setApiFetchError(err);
        });
    }

    setApiFetchError(error) {
        //console.log(error); console.log(error.statusCode);
        let errorMessage = `Błąd ${error.statusCode}: ${error.message}`;
        if (error.errorMessge) {
            errorMessage += "Dodatkowe informacje: " + error.errorMessage;
        }

        this.setState({
            error: {
                isError: true,
                errorMessage: errorMessage
            }
        });
    }

    render() {
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
                }}/>
                <div className="box">
                    <div className="columns">
                        <div className="column">
                            <h4 className="title is-4">
                                Obecny stan klimatyzatora: &emsp;
                                <br/><br/>
                                <CurrentStateTag
                                    tagState={this.state.currentAcState}
                                    onClick={e => {
                                    this.getCurrentAcState()
                                }}/>
                            </h4>
                        </div>
                        <div className="column">
                            <h4 className="title is-4">
                                Sterowanie ręczne: &emsp;
                                <br/><br/>
                                <ToggleStateButton
                                    actionType="on"
                                    stateRefreshCallback={e => this.getCurrentAcState()}
                                    setErrorCallback={e => this.setApiFetchError(e)}/>
                                <br/><br/>
                                <ToggleStateButton
                                    actionType="off"
                                    stateRefreshCallback={e => this.getCurrentAcState()}
                                    setErrorCallback={e => this.setApiFetchError(e)}/>
                            </h4>
                        </div>
                    </div>
                </div>
            </Fragment>
        );
    }
};

export default AcState;