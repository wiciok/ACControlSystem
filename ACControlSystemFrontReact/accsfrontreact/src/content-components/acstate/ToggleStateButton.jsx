import React, { Component } from 'react';
import 'bulma/css/bulma.css';

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
        //console.log("post " + endpointAddress);

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

        fetch(endpointAddress, {
            method: 'post',
            headers: new Headers({ "Content-Type": "application/json" }),
            body: JSON.stringify(acStateObj)
        }).then(response => {
            console.log("response: " + response.status);

            this.changeInProgressAppeareance(false);

            if (!response.ok) {
                let error = new Error(response.statusText);
                error.statusCode = response.status;

                response.json().then(x => {
                    console.log(x);
                    error.errorMessage = x;
                    throw error;
                });
                throw error;
            }
            else
                this.props.stateRefreshCallback();
        }).catch(err => {
            this.props.setErrorCallback(err);
        });
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