import React, {Component} from 'react';
import './UsersTableRow.css'

class UsersTableRow extends Component {

    render() {
        let rowCssClass="";
        if(this.props.isActive){
            rowCssClass="is-selected"
        }
        return (
            <tr onClick={e=>this.props.onRowClicked(this.props.data.id)} className={rowCssClass}>
                <th>{this.props.data.id}</th>
                <td>{this.props.data.emailAddress}</td>
                <td>{this.props.data.registrationTimestamp}</td>
            </tr>
        );
    }
}

export default UsersTableRow;