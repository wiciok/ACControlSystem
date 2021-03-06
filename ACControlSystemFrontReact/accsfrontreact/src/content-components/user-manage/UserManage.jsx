import React, { Component, Fragment } from 'react';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import UsersTable from './UsersTable';
import UsersAddEditForm from './UsersAddEditForm';
import 'bulma/css/bulma.css';
import sendAuth from './../../utils/sendAuth.js';
import setApiFetchError from './../../utils/setApiFetchError.js';
import { headerAuthFun } from './../../utils/authenticationHeaders.js';


class UserManage extends Component {
    constructor(props) {
        super(props);

        this.onRowSelected = this.onRowSelected.bind(this);
        this.getAllUsers = this.getAllUsers.bind(this);
        this.setApiFetchError = setApiFetchError.bind(this);

        this.endpointAddress = `${window.apiAddress}/user`;

        this.state = {
            allUsersData: null,
            selectedRow: null,
            error: {
                isError: false,
                errorMessage: null
            }
        };
    }

    componentDidMount() {
        this.getAllUsers();
    }

    getAllUsers() {
        let fullAddress = this.endpointAddress.concat("/all");
        let fetchObj = {
            method: 'get',
            headers: headerAuthFun()
        }

        fetch(fullAddress, fetchObj).then(response => {
            switch (response.status) {
                case 200:
                    response.json()
                        .then(json => { this.setState({ allUsersData: json, selectedRow: 0 }) })
                        .catch(err => { this.setState({ error: { isError: true, errorMessage: "Blad deserializacji odpowiedzi serwera do formatu JSON" } }); });
                    break;
                default:
                    if (response.status === 401)
                        sendAuth(this.getAllUsers, this.props.onLogout);

                    let error = new Error(response.statusText);
                    error.statusCode = response.status;
                    response.json().then(x => {
                        error.errorMessage = x;
                        this.setApiFetchError(error);
                    }).catch(() => { this.setApiFetchError(error); });
            }
        }).catch(err => { this.setApiFetchError(err); });
    }

    onRowSelected(rowId) {
        this.setState({ selectedRow: rowId });
    }

    render() {
        return (
            <Fragment>
                <h2 className="title is-2">Użytkownicy</h2>

                <ErrorMessageComponent
                    isVisible={this.state.error.isError}
                    bodyText={this.state.error.errorMessage}
                    onChangeErrorState={e => {
                        this.setState({
                            error: {
                                isError: false,
                                errorMessage: null
                            }
                        })
                    }} />

                <div className="box">
                    <div className="columns">
                        <div className="column">
                            <h4 className="title is-4">
                                Lista użytkowników:
                            </h4>
                            <UsersTable
                                data={this.state.allUsersData}
                                selectedRow={this.state.selectedRow}
                                onRowClicked={this.onRowSelected}
                            />
                            <div className="control">
                                <button className="button is-link is-success" onClick={e => this.onRowSelected(0)}>Dodaj nowego użytkownika</button>
                            </div>
                        </div>
                        <div className="column">
                            <UsersAddEditForm
                                isEdit={this.state.selectedRow}
                                editedUserData={this.state.selectedRow ? this.state.allUsersData[this.state.selectedRow - 1] : null}
                                refreshCallback={this.getAllUsers}
                                errorCallback={this.setApiFetchError}
                                onLogout={this.props.onLogout}
                            />
                        </div>
                    </div>
                </div>
            </Fragment>
        );
    }
}

export default UserManage;