import React, { Component, Fragment } from 'react';
import './AcScheduleTableRow.css'

class AcScheduleTableRow extends Component {
    constructor(props) {
        super(props);
        
        this.onRowHover=this.onRowHover.bind(this);
        this.onRowUnHover=this.onRowUnHover.bind(this);
        this.getAcSettingString=this.getAcSettingString.bind(this);
    }


    getDateFormatted(date, scheduleType) {
        switch (scheduleType) {
            case 0: //Single
                return `${this.props.weekDaysArray[date.getDay()]}, ${date.toLocaleString()}`;
            case 1: //EveryDay
                return date.toLocaleTimeString();
            case 2: //EveryHour
                return `[godzina] : ${date.getMinutes()} minut`
            case 3: //EveryDayOfWeek
                return `${this.props.weekDaysArray[date.getDay()]}, ${date.toLocaleTimeString()}`;
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

    getAcSettingString(){
        let currentAcSetting;
        
        for(let acSetting in this.props.acSettings){
            if(acSetting.uniqueId===this.props.data.acSettingGuid){
                currentAcSetting=acSetting;
                break;
            }
        }
        let readableName = "";
        if(!currentAcSetting){
            readableName="Brak ustawienia o danym id!";
            return readableName;
        }
        
        if (currentAcSetting.settings) {
            for (let key in currentAcSetting.settings) {
                let value = currentAcSetting.settings[key];
                readableName = readableName.concat(`${key} : ${value}`);
            }
        }
        else 
            readableName = readableName.concat("(Brak ustawień)");
        readableName = readableName.concat(`, id: ${currentAcSetting.uniqueId}`);

        return readableName;
    }

    render() {
        let startDate = new Date(this.props.data.startTime);
        let endDate = new Date(this.props.data.endTime);
        let acSetting = this.getAcSettingString();

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
                        {acSetting}
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