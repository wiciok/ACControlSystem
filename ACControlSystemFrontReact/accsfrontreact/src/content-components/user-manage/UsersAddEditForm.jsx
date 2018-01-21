import React, { Component, Fragment } from 'react';
import EmailInput from './EmailInput';
import { sha256 } from 'js-sha256';
import 'bulma/css/bulma.css';
import 'font-awesome/css/font-awesome.min.css'
import PasswordInput from './PasswordInput';

class UserAddEditForm extends Component {
    constructor(props) {
        super(props);

        this.onSaveButtonClick = this.onSaveButtonClick.bind(this);
        this.onDeleteButtonClick = this.onDeleteButtonClick.bind(this);
        this.changeButtonInProgress = this.changeButtonInProgress.bind(this);
        this.saveButtonChange = this.saveButtonChange.bind(this);
        this.onEmailEntered = this.onEmailEntered.bind(this);
        this.onPasswordEntered = this.onPasswordEntered.bind(this);
        this.doFetch = this.doFetch.bind(this);

        this.endpointAddress = `${window.apiAddress}/user`;

        this.state = {
            saveButtonActive: null,
            emailAddress: null,
            password: null
        }
    }

    onEmailEntered(email) {
        this.setState({
            emailAddress: email
        });
        this.saveButtonChange(email, this.state.password);
    }

    onPasswordEntered(password) {
        this.setState({
            password: password
        })
        this.saveButtonChange(this.state.emailAddress, password);
    }

    saveButtonChange(email, password) {
        this.setState({
            saveButtonActive: email && password
        })
    }

    onSaveButtonClick() {
        let userRegisterObject = {
            authenticationData: {
                EmailAddress: this.state.emailAddress,
                Password: sha256(this.state.password) 
            }
        }

        let fetchObj;
        if (this.props.isEdit) {
            userRegisterObject.id = this.props.editedUserData.id;
            fetchObj = {
                method: 'put',
                body: JSON.stringify(userRegisterObject)
            }
        }
        else {
            fetchObj = {
                method: 'post',
                body: JSON.stringify(userRegisterObject)
            }
        }
        fetchObj.headers = new Headers({ "Content-Type": "application/json" });
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken");

        console.log(fetchObj);

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

                    response.json().then(x => {
                        console.log(x);
                        error.errorMessage = x;
                        this.props.errorCallback(error);
                    });
                    this.props.errorCallback(error);
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

        let saveButton = this.state.saveButtonActive
            ? <button
                className="button is-link"
                onClick={this.onSaveButtonClick}
                ref={saveButton => this.saveButton = saveButton}>
                Zapisz
                </button>
            : <button
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
                <PasswordInput initialValue={this.props.editedUserData} onPasswordEntered={this.onPasswordEntered} />

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