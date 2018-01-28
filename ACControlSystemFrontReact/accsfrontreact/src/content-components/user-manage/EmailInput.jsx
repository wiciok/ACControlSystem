import React, { Component } from 'react';
import './EmailInput.css';

class EmailInput extends Component {
    constructor(props) {
        super(props);

        this.onChange = this.onChange.bind(this);
    }

    onChange() {
        if(!this.emailInput.value){
            this.emailInput.classList.remove("is-danger", "is-success");
            this.props.onEmailEntered(null);
            return;
        }

        if (this.validateEmail(this.emailInput.value)) {
            this.emailInput.classList.remove("is-danger");
            this.emailInput.classList.add("is-success");
            this.warningIcon.style.visibility = "hidden";
            this.props.onEmailEntered(this.emailInput.value);

        }
        else {
            this.emailInput.classList.add("is-danger");
            this.emailInput.classList.remove("is-success");
            this.warningIcon.style.visibility = "visible";
            this.props.onEmailEntered(null);
        }
    }

    componentWillReceiveProps(newProps){
        if (this.props.initialValue !== newProps.initialValue) {
            if(newProps.initialValue){
                this.emailInput.value = newProps.initialValue
                this.onChange();
            }
            else{
                this.emailInput.value = '';
                this.onChange();
            }
        }
    }


    validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email.toLowerCase());
    }

    render() {
        return (
            <div className="field">
                <label className="label">Email:</label>
                <div className="control has-icons-left has-icons-right">
                    <input
                        className="input"
                        type="email"
                        placeholder="Wpisz tekst"
                        ref={input => this.emailInput = input}
                        onChange={this.onChange}
                    />
                    <span className="icon is-small is-left">
                        <i className="fa fa-envelope"></i>
                    </span>
                    <span className="icon is-small is-right">
                        <i className="fa fa-warning" ref={icon => this.warningIcon = icon}></i>
                    </span>
                </div>
            </div>
        );
    }
}

export default EmailInput;