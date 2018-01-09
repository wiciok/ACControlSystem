import React, { Component, Fragment } from 'react';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import AcSettingsSelect from './AcSettingsSelect';
import AcSettingTable from './AcSettingTable';
import AcSettingAdd from './AcSettingAdd';

class AcSettings extends Component {
    constructor(props) {
        super(props);

        this.onSelectionChanged = this.onSelectionChanged.bind(this);
        this.setApiFetchError = this.setApiFetchError.bind(this);
        this.onDeleteButtonClick = this.onDeleteButtonClick.bind(this);
        this.onSetAsDefaultButtonClick = this.onSetAsDefaultButtonClick.bind(this);
        this.onAcSettingAddButtonClick = this.onAcSettingAddButtonClick.bind(this);
        this.acSettingRegistered = this.acSettingRegistered.bind(this);
        this.doFetch = this.doFetch.bind(this);

        this.endpointAddress = `${window.apiAddress}/acsetting`;

        this.state = {
            error: {
                isError: false,
                errorMessage: null,
                allAcSettings: null,
                currentAcSetting: null
            }
        };
    }

    componentDidMount() {
        this.getAcSettings();
    }

    onSelectionChanged(guid) {
        let currentAcSetting;

        for (let index in this.state.allAcSettings) {
            if (this.state.allAcSettings[index].uniqueId === guid) {
                currentAcSetting = this.state.allAcSettings[index];
                break;
            }
        }

        this.setState({
            currentAcSetting: currentAcSetting
        }, () => { console.log(this.state); })
    }

    onDeleteButtonClick() {
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken/").concat(this.state.currentAcSetting.uniqueId);
        let fetchObj = {
            method: 'delete'
        }

        console.log(fetchObj);

        this.doFetch(fetchObj, fullAddress, this.getAcSettings, this.removeButton)
    }

    onSetAsDefaultButtonClick() {
        let isOnOff = this.state.currentAcSetting.isTurnOff ? "/defaultOff/" : "/defaultOn/"

        let fullAddress = this.endpointAddress
            .concat("/123temporaryfaketoken")
            .concat(isOnOff)
            .concat(this.state.currentAcSetting.uniqueId);
        let fetchObj = {
            method: 'post'
        }

        console.log(fetchObj);

        this.doFetch(fetchObj, fullAddress, this.getAcSettings, this.setDefaultButton)
    }

    onAcSettingAddButtonClick(newObj, button){
        let newSettingType = "/nec"; //or /raw

        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken").concat(newSettingType);
        let fetchObj = {
            method: 'post',
            body: JSON.stringify(newObj),
            headers: new Headers({ "Content-Type": "application/json" })
        }   
        console.log(newObj);
        console.log(fetchObj);

        this.doFetch(fetchObj, fullAddress, this.acSettingRegistered, button)
    }

    acSettingRegistered(){
        alert("registered");
    }


    getAcSettings() {
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken");
        let fetchObj = {
            method: 'get'
        }

        let successCallback = json => {
            this.setState({
                allAcSettings: json
            });
        }

        this.doFetch(fetchObj, fullAddress, successCallback, null)
    }

    doFetch(fetchObj, fullAddress, successCallback, button) {
        this.changeButtonInProgress(true, button);

        fetch(fullAddress, fetchObj)
            .then(response => {
                console.log("response: " + response.status);

                this.changeButtonInProgress(false, button);

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
                        .then(json => successCallback(json))
                        .catch(err => { this.setApiFetchError(err) });
                }
            }).catch(err => {
                this.setApiFetchError(err);
            })
    }

    changeButtonInProgress(isInProgress, button) {
        if (button == null)
            return;

        if (isInProgress === true) {
            button.classList.add("is-loading");
            button.classList.remove("is-primary");
        } else {
            button.classList.remove("is-loading");
            button.classList.add("is-primary");
        }
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
        let removeButton =
            <button className="button is-link is-danger" onClick={this.onDeleteButtonClick} ref={removeButton => this.removeButton = removeButton}>
                Usuń
            </button>

        let setAsDefaultOnOffSettingButton;
        if (this.state.currentAcSetting && this.state.currentAcSetting.isTurnOff)
            setAsDefaultOnOffSettingButton =
                <button className="button is-link is-primary" onClick={this.onSetAsDefaultButtonClick} ref={setDefaultButton => this.setDefaultButton = setDefaultButton}>
                    Ustaw jako domyślne ust. wyłączania
                </button>
        else
            setAsDefaultOnOffSettingButton =
                <button className="button is-link is-primary" onClick={this.onSetAsDefaultButtonClick} ref={setDefaultButton => this.setDefaultButton = setDefaultButton}>
                    Ustaw jako domyślne ust. włączania
                </button>

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

                <div className="box">
                    <h4 className="title is-4">Dostępne ustawienia:</h4>
                    <AcSettingsSelect
                        onChange={this.onSelectionChanged}
                        allAcSettings={this.state.allAcSettings} />
                    <br /><br />
                    <AcSettingTable acSetting={this.state.currentAcSetting} />
                    <span className="control">
                        {removeButton}&emsp;
                        {setAsDefaultOnOffSettingButton}
                    </span>
                </div>
                <div className="box">
                    <h4 className="title is-4">Zarejestruj nowe ustawienie:</h4>
                    <AcSettingAdd
                        onAddButtonClick={this.onAcSettingAddButtonClick}
                    />
                </div>
            </Fragment>
        );
    }
}

export default AcSettings;