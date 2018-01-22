import React, { Component, Fragment } from 'react';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import AcSettingsSelect from './AcSettingsSelect';
import AcSettingTable from './AcSettingTable';
import AcSettingAdd from './AcSettingAdd';
import AcSettingsDefaultOnOffStatus from './AcSettingsDefaultOnOffStatus';
import sendAuth from '../../sendAuth.js';
import Cookies from 'js-cookie';

class AcSettings extends Component {
    constructor(props) {
        super(props);

        this.onSelectionChanged = this.onSelectionChanged.bind(this);
        this.setApiFetchError = this.setApiFetchError.bind(this);
        this.onDeleteButtonClick = this.onDeleteButtonClick.bind(this);
        this.onSetAsDefaultButtonClick = this.onSetAsDefaultButtonClick.bind(this);
        this.onAcSettingAddButtonClick = this.onAcSettingAddButtonClick.bind(this);
        this.getTurnOnOffSetting = this.getTurnOnOffSetting.bind(this);
        this.doFetch = this.doFetch.bind(this);

        this.endpointAddress = `${window.apiAddress}/acsetting`;

        this.state = {
            error: {
                defaultOn: null,
                defaultOff: null,
                isError: false,
                errorMessage: null,
                allAcSettings: null,
                currentAcSetting: null
            }
        };
    }

    componentDidMount() {
        this.getAcSettings();
        this.getTurnOnOffSetting();
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
        let fullAddress = this.endpointAddress.concat(this.state.currentAcSetting.uniqueId);
        let fetchObj = {
            method: 'delete',
            headers: new Headers({ "Authorization": 'Basic ' + btoa(":" + Cookies.get('token')) })
        }

        this.doFetch(fetchObj, fullAddress, this.getAcSettings, this.removeButton, this.onDeleteButtonClick)
    }

    onSetAsDefaultButtonClick() {
        let isOnOff = this.state.currentAcSetting.isTurnOff ? "/defaultOff/" : "/defaultOn/"

        let fullAddress = this.endpointAddress.concat(isOnOff).concat(this.state.currentAcSetting.uniqueId);
        let fetchObj = {
            method: 'post',
            headers: new Headers({ "Authorization": 'Basic ' + btoa(":" + Cookies.get('token')) })
        }

        this.doFetch(fetchObj, fullAddress, this.getAcSettings, this.setDefaultButton, this.onSetAsDefaultButtonClick)
    }

    onAcSettingAddButtonClick(newObj, button) {
        let newSettingType = "/nec"; //or /raw

        let fullAddress = this.endpointAddress.concat(newSettingType);
        let fetchObj = {
            method: 'post',
            body: JSON.stringify(newObj),
            headers: new Headers([["Content-Type", "application/json"], ["Authorization", 'Basic ' + btoa(":" + Cookies.get('token'))]])
        }

        this.doFetch(fetchObj, fullAddress, this.getAcSettings, button, this.onAcSettingAddButtonClick)
    }


    getAcSettings() {
        let fetchObj = {
            method: 'get',
            headers: new Headers({ "Authorization": 'Basic ' + btoa(":" + Cookies.get('token')) })
        }

        let successCallback = json => {
            this.setState({
                allAcSettings: json
            });
        }

        this.doFetch(fetchObj, this.endpointAddress, successCallback, null, this.getAcSettings)
    }

    getTurnOnOffSetting() {
        let endpointAddress = `${window.apiAddress}/acsetting`;
        let fullAddress = endpointAddress.concat("/defaultOn");
        let fetchObj = {
            method: 'get',
            headers: new Headers({ "Authorization": 'Basic ' + btoa(":" + Cookies.get('token')) })
        }

        fetch(fullAddress, fetchObj)
            .then(response => {
                console.log("check turn on response: " + response.status);

                switch (response.status) {
                    case 200:
                        response.json().then(data => { this.setState({ defaultOn: data }); })
                        break;
                    case 401:
                        sendAuth(this.getTurnOnOffSetting);
                        break;
                    case 404:
                        this.setState({ defaultOn: null });
                        break;
                    default:
                        break;
                }
            }).catch(err => { this.setApiFetchError(); })

        endpointAddress = `${window.apiAddress}/acsetting`;
        fullAddress = endpointAddress.concat("/defaultOff");

        fetch(fullAddress, fetchObj)
            .then(response => {
                console.log("check turn off response: " + response.status);

                switch (response.status) {
                    case 200:
                        response.json().then(data => { this.setState({ defaultOff: data }, () => console.log(data)) })
                        break;
                    case 401:
                        sendAuth(this.getTurnOnOffSetting);
                        break;
                    case 404:
                        this.setState({ defaultOff: null })
                        break;
                    default:
                        break;
                }
            }).catch(err => { this.setApiFetchError(); })
    }


    doFetch(fetchObj, fullAddress, successCallback, button, retryCallback) {
        this.changeButtonInProgress(true, button);

        fetch(fullAddress, fetchObj)
            .then(response => {
                console.log("response: " + response.status);

                this.changeButtonInProgress(false, button);

                if (!response.ok) {

                    if (response.status === 401)
                        sendAuth(retryCallback);

                    let error = new Error(response.statusText);
                    error.statusCode = response.status;

                    response.json().then(data => {
                        console.log(data);
                        error.errorMessage = data;
                        this.setApiFetchError(error);
                    }).catch(() => { this.setApiFetchError(error); });
                }

                else {
                    response.json()
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

        if (error.errorMessage) {
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
                    <h4 className="title is-4">Domyślne ustawienia:</h4>
                    <AcSettingsDefaultOnOffStatus
                        defaultOn={this.state.defaultOn}
                        defaultOff={this.state.defaultOff} />
                </div>

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