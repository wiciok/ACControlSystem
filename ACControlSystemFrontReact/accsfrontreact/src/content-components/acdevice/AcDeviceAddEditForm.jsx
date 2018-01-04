import React, { Component, Fragment } from 'react';

class AcDeviceAddEditForm extends Component {
    constructor(props) {
        super(props);

        this.onSaveButtonClick = this.onSaveButtonClick.bind(this);
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
        }
    }

    onSaveButtonClick() {
        let acDeviceObj = {
            brand: this.brandInput.value,
            model: this.modelInput.value
        }

        let fetchObj;
        if (this.props.isEdit) {
            acDeviceObj.id = this.props.editedDeviceData.id;
            fetchObj = {
                method: 'put',
                body: JSON.stringify(acDeviceObj)
            }
        }
        else {
            acDeviceObj.id = 0;
            fetchObj = {
                method: 'post',
                body: JSON.stringify(acDeviceObj)
            }
        }

        this.doFetch(acDeviceObj, fetchObj);
    }

    changeButtonInProgress(isInProgress) {
        if (isInProgress === true) {
            this.saveButton.classList.add("is-loading");
            this.saveButton.classList.remove("is-primary");
        } else {
            this.saveButton.classList.remove("is-loading");
            this.saveButton.classList.add("is-primary");
        }
    }

    doFetch(acDeviceObj, fetchObj) {
        let fullAddress = this.endpointAddress.concat("/123temporaryfaketoken");
        fetchObj.headers = new Headers({ "Content-Type": "application/json" });

        this.changeButtonInProgress(true);

        fetch(fullAddress, fetchObj)
            .then(response => {
                console.log("response: " + response.status);

                this.changeButtonInProgress(false);

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
        let setActiveButton = <div className="control">
            <button className="button is-link is-success">Ustaw jako aktywny</button>
        </div>

        let idLabel = <label className="label">Id: {this.props.editedDeviceData ? this.props.editedDeviceData.id : null}</label>

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

                    {this.props.isEdit ? setActiveButton : null}
                </div>
            </Fragment>
        );
    }
}

export default AcDeviceAddEditForm;