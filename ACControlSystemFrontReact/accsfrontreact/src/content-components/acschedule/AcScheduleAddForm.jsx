import React, { Component, Fragment } from 'react';
import 'bulma/css/bulma.css';
import 'font-awesome/css/font-awesome.min.css'
import DatePicker from './DatePicker';

class AcScheduleAddForm extends Component {
    constructor(props) {
        super(props);

        this.onSelectChange = this.onSelectChange.bind(this);
        this.onValidStartDateOrTimeEntered = this.onValidStartDateOrTimeEntered.bind(this);
        this.onValidEndDateOrTimeEntered = this.onValidEndDateOrTimeEntered.bind(this);
        this.onSaveButtonClick = this.onSaveButtonClick.bind(this);
        this.saveButtonActivate = this.saveButtonActivate.bind(this);

        this.state = {
            saveButtonActive: false,
            selectedScheduleType: 0,
            startTime: null,
            endTime: null,
            acSettingsGuid: null
        };
    }

    onSelectChange() {
        let value = this.scheduleTypeSelect.value;

        this.setState({
            selectedScheduleType: Number.parseInt(value, 10),
            startTime: null,
            endTime: null,
            acSettingsGuid: null
        }, () => { this.saveButtonActivate(); });
    }

    saveButtonActivate() {
        if (this.state.startTime !== null && this.state.endTime !== null) { //&& this.state.acSettingsGuid !== null
            this.setState({
                saveButtonActive: true
            });
        }
        else {
            this.setState({
                saveButtonActive: false
            });
        }
        console.log(this.state);
    }

    onValidStartDateOrTimeEntered(dateObj) {
        this.setState({
            startTime: dateObj
        }, () => { this.saveButtonActivate(); });
    }

    onValidEndDateOrTimeEntered(dateObj) {
        this.setState({
            endTime: dateObj
        }, () => { this.saveButtonActivate(); });
    }

    onSaveButtonClick() {
        let newAcScheduleObj = {
            id: 0,
            startTime: this.state.startTime,
            endTime: this.state.endTime,
            scheduleType: this.state.selectedScheduleType,
            acSettingsGuid: null
        }
        console.log(newAcScheduleObj);

        this.props.addCallback(newAcScheduleObj);
    }

    render() {
        let options = this.props.scheduleArray.map((item, index) =>
            <option value={index} key={index}>{item}</option>
        );

        let saveButton = this.state.saveButtonActive
            ? <button
                className="button is-link"
                onClick={this.onSaveButtonClick}
                ref={saveButton => this.saveButton = saveButton}>
                Zapisz
                </button>
            : <button
                className="button is-link"
                onClick={this.onSaveButtonClick}
                disabled
                ref={saveButton => this.saveButton = saveButton}>
                Zapisz
                </button>
        return (
            <Fragment>
                <h4 className="title is-4"> Dodaj nową regułę terminarza:</h4>
                <label>Cykliczność reguły:</label>
                <br />
                <div className="select">
                    <select onChange={this.onSelectChange} ref={sel => this.scheduleTypeSelect = sel}>{options}</select>
                </div>
                <br />
                <br />

                <div className="columns">
                    <div className="column">
                        <label>Czas rozpoczęcia - </label>
                        <DatePicker
                            scheduleType={this.state.selectedScheduleType}
                            weekDaysArray={this.props.weekDaysArray}
                            onChange={this.onValidStartDateOrTimeEntered}
                        />
                    </div>
                    <div className="column">
                        <label>Czas zakończenia - </label>
                        <DatePicker
                            scheduleType={this.state.selectedScheduleType}
                            weekDaysArray={this.props.weekDaysArray}
                            onChange={this.onValidEndDateOrTimeEntered}
                        />
                    </div>
                </div>
                {saveButton}
            </Fragment>
        );
    }
}

export default AcScheduleAddForm;