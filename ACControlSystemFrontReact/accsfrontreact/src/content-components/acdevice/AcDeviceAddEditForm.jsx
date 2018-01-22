import React, { Component, Fragment } from 'react';
import sendAuth from './../../utils/sendAuth.js';
import { headerAuthAndContentTypeJson, headerAuth } from './../../utils/authenticationHeaders.js';

class AcDeviceAddEditForm extends Component {
    constructor(props) {
        super(props);

        this.onSaveButtonClick = this.onSaveButtonClick.bind(this);
        this.onDeleteButtonClick = this.onDeleteButtonClick.bind(this);
        this.onSetActiveButtonClick = this.onSetActiveButtonClick.bind(this);
        this.changeButtonInProgress = this.changeButtonInProgress.bind(this);
        this.doFetch = this.doFetch.bind(this);

        this.endpointAddress = `${window.apiAddress}/acdevice`;
    }

    componentDidUpdate() {
        if (this.props.editedDeviceData) {
            this.brandInput.value = this.props.editedDeviceData.brand;
            this.modelInput.value = this.props.editedDeviceData.model;
        } else {
            this.brandInput.value = '';
            this.modelInput.value = '';
        }
    }

    onSaveButtonClick() {
        let acDeviceObj = {
            brand: this.brandInput.value,
            model: this.modelInput.value
        }

        let fetchObj = {
            body: JSON.stringify(acDeviceObj),
            headers: headerAuthAndContentTypeJson,
        };

        if (this.props.isEdit) {
            acDeviceObj.id = this.props.editedDeviceData.id;
            fetchObj.method = 'put';
        }
        else {
            acDeviceObj.id = 0;
            fetchObj.method = 'post';
        }

        this.doFetch(fetchObj, this.endpointAddress, this.saveButton, this.onSaveButtonClick);
    }

    onDeleteButtonClick() {
        let fetchObj = {
            method: 'delete',
            headers: headerAuth
        }
        let fullAddress = this.endpointAddress.concat(`/${this.props.editedDeviceData.id}`);

        this.doFetch(fetchObj, fullAddress, this.removeButton, this.onDeleteButtonClick);
    }

    onSetActiveButtonClick() {
        let headers = headerAuthAndContentTypeJson

        let fetchObj = {
            method: 'put',
            body: `${this.props.editedDeviceData.id}`,
            headers: headers
        }
        let fullAddress = this.endpointAddress.concat("/current");

        this.doFetch(fetchObj, fullAddress, this.setActiveButton, this.onSetActiveButtonClick);
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
                    if (response.status === 401) {
                        sendAuth(retryCallback);
                    }

                    let error = new Error(response.statusText);
                    error.statusCode = response.status;

                    response.json().then(data => {
                        error.errorMessage = data;
                        this.props.errorCallback(error);
                    }).catch(() => { this.props.errorCallback(error); });
                }

                this.props.refreshCallback();
            })
            .catch(err => {
                this.props.errorCallback(err);
            });
    }


    render() {
        let idLabel = <label className="label">Id: {this.props.editedDeviceData ? this.props.editedDeviceData.id : null}</label>

        let setActiveButton = <div className="control">
            <button className="button is-link is-success" onClick={this.onSetActiveButtonClick} ref={setActiveButton => this.setActiveButton = setActiveButton}>
                Ustaw jako aktywny
            </button>
        </div>

        let removeButton = <div className="control">
            <button className="button is-link is-danger" onClick={this.onDeleteButtonClick} ref={removeButton => this.removeButton = removeButton}>
                Usuń
            </button>
        </div>

        return (
            <Fragment>
                <h4 className="title is-4">
                    {this.props.isEdit ? "Edytuj klimatyzator:" : "Dodaj klimatyzator"}
                </h4>

                {this.props.isEdit ? idLabel : null}

                <div className="field">
                    <label className="label">Marka:</label>
                    <div className="control">
                        <input
                            className="input"
                            type="text"
                            placeholder="Wpisz tekst"
                            ref={input => this.brandInput = input} />
                    </div>
                </div>

                <div className="field">
                    <label className="label">Model:</label>
                    <div className="control">
                        <input
                            className="input"
                            type="text"
                            placeholder="Wpisz tekst"
                            ref={input => this.modelInput = input} />
                    </div>
                </div>

                <div className="field is-grouped">
                    <div className="control">
                        <button
                            className="button is-link"
                            onClick={this.onSaveButtonClick}
                            ref={saveButton => this.saveButton = saveButton}>Zapisz</button>
                    </div>
                    {this.props.isEdit ? removeButton : null}
                    {this.props.isEdit ? setActiveButton : null}
                </div>
            </Fragment>
        );
    }
}

export default AcDeviceAddEditForm;