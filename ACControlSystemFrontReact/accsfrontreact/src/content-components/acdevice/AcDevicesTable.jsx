import React from 'react';
import AcDeviceTableRow from './AcDeviceTableRow';

const AcDevicesTable = (props) => {
    if (this.props && this.props.data !== null && this.props.data !== undefined)
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
                    {this.props.data.map(item =>
                        <AcDeviceTableRow
                            data={item}
                            key={item.id}
                            isActive={this.props.selectedRow === item.id}
                            onRowClicked={this.props.onRowClicked}
                        />)}
                </tbody>
            </table>
        );
    else
        return <h4>Brak zapisanych klimatyzatorów</h4>
}

export default AcDevicesTable;