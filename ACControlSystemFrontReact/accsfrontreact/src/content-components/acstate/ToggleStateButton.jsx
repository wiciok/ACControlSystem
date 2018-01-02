import React, {Component} from 'react';
import 'bulma/css/bulma.css';

class ToggleStateButton extends Component {
    constructor(props) {
        super(props);
        this
            .toggleStateFunc
            .bind(this);
        this.cssClassBasic = "button is-medium is-outlined";
        this.cssClass = this.cssClassBasic;

        switch (this.props.actionType) {
            case "on":
                this.cssClass += " is-success ";
                this.tagText = "Włącz";
                break;
            case "off":
                this.cssClass += " is-danger ";
                this.tagText = "Wyłącz";
                break;
        };
    }

    toggleStateFunc() {
        /*this.refs.button.className = this.cssClassBasic;
        this.refs.button.className += "is-loading";*/
        //todo: use refs to add is-loading style while async actions are processed

    }

    render() {
        return (
            <button onClick={this.toggleStateFunc} className={this.cssClass}>

                {this.tagText}
            </button>
        );
    }
}

export default ToggleStateButton;