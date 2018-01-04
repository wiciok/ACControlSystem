import React, {Component} from 'react';
import 'bulma/css/bulma.css';

class ToggleStateButton extends Component {
    constructor(props) {
        super(props);
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
        this
            .changeInProgressAppeareance
            .bind(this);

        this.changeInProgressAppeareance(true);

        let endpointAddress = `${window.apiAddress}/acstate`;
        //console.log("post " + endpointAddress);

        let headers = new Headers({"Content-Type": "application/json"});

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

        //console.log(acStateObj);
        //console.log(JSON.stringify(acStateObj));

        fetch(endpointAddress, {
            method: 'post',
            headers: headers,
            body: JSON.stringify(acStateObj)
        }).then(response => {
            console.log("response: " + response.status);

            this.changeInProgressAppeareance(false);

            if (!response.ok) {
                let error = new Error(response.statusText);
                error.statusCode = response.status;

                //todo: poprawic to/usunac
                if (response.bodyUsed) {
                    response.json().then(x=>{
                        console.log(x);
                        error.errorMessage=x;
                        throw error;
                    });                   
                };
                throw error;
            }

            this.props.stateRefreshCallback();

        }).catch(err => {
            //console.log("error in toggle state: "+err);
            this
                .props
                .setErrorCallback(err);
        });

    }

    changeInProgressAppeareance(isInProgress) {
        if (isInProgress === true) {
            this
                .toggleButton
                .classList
                .add("is-loading");
            this
                .toggleButton
                .classList
                .remove(this.colorClass);
        } else {
            this
                .toggleButton
                .classList
                .remove("is-loading");
            this
                .toggleButton
                .classList
                .add(this.colorClass);
        }
    }

    render() {
        return (
            <button
                onClick={this
                .toggleStateFunc
                .bind(this)}
                className={this.cssClass}
                name="toggleButton"
                ref={toggleButton => this.toggleButton = toggleButton}>
                {this.tagText}
            </button>
        );
    }
}

export default ToggleStateButton;