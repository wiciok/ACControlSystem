import React from 'react';
import './AcDeviceTableRow.css'

const AcDeviceTableRow = (props) => {
    let rowCssClass = "";
    if (props.isActive) {
        rowCssClass = "is-selected"
    }
    return (
        <tr onClick={e => props.onRowClicked(props.data.id)} className={rowCssClass}>
            <th>{props.data.id}</th>
            <td>{props.data.brand}</td>
            <td>{props.data.model}</td>
        </tr>
    );
}

export default AcDeviceTableRow;

