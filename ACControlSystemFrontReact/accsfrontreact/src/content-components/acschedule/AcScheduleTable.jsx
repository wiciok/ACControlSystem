import React, { Component } from 'react';
import AcScheduleTableRow from './AcScheduleTableRow';
import './AcScheduleTable.css'

class AcScheduleTable extends Component {
    constructor(props) {
        super(props);

        this.getAllAcSettings = this.getAllAcSettings.bind(this);
        this.doFetch = this.doFetch.bind(this);
    }

    componentDidMount() {
        this.getAllAcSettings();
    }

    getAllAcSettings() {
        //todo

        this.doFetch();
    }

    doFetch() {
        //todo
    }

    render() {
        if (this.props.scheduleData) {
            return (
                <div className="overflow">
                    <table className="table is-fullwidth">
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
                                scheduleDictionary={this.props.scheduleDictionary}
                                weekDaysArray={this.props.weekDaysArray}
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