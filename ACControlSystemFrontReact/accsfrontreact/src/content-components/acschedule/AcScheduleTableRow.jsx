import React, { Component, Fragment } from 'react';
import './AcScheduleTableRow.css'

class AcScheduleTableRow extends Component {
    constructor(props) {
        super(props);
        
        this.onRowHover=this.onRowHover.bind(this);
        this.onRowUnHover=this.onRowUnHover.bind(this);
    }


    getDateFormatted(date, scheduleType) {
        switch (scheduleType) {
            case 0: //Single
                return `${this.props.weekDaysArray[date.getDay()]}, ${date.toLocaleString()}`;
                break;
            case 1: //EveryDay
                return date.toLocaleTimeString();
                break;
            case 2: //EveryHour
                return `[godzina] : ${date.getMinutes()} minut`
                break;
            case 3: //EveryDayOfWeek
                return `${this.props.weekDaysArray[date.getDay()]}, ${date.toLocaleTimeString()}`;
                break;
        }
    }

    onRowHover(){
        this.row1.classList.add("is-selected");
        this.row2.classList.add("is-selected");
    }

    onRowUnHover(){
        this.row1.classList.remove("is-selected");
        this.row2.classList.remove("is-selected");
    }

    render() {
        let startDate = new Date(this.props.data.startTime);
        let endDate = new Date(this.props.data.endTime);

        return (
            <Fragment>
                <tr ref={row => this.row1=row} onMouseOver={this.onRowHover} onMouseLeave={this.onRowUnHover}>
                    <th>{this.props.data.id}</th>
                    <td>{this.props.scheduleArray[this.props.data.scheduleType]}</td>
                    <td>{this.getDateFormatted(startDate, this.props.data.scheduleType)}</td>
                    <td>{this.getDateFormatted(endDate, this.props.data.scheduleType)}</td>
                </tr>
                <tr ref={row => this.row2=row} onMouseOver={this.onRowHover} onMouseLeave={this.onRowUnHover}>
                    <td></td>

                    <td colSpan="2">
                        {this.props.data.acSettingGuid}
                    </td>
                    <td>
                        <div className="control">
                            <button 
                                className="button is-link is-danger is-small" 
                                onClick={e=>this.props.onDeleteButtonClick(this.props.data.id)} 
                                ref={removeButton => this.removeButton = removeButton}
                            >
                                Usuń
                            </button>
                        </div>
                    </td>
                </tr>
            </Fragment>
        );
    }
}

export default AcScheduleTableRow;