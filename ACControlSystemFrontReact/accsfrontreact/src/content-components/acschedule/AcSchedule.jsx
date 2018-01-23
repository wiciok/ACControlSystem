import React, { Component, Fragment } from 'react';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import AcScheduleTable from './AcScheduleTable';
import AcScheduleAddForm from './AcScheduleAddForm';
import sendAuth from './../../utils/sendAuth.js';
import setApiFetchError from './../../utils/setApiFetchError.js';
import { headerAuthAndContentTypeJsonFun, headerAuthFun } from './../../utils/authenticationHeaders.js';

class AcSchedule extends Component {
    constructor(props) {
        super(props);

        this.checkTurnOffSetting = this.checkTurnOffSetting.bind(this);
        this.addNewSchedule = this.addNewSchedule.bind(this);
        this.removeSchedule = this.removeSchedule.bind(this);
        this.getAllSchedulesData = this.getAllSchedulesData.bind(this);
        this.getAcSettings = this.getAcSettings.bind(this);
        this.changeAllSchedulesState = this.changeAllSchedulesState.bind(this);
        this.doFetch = this.doFetch.bind(this);
        this.setApiFetchError = setApiFetchError.bind(this);

        this.endpointAddress = `${window.apiAddress}/acschedule`;

        this.state = {
            isTurnOffSettingSet: true,
            allSchedulesData: null,
            acSettings: null,
            error: {
                isError: false,
                errorMessage: null
            }
        };

        this.scheduleArray = ['Jednorazowo', 'Codziennie', 'Co godzinę', 'Dzień tygodnia'];

        this.weekDaysArray = ["niedziela", "poniedziałek", "wtorek", "środa", "czwartek", "piątek", "sobota"]
    }

    componentDidMount() {
        this.getAllSchedulesData();
        this.getAcSettings();
        this.checkTurnOffSetting();
    }

    checkTurnOffSetting() {
        let endpointAddress = `${window.apiAddress}/acsetting`;
        let fullAddress = endpointAddress.concat("/defaultOff");

        let fetchObj = {
            method: 'get',
            headers: headerAuthFun()
        }

        fetch(fullAddress, fetchObj)
            .then(response => {

                switch (response.status) {
                    case 200:
                        this.setState({ isTurnOffSettingSet: true })
                        break;
                    case 404:
                        //response.json().then(json => {this.setState({ isTurnOffSettingSet: false })})
                        this.setState({ isTurnOffSettingSet: false });
                        break;
                    case 401:
                        sendAuth(this.checkTurnOffSetting);
                        break;
                    default:
                        break;
                }
            }).catch(err => {
                this.setApiFetchError(err);
            })
    }


    getAllSchedulesData() {
        let fetchObj = {
            method: 'get',
            headers: headerAuthFun()
        };
        this.doFetch(fetchObj, this.endpointAddress, this.changeAllSchedulesState, this.getAllSchedulesData);
    }

    changeAllSchedulesState(data) {
        this.setState({
            allSchedulesData: data
        })
    }

    removeSchedule(id) {
        let fullAddress = this.endpointAddress.concat(`/${id}`);

        let fetchObj = {
            method: 'delete',
            headers: headerAuthFun()
        };

        this.doFetch(fetchObj, fullAddress, this.getAllSchedulesData, this.removeSchedule);
    }

    addNewSchedule(acSchedule) {
        let fetchObj = {
            method: 'post',
            body: JSON.stringify(acSchedule),
            headers: headerAuthAndContentTypeJsonFun(),
        };

        this.doFetch(fetchObj, this.endpointAddress, this.getAllSchedulesData, this.addNewSchedule);
    }

    getAcSettings() {
        let endpointAddress = `${window.apiAddress}/acsetting`;
        let fullAddress = endpointAddress.concat("/allon");

        let fetchObj = {
            method: 'get',
            headers: headerAuthFun()
        }

        let callback = (arg) =>
            this.setState({
                acSettings: arg
            });

        this.doFetch(fetchObj, fullAddress, callback, this.getAcSettings);
    }

    doFetch(fetchObj, fullAddress, successCallback, retryCallback) {
        fetch(fullAddress, fetchObj)
            .then(response => {

                switch (response.status) {
                    case 204:
                        successCallback();
                        break;
                    case 200:
                        response.json()
                            .then(json => successCallback(json))
                            .catch(err => { this.setState({ error: { isError: true, errorMessage: "Blad deserializacji odpowiedzi serwera do formatu JSON" } }); })
                        break;
                    case 401:
                        sendAuth(retryCallback)
                        break;
                    default:
                        let error = new Error(response.statusText);
                        error.statusCode = response.status;

                        response.json().then(data => {
                            error.errorMessage = data;
                            this.setApiFetchError(error);
                        }).catch(() => { this.setApiFetchError(error); });
                }
            });
    }

    render() {
        let addForm =
            <AcScheduleAddForm
                scheduleArray={this.scheduleArray}
                weekDaysArray={this.weekDaysArray}
                addCallback={this.addNewSchedule}
                acSettings={this.state.acSettings}
            />
        let addNotPossible =
            <article className="message is-danger">
                <div className="message-header">
                    <p>Błąd!</p>
                </div>
                <div className="message-body">
                    Nie można dodać nowego wpisu terminarza! Ustawienie wyłączające klimatyzator nie zostało zdefiniowane w ustawieniach!
                    </div>
            </article>


        return (
            <Fragment>
                <h2 className="title is-2">Terminarz</h2>
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
                    <h4 className="title is-4">Lista wpisów terminarza:</h4>
                    <AcScheduleTable
                        scheduleData={this.state.allSchedulesData}
                        acSettings={this.state.acSettings}
                        onDeleteButtonClick={this.removeSchedule}
                        scheduleArray={this.scheduleArray}
                        weekDaysArray={this.weekDaysArray}
                    />
                </div>

                <div className="box">
                    <h4 className="title is-4"> Dodaj nową regułę terminarza:</h4>
                    {this.state.isTurnOffSettingSet ? addForm : addNotPossible}
                </div>
            </Fragment>
        );
    }
}

export default AcSchedule;