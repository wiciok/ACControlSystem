import React, { Component } from 'react';
import UsersTableRow from './UsersTableRow';

class UsersTable extends Component {

    render() {
        if (this.props.data) {
            return (
                <table className="table is-hoverable">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Email</th>
                            <th>Data rejestracji</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this
                            .props
                            .data
                            .map(item => <UsersTableRow
                                data={item}
                                key={item.id}
                                isActive={this.props.selectedRow === item.id}
                                onRowClicked={this.props.onRowClicked}
                            />)}
                    </tbody>
                </table>
            );
        } else {
            return <h4>Brak zapisanych użytkowników</h4>
        }
    }
}

export default UsersTable;