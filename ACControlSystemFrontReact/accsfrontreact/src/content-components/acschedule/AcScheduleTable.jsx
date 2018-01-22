import React from 'react';
import AcScheduleTableRow from './AcScheduleTableRow';
import './AcScheduleTable.css'

const AcScheduleTable = (props) => {
    if (props.scheduleData)
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
                        {props.scheduleData.map(item => <AcScheduleTableRow
                            data={item}
                            key={item.id}
                            acSettings={props.acSettings}
                            scheduleArray={props.scheduleArray}
                            weekDaysArray={props.weekDaysArray}
                            onDeleteButtonClick={props.onDeleteButtonClick}
                        />)}
                    </tbody>
                </table>
            </div>
        );
    else
        return <h4>Brak zapisanych wpisów terminarza</h4>
}

export default AcScheduleTable;