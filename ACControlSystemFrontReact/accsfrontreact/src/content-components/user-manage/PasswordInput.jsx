import React, { Component } from 'react';
import './EmailInput.css';

class PasswordInput extends Component {
    constructor(props) {
        super(props);

        this.onChange = this.onChange.bind(this);
    }

    onChange() {
        if (!this.passwordInput.value) {
            this.passwordInput.classList.remove("is-danger", "is-success");
            this.props.onPasswordEntered(null);
            return;
        }

        if (this.validatePassword(this.passwordInput.value)) {
            this.passwordInput.classList.remove("is-danger");
            this.passwordInput.classList.add("is-success");
            this.warningIcon.style.visibility = "hidden";
            this.props.onPasswordEntered(this.passwordInput.value);

        }
        else {
            this.passwordInput.classList.add("is-danger");
            this.passwordInput.classList.remove("is-success");
            this.warningIcon.style.visibility = "visible";
            this.props.onPasswordEntered(null);
        }
    }


    componentWillReceiveProps(newProps){
        if (this.props.initialValue !== newProps.initialValue) {
            this.passwordInput.value = '';
            this.onChange();
        }
    }


    validatePassword(password) {
        return `${password}`.length >= 6;
    }

    render() {
        return (
            <div className="field">
                <label className="label">Hasło:</label>
                <div className="control has-icons-left has-icons-right">
                    <input
                        className="input"
                        type="password"
                        placeholder="Wpisz tekst"
                        ref={input => this.passwordInput = input}
                        onChange={this.onChange}
                    />
                    <span className="icon is-small is-left">
                        <i className="fa fa-lock"></i>
                    </span>
                    <span className="icon is-small is-right">
                        <i className="fa fa-warning" ref={icon => this.warningIcon = icon}></i>
                    </span>
                </div>
            </div>
        );
    }
}

export default PasswordInput;