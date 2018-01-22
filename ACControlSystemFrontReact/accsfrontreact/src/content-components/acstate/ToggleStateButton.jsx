import React, { Component } from 'react';
import sendAuth from '../../sendAuth.js';
import 'bulma/css/bulma.css';
import { headerAuthAndContentTypeJson } from '../../authenticationHeaders.js';

class ToggleStateButton extends Component {
    constructor(props) {
        super(props);

        this.toggleStateFunc = this.toggleStateFunc.bind(this);
        this.changeInProgressAppeareance = this.changeInProgressAppeareance.bind(this);

        this.cssClass = "button is-medium is-outlined";

        switch (this.props.actionType) {
            case "on":
                this.colorClass = "is-success";
                this.tagText = "Włącz";
                break;
            case "off":
                this.colorClass = "is-danger";
                this.tagText = "Wyłącz";
                break;
            default:
                break;
        };
        this.cssClass += ` ${this.colorClass}`;
    }

    toggleStateFunc() {
        this.changeInProgressAppeareance(true);

        let endpointAddress = `${window.apiAddress}/acstate`;

        let acStateObj;
        switch (this.props.actionType) {
            case "on":
                acStateObj = {
                    isTurnOff: false,
                    acSettingGuid: null
                }
                break;
            case "off":
                acStateObj = {
                    isTurnOff: true,
                    acSettingGuid: null
                }
                break;
            default:
                break;
        };

        let fetchObj = {
            method: 'post',
            headers: headerAuthAndContentTypeJson,
            body: JSON.stringify(acStateObj)
        }

        fetch(endpointAddress, fetchObj).then(response => {

            this.changeInProgressAppeareance(false);

            if (!response.ok) {
                if (response.status === 401)
                    sendAuth(this.toggleStateFunc);


                let error = new Error(response.statusText);
                error.statusCode = response.status;

                response.json().then(data => {
                    error.errorMessage = data;
                    this.props.setErrorCallback(error);
                }).catch(() => { this.props.setErrorCallback(error); });
            }
            else
                this.props.stateRefreshCallback();

        }).catch(err => { this.props.setErrorCallback(err); });
    }

    changeInProgressAppeareance(isInProgress) {
        if (isInProgress === true) {
            this.toggleButton.classList.add("is-loading");
            this.toggleButton.classList.remove(this.colorClass);
        } else {
            this.toggleButton.classList.remove("is-loading");
            this.toggleButton.classList.add(this.colorClass);
        }
    }

    render() {
        return (
            <button
                onClick={this.toggleStateFunc}
                className={this.cssClass}
                name="toggleButton"
                ref={toggleButton => this.toggleButton = toggleButton}>
                {this.tagText}
            </button>
        );
    }
}

export default ToggleStateButton;