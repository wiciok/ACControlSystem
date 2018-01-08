import React, { Component, Fragment } from 'react';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import AcSettingsSelect from './AcSettingsSelect';

class AcSettings extends Component {
    constructor(props) {
        super(props);

        this.onSelectionChanged=this.onSelectionChanged.bind(this);
        this.setApiFetchError=this.setApiFetchError.bind(this);

        this.endpointAddress = `${window.apiAddress}/acsetting`;

        this.state = {
            error: {
                isError: false,
                errorMessage: null,
                allAcSettings: null
            }
        };
    }

    componentDidMount(){
        this.getAcSettings();
    }

    onSelectionChanged(guid){
        console.log(guid);
    }

    getAcSettings(){
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken");


        fetch(fullAddress)
            .then(response => {
                console.log("response: " + response.status);

                if (!response.ok) {
                    let error = new Error(response.statusText);
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
                else {
                    response
                        .json()
                        .then(json => {
                            this.setState({
                                allAcSettings: json
                            });
                            console.log(this.state.allAcSettings);
                        })
                        .catch(err => {this.props.errorCallback(err)});
                }
            }).catch(err=>{
                this.props.errorCallback(err);
            })
    }

    setApiFetchError(error) {
        let errorMessage = `${error.message}`;
        //console.log(error); console.log(error.statusCode);
        if (error.statusCode) {
            errorMessage = `Błąd ${error.statusCode}: `.concat(errorMessage);
        }

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
                 <h2 className="title is-2">Ustawienia</h2>
                <ErrorMessageComponent
                    isVisible={this.state.error.isError}
                    bodyText={this.state.error.errorMessage}
                    onChangeErrorState={() => {
                        this.setState({
                            error: {
                                isError: false,
                                errorMessage: null
                            }
                        })
                    }} />
                
                <AcSettingsSelect
                    onChange={this.onSelectionChanged}
                    allAcSettings={this.state.allAcSettings}/>
            </Fragment>
        );
    }
}

export default AcSettings;