import React, { Component, Fragment } from 'react';
import 'bulma/css/bulma.css';
import 'font-awesome/css/font-awesome.min.css'
import DatePicker from './DatePicker';

class AcScheduleAddForm extends Component {
    constructor(props) {
        super(props);

        this.onSelectChange = this.onSelectChange.bind(this);
        this.onValidDateOrTimeEntered = this.onValidStartDateOrTimeEntered.bind(this);

        this.state = {
            selectedScheduleType: 0
        };
    }

    onSelectChange() {
        let value = this.scheduleTypeSelect.value;

        this.setState({
            selectedScheduleType: Number.parseInt(value, 10)
        });

        console.log(this.state.selectedScheduleType);
    }

    onValidStartDateOrTimeEntered(dateObj) {
        console.log(dateObj);
    }

    onValidEndDateOrTimeEntered(dateObj) {
        console.log(dateObj);
    }


    render() {
        let options = this.props.scheduleArray.map((item, index) =>
            <option value={index} key={index}>{item}</option>
        );
        return (
            <Fragment>
                <h4 className="title is-4"> Dodaj nową regułę terminarza:</h4>
                <form>
                    <label>Cykliczność reguły:</label>
                    <br />
                    <div className="select">
                        <select onChange={this.onSelectChange} ref={sel => this.scheduleTypeSelect = sel}>{options}</select>
                    </div>
                    <br />
                    <br />

                    <div class="columns">
                        <div class="column">
                            <label>Czas rozpoczęcia - </label>
                            <DatePicker
                                scheduleType={this.state.selectedScheduleType}
                                weekDaysArray={this.props.weekDaysArray}
                                onChange={this.onValidStartDateOrTimeEntered}
                            />
                        </div>
                        <div class="column">
                            <label>Czas zakończenia - </label>
                            <DatePicker
                                scheduleType={this.state.selectedScheduleType}
                                weekDaysArray={this.props.weekDaysArray}
                                onChange={this.onValidEndDateOrTimeEntered}
                            />
                        </div>
                    </div>




                </form>
            </Fragment>
        );
    }
}

export default AcScheduleAddForm;