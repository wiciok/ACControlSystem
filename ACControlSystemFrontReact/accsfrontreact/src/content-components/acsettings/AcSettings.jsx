import React, { Component, Fragment } from 'react';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import AcSettingsSelect from './AcSettingsSelect';
import AcSettingTable from './AcSettingTable';
import AcSettingAdd from './AcSettingAdd';
import AcSettingsDefaultOnOffStatus from './AcSettingsDefaultOnOffStatus';
import sendAuth from './../../utils/sendAuth.js';
import setApiFetchError from './../../utils/setApiFetchError.js';
import { headerAuthAndContentTypeJsonFun, headerAuthFun } from './../../utils/authenticationHeaders.js';

class AcSettings extends Component {
    constructor(props) {
        super(props);

        this.onSelectionChanged = this.onSelectionChanged.bind(this);
        this.setApiFetchError = setApiFetchError.bind(this);
        this.onDeleteButtonClick = this.onDeleteButtonClick.bind(this);
        this.onSetAsDefaultButtonClick = this.onSetAsDefaultButtonClick.bind(this);
        this.onAcSettingAddButtonClick = this.onAcSettingAddButtonClick.bind(this);
        this.getTurnOnOffSetting = this.getTurnOnOffSetting.bind(this);
        this.onAcSettingAdded = this.onAcSettingAdded.bind(this);
        this.doFetch = this.doFetch.bind(this);
        this.getAcSettings = this.getAcSettings.bind(this);

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

        this.setState({ currentAcSetting: currentAcSetting })
    }

    onDeleteButtonClick(e) {
        let fullAddress = this.endpointAddress.concat('/' + this.state.currentAcSetting.uniqueId);
        let fetchObj = {
            method: 'delete',
            headers: headerAuthFun()
        }

        this.doFetch(fetchObj, fullAddress, this.getAcSettings, e.target, this.onDeleteButtonClick)
    }

    onSetAsDefaultButtonClick(e) {
        let isOnOff = this.state.currentAcSetting.isTurnOff ? "/defaultOff/" : "/defaultOn/"

        let fullAddress = this.endpointAddress.concat(isOnOff).concat(this.state.currentAcSetting.uniqueId);
        let fetchObj = {
            method: 'post',
            headers: headerAuthFun()
        }

        this.doFetch(fetchObj, fullAddress, this.getTurnOnOffSetting, e.target, this.onSetAsDefaultButtonClick)
    }

    onAcSettingAddButtonClick(newObj, button, resetOptionsCallback) {
        let newSettingType = "/nec"; //or /raw

        let fullAddress = this.endpointAddress.concat(newSettingType);
        let fetchObj = {
            method: 'post',
            body: JSON.stringify(newObj),
            headers: headerAuthAndContentTypeJsonFun(),
        }

        this.resetOptionsCallback = resetOptionsCallback;

        this.doFetch(fetchObj, fullAddress, this.onAcSettingAdded, button, this.onAcSettingAddButtonClick)
    }

    onAcSettingAdded() {
        this.resetOptionsCallback();
        this.getAcSettings();
    }



    getAcSettings() {
        let fetchObj = {
            method: 'get',
            headers: headerAuthAndContentTypeJsonFun(),
        };

        let successCallback = json => {
            if (!json || json && json.length === 0)
                this.setState({ allAcSettings: null }, () => { this.getTurnOnOffSetting(); });
            else
                this.setState({ allAcSettings: json }, () => { this.getTurnOnOffSetting(); });
        };

        this.doFetch(fetchObj, this.endpointAddress, successCallback, null, this.getAcSettings);
    }

    getTurnOnOffSetting() {
        let endpointAddress = `${window.apiAddress}/acsetting`;
        let fullAddress = endpointAddress.concat("/defaultOn");
        let fetchObj = {
            method: 'get',
            headers: headerAuthFun()
        }

        fetch(fullAddress, fetchObj)
            .then(response => {

                switch (response.status) {
                    case 200:
                        response.json().then(data => { this.setState({ defaultOn: data }); })
                        break;
                    case 401:
                        sendAuth(this.getTurnOnOffSetting, this.props.onLogout);
                        break;
                    case 404:
                        this.setState({ defaultOn: null });
                        break;
                    default:
                        break;
                }
            }).catch(err => { this.setApiFetchError(err); })

        endpointAddress = `${window.apiAddress}/acsetting`;
        fullAddress = endpointAddress.concat("/defaultOff");

        fetch(fullAddress, fetchObj)
            .then(response => {

                switch (response.status) {
                    case 200:
                        response.json().then(data => { this.setState({ defaultOff: data }) });
                        break;
                    case 401:
                        sendAuth(this.getTurnOnOffSetting, this.props.onLogout);
                        break;
                    case 404:
                        this.setState({ defaultOff: null })
                        break;
                    default:
                        break;
                }
            }).catch(err => { this.setApiFetchError(err); })
    }


    doFetch(fetchObj, fullAddress, successCallback, button, retryCallback) {
        this.changeButtonInProgress(true, button);

        fetch(fullAddress, fetchObj)
            .then(response => {
                this.changeButtonInProgress(false, button);

                if (!response.ok) {

                    if (response.status === 401)
                        sendAuth(retryCallback, this.props.onLogout);

                    let error = new Error(response.statusText);
                    error.statusCode = response.status;

                    response.json().then(data => {
                        error.errorMessage = data;
                        this.setApiFetchError(error);
                    }).catch(() => { this.setApiFetchError(error); });
                }

                else {
                    response.json()
                        .then(json => successCallback(json))
                        .catch(() => { successCallback() });
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

                <div className="box">
                    <h4 className="title is-4">Domyślne ustawienia:</h4>
                    <AcSettingsDefaultOnOffStatus
                        defaultOn={this.state.defaultOn}
                        defaultOff={this.state.defaultOff} />
                </div>
                <div className="box">
                    <h4 className="title is-4">Dostępne ustawienia:</h4>
                    {this.state.allAcSettings
                        ? <Fragment>
                            <AcSettingsSelect
                                onChange={this.onSelectionChanged}
                                allAcSettings={this.state.allAcSettings}
                                dummyInitialValue={true}
                            />
                            <br /> <br />
                            <AcSettingTable
                                acSetting={this.state.currentAcSetting}
                                onDeleteButtonClick={this.onDeleteButtonClick}
                                onSetAsDefaultButtonClick={this.onSetAsDefaultButtonClick}
                            />
                        </Fragment>
                        : <div>Brak dostępnych ustawień!</div>
                    }
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