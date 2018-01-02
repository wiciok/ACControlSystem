import React, {Component} from 'react';
import 'bulma/css/bulma.css';

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

class ToggleStateButton extends Component {
    constructor(props) {
        super(props);
        this.cssClass = "button is-medium is-outlined";

        switch (this.props.actionType) {
            case "on":
                this.colorClass="is-success";                
                this.tagText = "Włącz";
                break;
            case "off":
                this.colorClass="is-danger";
                this.tagText = "Wyłącz";
                break;
        };
        this.cssClass+=` ${this.colorClass}`;
    }

    async toggleStateFunc() {
        this
            .changeInProgressAppeareance
            .bind(this);
        this.changeInProgressAppeareance(true);

        await sleep(2000);

        this.changeInProgressAppeareance(false);
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