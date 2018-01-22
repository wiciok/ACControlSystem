import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import { sha256 } from 'js-sha256';
import EmailInput from '../content-components/user-manage/EmailInput';
import PasswordInput from '../content-components/user-manage/PasswordInput';
import ErrorMessageComponent from '../ErrorMessageComponent';
import setApiFetchError from '../setApiFetchError.js';

class LoginPage extends Component {
    constructor(props) {
        super(props);

        this.onEmailEntered = this.onEmailEntered.bind(this);
        this.onPasswordEntered = this.onPasswordEntered.bind(this);
        this.onLoginButtonClick = this.onLoginButtonClick.bind(this);
        this.setApiFetchError = setApiFetchError.bind(this);

        this.state = {
            emailEntered: false,
            passwordEntered: false,
            error: {
                isError: false,
                errorMessage: null
            }
        }

        this.email = null;
        this.password = null;
    }

    onEmailEntered(val) {
        if (val == null)
            return;

        this.email = val;
        this.setState({
            emailEntered: true
        });
    }

    onPasswordEntered(val) {
        if (val == null)
            return;

        this.password = val;
        this.setState({
            passwordEntered: true
        });
    }

    onLoginButtonClick() {

        let endpointAddress = `${window.apiAddress}/auth`;
        let authObj = {
            EmailAddress: this.email,
            PasswordHash: sha256(this.password)
        };

        let fetchObj = {
            method: 'post',
            body: JSON.stringify(authObj),
            headers: new Headers({ "Content-Type": "application/json" })
        };

        fetch(endpointAddress, fetchObj).then(response => {
            if (response.status === 200) {
                response.json().then(token=>{
                    this.props.onLogin(this.email, sha256(this.password), token);
                })
            }
            else if (response.status === 401) {
                let error = new Error("Błąd logowania! Błędne hasło!");
                
                this.setApiFetchError(error);
            }
            else if (response.status === 400) {
                let error = new Error("Błąd logowania! Brak konta o podanym adresie email!");
                
                this.setApiFetchError(error);
            }
            else {
                let error = new Error(response.statusText);
                error.statusCode = response.status;
                response.json().then(x => {
                    console.log(x);
                    error.errorMessage = x;
                    this.setApiFetchError(error);
                });
                this.setApiFetchError(error);
            }
        })
    }

    render() {
        let saveButton = this.state.passwordEntered && this.state.emailEntered
            ? <button
                className="button is-link"
                onClick={this.onLoginButtonClick}>
                Zaloguj
            </button>
            : <button
                className="button is-link"
                disabled>
                Zaloguj
            </button>

        return (
            <div className="columns">
                <div className="column is-3">
                    <aside className="menu">
                        <p className="menu-label">Logowanie</p>
                        <ul className="menu-list">
                            <NavLink to='/login' activeClassName="is-active">
                                Logowanie
                            </NavLink>
                        </ul>
                    </aside>
                </div>
                <div className="column is-9">
                    <h2 className="title is-2">Logowanie</h2>
                    <div className="box">
                        <ErrorMessageComponent
                            isVisible={this.state.error.isError}
                            bodyText={this.state.error.errorMessage}
                            onChangeErrorState={e => {
                                this.setState({
                                    error: {
                                        isError: false,
                                        errorMessage: null
                                    }
                                })
                            }} />
                        <EmailInput onEmailEntered={this.onEmailEntered} />
                        <PasswordInput onPasswordEntered={this.onPasswordEntered} />
                        {saveButton}
                    </div>
                </div>
            </div>
        );
    }
}

export default LoginPage;