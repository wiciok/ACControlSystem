import React, { Component, Fragment } from 'react';
import Datetime from 'react-datetime';
import 'react-datetime/css/react-datetime.css';
import InputMinutes from './InputMinutes';
import InputDayOfWeek from './InputDayOfWeek';

class DatePicker extends Component {
    constructor(props) {
        super(props);

        this.onChange = this.onChange.bind(this);

        this.onDateTimeEntered = this.onDateTimeEntered.bind(this);
        this.onTimeEntered = this.onTimeEntered.bind(this);
        this.onMinutesEntered = this.onMinutesEntered.bind(this);
        this.onDayOfWeekEntered = this.onDayOfWeekEntered.bind(this);
        this.onDayOfWeekDayEntered = this.onDayOfWeekDayEntered.bind(this);
        this.onDayOfWeekTimeEntered = this.onDayOfWeekTimeEntered.bind(this);

        this.dowDay = null;
        this.dowTime = null;
    }

    onChange(dateObject) {
        this.props.onChange(dateObject);
    }

    //---------------------------------

    //single:
    onDateTimeEntered(obj) {
        this.onChange(obj._d);
    }

    //every day:
    onTimeEntered(obj) {
        this.onChange(obj._d);
    }

    //every hour:
    onMinutesEntered(minutes) {
        let dateObj = new Date();
        dateObj.setMinutes(minutes);
        this.onChange(dateObj);
    }

    //every day of week:
    onDayOfWeekEntered() {
        if (this.dowDay == null || this.dowTime == null)
            return;
        let dateObj = this.dowTime;

        for (let i = 0; i < 7; i++) {
            if (dateObj.getDay() === this.dowDay)
                break;
            dateObj.setDate(dateObj.getDate() + 1);
        }

        this.onChange(dateObj)
    }

    onDayOfWeekDayEntered(dayOfWeekNumber) {
        this.dowDay = dayOfWeekNumber;
        this.onDayOfWeekEntered();
    }

    onDayOfWeekTimeEntered(time) {
        this.dowTime = time._d;
        this.onDayOfWeekEntered();
    }

    //---------------------------------

    render() {
        let dateTime;
        let labelText;

        console.log("datepicker scheduletype", this.props.scheduleType);

        switch (this.props.scheduleType) {
            case 0: //single
                dateTime = <Datetime
                    viewMode='days'
                    input={false}
                    onChange={this.onDateTimeEntered}
                />
                labelText = "Wybierz datę i godzinę:";
                break;
            case 1: //every day
                dateTime = <Datetime
                    onChange={this.onTimeEntered}
                    viewMode='time'
                    dateFormat={false}
                    input={false}
                />
                labelText = "Wybierz godzinę:";
                break;
            case 2: //every hour
                dateTime = <InputMinutes onChanged={this.onMinutesEntered} />
                labelText = "Wpisz minuty (format: mm)";
                break;
            case 3: //every day of week
                dateTime = [
                    <InputDayOfWeek
                        onChanged={this.onDayOfWeekDayEntered}
                        weekDaysArray={this.props.weekDaysArray}
                        key={0}
                    />,
                    <Datetime
                        onChange={this.onDayOfWeekTimeEntered}
                        viewMode='time'
                        dateFormat={false}
                        input={false}
                        key={1}
                    />
                ];
                labelText = "Wybierz dzień tygodnia i godzinę:";
                break;
        }

        return (
            <Fragment>
                <label>{labelText}</label>
                <br />
                {dateTime}
            </Fragment>
        );
    }
}

export default DatePicker;