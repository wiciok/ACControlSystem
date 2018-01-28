import React from 'react';
import AcDeviceTableRow from './AcDeviceTableRow';

const AcDevicesTable = (props) => {
    if (props && props.data !== null && props.data !== undefined)
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
                    { props.data.map(item =>
                        <AcDeviceTableRow
                            data={item}
                            key={item.id}
                            isActive={props.selectedRow === item.id}
                            onRowClicked={props.onRowClicked}
                        />)}
                </tbody>
            </table>
        );
    else
        return <h4>Brak zapisanych klimatyzatorów</h4>
}

export default AcDevicesTable;