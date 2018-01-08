import React, { Component, Fragment } from 'react';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import AcScheduleTable from './AcScheduleTable';
import AcSettingsSelect from '../acsettings/AcSettingsSelect';
import AcScheduleAddForm from './AcScheduleAddForm';

class AcSchedule extends Component {
    constructor(props) {
        super(props);

        this.addNewSchedule = this.addNewSchedule.bind(this);
        this.removeSchedule = this.removeSchedule.bind(this);
        this.getAllSchedulesData = this.getAllSchedulesData.bind(this);
        this.getAcSettings = this.getAcSettings.bind(this);
        this.changeAllSchedulesState = this.changeAllSchedulesState.bind(this);
        this.doFetch = this.doFetch.bind(this);

        this.endpointAddress = `${window.apiAddress}/acschedule`;

        this.state = {
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
    }

    getAllSchedulesData() {
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken");

        let fetchObj = {
            method: 'get'
        };
        this.doFetch(fetchObj, fullAddress, this.changeAllSchedulesState);
    }

    changeAllSchedulesState(data) {
        console.log(data);
        this.setState({
            allSchedulesData: data
        })
    }

    removeSchedule(id) {
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken").concat(`/${id}`);

        let fetchObj = {
            method: 'delete'
        };

        this.doFetch(fetchObj, fullAddress, this.getAllSchedulesData);
    }

    addNewSchedule(acSchedule) {
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken");
        let fetchObj = {
            method: 'post',
            body: JSON.stringify(acSchedule),
            headers: new Headers({ "Content-Type": "application/json" })
        };

        console.log(fetchObj);
        this.doFetch(fetchObj, fullAddress, this.getAllSchedulesData);
    }

    getAcSettings() {
        let endpointAddress = `${window.apiAddress}/acsetting`;
        let fullAddress = endpointAddress.concat("/123temporaryfaketoken");

        let fetchObj = {
            method: 'get'
        }

        let callback = (arg) =>
            this.setState({
                acSettings: arg
            });

        this.doFetch(fetchObj, fullAddress, callback);
    }

    doFetch(fetchObj, fullAddress, successCallback) {
        fetch(fullAddress, fetchObj)
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
                    if (response.status === 204) {
                        successCallback();
                        return;
                    }

                    response
                        .json()
                        .then(json => successCallback(json))
                        .catch(err => {
                            this.setState({
                                error: {
                                    isError: true,
                                    errorMessage: "Blad deserializacji odpowiedzi serwera do formatu JSON"
                                }
                            });
                        })
                }
            }).catch(err => {
                this.setApiFetchError(err);
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
                    <AcScheduleAddForm
                        scheduleArray={this.scheduleArray}
                        weekDaysArray={this.weekDaysArray}
                        addCallback={this.addNewSchedule}
                        acSettings={this.state.acSettings}
                    />
                </div>
            </Fragment>
        );
    }
}

export default AcSchedule;