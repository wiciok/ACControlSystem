import React, { Component } from 'react';
import AcScheduleTableRow from './AcScheduleTableRow';
import './AcScheduleTable.css'

class AcScheduleTable extends Component {

    render() {
        if (this.props.scheduleData) {
            return (
                <div className="overflow">
                    <table className="table is-fullwidth schedule-table">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Cykliczność</th>
                                <th>Czas startu</th>
                                <th>Czas zakończenia</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.scheduleData.map(item => <AcScheduleTableRow
                                data={item}
                                key={item.id}
                                acSettings={this.props.acSettings}
                                scheduleArray={this.props.scheduleArray}
                                weekDaysArray={this.props.weekDaysArray}
                                onDeleteButtonClick={this.props.onDeleteButtonClick}
                            />)}
                        </tbody>
                    </table>
                </div>
            );
        }
        else {
            return <h4>Brak zapisanych wpisów terminarza</h4>
        }
    }
}

export default AcScheduleTable;