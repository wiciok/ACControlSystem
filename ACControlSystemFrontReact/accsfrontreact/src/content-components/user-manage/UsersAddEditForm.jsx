import React, { Component, Fragment } from 'react';
import Cookies from 'js-cookie';
import { sha256 } from 'js-sha256';
import 'bulma/css/bulma.css';
import 'font-awesome/css/font-awesome.min.css'

import sendAuth from './../../utils/sendAuth.js';
import { headerAuthFun, headerAuthAndContentTypeJsonFun } from './../../utils/authenticationHeaders.js';

import EmailInput from './EmailInput';
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
            EmailAddress: this.state.emailAddress,
            PasswordHash: sha256(this.state.password)
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
        fetchObj.headers = headerAuthAndContentTypeJsonFun()

        this.doFetch(fetchObj, this.endpointAddress, this.saveButton, this.onSaveButtonClick);
    }

    onDeleteButtonClick() {
        let fetchObj = {
            method: 'delete',
            headers: headerAuthFun()
        }
        let fullAddress = this.endpointAddress.concat(`/${this.props.editedUserData.id}`);


        let loggedUserEmail = Cookies.get('userEmail');
        if (loggedUserEmail === this.props.editedUserData.emailAddress && this.deleteCurrentUserAdditionalActivities())
            this.doFetch(fetchObj, fullAddress, this.removeButton, this.onDeleteButtonClick);
    }

    deleteCurrentUserAdditionalActivities() {
        if (window.confirm('Czy na pewno chcesz usunąć swoje konto?')) {
            Cookies.remove('userEmail', { path: '/' });
            Cookies.remove('userPasswordHash', { path: '/' });
            Cookies.remove('token', { path: '/' });
            return true;
        } else
            return false;
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

    doFetch(fetchObj, fullAddress, button, retryCallback) {
        this.changeButtonInProgress(true, button);

        fetch(fullAddress, fetchObj)
            .then(response => {
                this.changeButtonInProgress(false, button);

                if (!response.ok) {
                    if (response.status === 401)
                        sendAuth(retryCallback, this.props.onLogout);

                    let error = new Error(response.statusText);
                    error.statusCode = response.status;

                    response.json().then(x => {
                        error.errorMessage = x;
                        this.props.errorCallback(error);
                    }).catch(() => { this.props.errorCallback(error); });
                }

                this.props.refreshCallback();
            })
            .catch(err => { this.props.errorCallback(err); });
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
                <h4 className="title is-4">{captionText}</h4>
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