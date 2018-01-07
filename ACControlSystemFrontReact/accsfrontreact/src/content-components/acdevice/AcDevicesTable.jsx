import React, {Component} from 'react';
import AcDeviceTableRow from './AcDeviceTableRow';

class AcDevicesTable extends Component {
    constructor(props) {
        super(props);
        this.data = null;
    }

    render() {
        if (this.props.data) {
            return (
                <table className="table is-hoverable">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Marka</th>
                            <th>Model</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this
                            .props
                            .data
                            .map(item => <AcDeviceTableRow
                                data={item}
                                key={item.id}
                                isActive={this.props.selectedRow === item.id}
                                onRowClicked={this
                                .props
                                .onRowClicked}/>)}
                    </tbody>
                </table>
            );
        } else {
            return <h4>Brak zapisanych klimatyzatorów</h4>
        }

    }
}

export default AcDevicesTable;