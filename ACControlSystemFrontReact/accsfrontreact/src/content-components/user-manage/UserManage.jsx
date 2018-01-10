import React, { Component, Fragment } from 'react';
import ErrorMessageComponent from '../../ErrorMessageComponent';
import UsersTable from './UsersTable';
import UsersAddEditForm from './UsersAddEditForm';
import 'bulma/css/bulma.css';


class UserManage extends Component {
    constructor(props) {
        super(props);

        this.onRowSelected = this.onRowSelected.bind(this);
        this.getAllUsers = this.getAllUsers.bind(this);
        this.setApiFetchError = this.setApiFetchError.bind(this);

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
        //let fullAddress = this.endpointAddress.concat("/all");
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken/all");
        console.log("get " + fullAddress);

        fetch(fullAddress).then(response => {
            console.log(response.status);
            switch (response.status) {
                case 200:
                    response
                        .json()
                        .then(json => {
                            console.log(json);
                            this.setState({
                                allUsersData: json,
                                selectedRow: 0
                            })
                        })
                        .catch(err => {
                            this.setState({
                                error: {
                                    isError: true,
                                    errorMessage: "Blad deserializacji odpowiedzi serwera do formatu JSON"
                                }
                            });
                        });
                    break;
                default:
                    let error = new Error(response.statusText);
                    error.statusCode = response.status;

                    //todo: poprawic to/usunac
                    if (response.bodyUsed) {
                        response
                            .json()
                            .then(x => {
                                console.log(x);
                                error.errorMessage = x;
                                throw error;
                            });
                    };

                    throw error;
            }
        }).catch(err => {
            console.log(err);
            this.setApiFetchError(err);
        });
    }

    setApiFetchError(error) {
        let errorMessage = `${error.message}`;
        //console.log(error); console.log(error.statusCode);
        if (error.statusCode) {
            errorMessage = `Błąd ${error.statusCode}: `.concat(errorMessage);
        }

        if (error.errorMessge) {
            errorMessage += "Dodatkowe informacje: " + error.errorMessage;
        }

        this.setState({
            error: {
                isError: true,
                errorMessage: errorMessage
            }
        });
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
                            {<UsersAddEditForm
                                isEdit={this.state.selectedRow}
                                editedUserData={this.state.selectedRow ? this.state.allUsersData[this.state.selectedRow - 1] : null}
                                refreshCallback={this.getAllUsers}
                                errorCallback={this.setApiFetchError}
                            />}
                        </div>
                    </div>
                </div>
            </Fragment>
        );
    }
}

export default UserManage;