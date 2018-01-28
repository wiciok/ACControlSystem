import React from 'react';
import './UsersTableRow.css'

const UsersTableRow = (props) => {
    let rowCssClass = "";
    if (props.isActive) {
        rowCssClass = "is-selected"
    }
    return (
        <tr onClick={e => props.onRowClicked(props.data.id)} className={rowCssClass}>
            <th>{props.data.id}</th>
            <td>{props.data.emailAddress}</td>
            <td>{props.data.registrationTimestamp}</td>
        </tr>
    );
}

export default UsersTableRow;