import React, {Component, Fragment} from 'react';
import 'bulma/css/bulma.css';

import CurrentStateTag from './current-state-tag/CurrentStateTag';
import ToggeStateButton from './ToggleStateButton';
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
        console.log(endpointAddress);

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
                        .catch();
                    break;
                default:
                    throw new Error("Unexpected return code");
            }
        }).catch(err => {
            console.log(err);
            alert('error');
            this.setState({
                error: {
                    isError: true,
                    errorMessage: "Błąd pobierania danych z API!"
                }
            });

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
                                    onClick={this.getCurrentAcState}/>
                            </h4>
                        </div>
                        <div className="column">
                            <h4 className="title is-4">
                                Sterowanie ręczne: &emsp;
                                <br/><br/>
                                <ToggeStateButton actionType="on"/>
                                <br/><br/>
                                <ToggeStateButton actionType="off"/>
                            </h4>
                        </div>
                    </div>
                </div>
            </Fragment>
        );
    }
};

export default AcState;