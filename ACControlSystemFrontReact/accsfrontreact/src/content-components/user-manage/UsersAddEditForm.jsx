import React, { Component, Fragment } from 'react';
import EmailInput from './EmailInput';
import 'bulma/css/bulma.css';
import 'font-awesome/css/font-awesome.min.css'

class UserAddEditForm extends Component {
    constructor(props) {
        super(props);

        this.onSaveButtonClick = this.onSaveButtonClick.bind(this);
        this.onDeleteButtonClick = this.onDeleteButtonClick.bind(this);
        this.changeButtonInProgress = this.changeButtonInProgress.bind(this);
        this.onEmailEntered = this.onEmailEntered.bind(this);
        this.doFetch = this.doFetch.bind(this);

        this.endpointAddress = `${window.apiAddress}/user`;

        this.state = {
            emailAddress: null
        }
    }

    onEmailEntered(email) {
        this.setState({
            emailAddress: email
        });
    }

    onSaveButtonClick() {
        let userObj = {
            brand: this.emailInput.value
        }

        let fetchObj;
        if (this.props.isEdit) {
            userObj.id = this.props.editedUserData.id;
            fetchObj = {
                method: 'put',
                body: JSON.stringify(userObj)
            }
        }
        else {
            userObj.id = 0;
            fetchObj = {
                method: 'post',
                body: JSON.stringify(userObj)
            }
        }
        fetchObj.headers = new Headers({ "Content-Type": "application/json" });
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken");

        this.doFetch(fetchObj, fullAddress, this.saveButton);
    }

    onDeleteButtonClick() {
        let fetchObj = {
            method: 'delete'
        }
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken").concat(`/${this.props.editedDeviceData.id}`);

        this.doFetch(fetchObj, fullAddress, this.removeButton);
    }


    changeButtonInProgress(isInProgress, button) {
        if (isInProgress === true) {
            button.classList.add("is-loading");
            button.classList.remove("is-primary");
        } else {
            button.classList.remove("is-loading");
            button.classList.add("is-primary");
        }
    }

    doFetch(fetchObj, fullAddress, button) {
        this.changeButtonInProgress(true, button);

        fetch(fullAddress, fetchObj)
            .then(response => {
                console.log("response: " + response.status);

                this.changeButtonInProgress(false, button);

                if (!response.ok) {
                    let error = new Error(response.statusText);
                    error.statusCode = response.status;

                    //todo: poprawic to/usunac
                    if (response.bodyUsed) {
                        response.json()
                            .then(x => {
                                console.log(x);
                                error.errorMessage = x;
                                throw error;
                            });
                    };
                    throw error;
                }

                this.props.refreshCallback();
            })
            .catch(err => {
                //console.log("error in toggle state: "+err);
                this.props.errorCallback(err);
            });
    }


    render() {
        let idLabel = <label className="label">Id: {this.props.editedUserData ? this.props.editedUserData.id : null}</label>
        let emailInput = (this.props.editedUserData)
            ? <EmailInput initialValue={this.props.editedUserData.emailAddress} onEmailEntered={this.onEmailEntered} />
            : <EmailInput onEmailEntered={this.onEmailEntered} />

        let removeButton = <div className="control">
            <button className="button is-link is-danger" onClick={this.onDeleteButtonClick} ref={removeButton => this.removeButton = removeButton}>
                Usuń
            </button>
        </div>

        let saveButton = this.state.emailAddress
            ?   <button
                    className="button is-link"
                    onClick={this.onSaveButtonClick}
                    ref={saveButton => this.saveButton = saveButton}>
                    Zapisz
                </button>
            :   <button
                    className="button is-link"
                    onClick={this.onSaveButtonClick}
                    disabled
                    ref={saveButton => this.saveButton = saveButton}>
                    Zapisz
                </button>

        let captionText = this.props.isEdit ? "Edytuj użytkownika:" : "Dodaj użytkownika";

        return (
            <Fragment>
                <h4 className="title is-4">
                    {captionText}
                </h4>

                {this.props.isEdit ? idLabel : null}

                {emailInput}

                <div className="field is-grouped">
                    <div className="control">
                        {saveButton}
                    </div>
                    {this.props.isEdit ? removeButton : null}
                </div>
            </Fragment>
        );
    }
}

export default UserAddEditForm;