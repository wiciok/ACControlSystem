import React, { Component, Fragment } from 'react';
import 'bulma/css/bulma.css';

class AcSettingAdd extends Component {
    constructor(props) {
        super(props);

        this.onRegisterButtonClick = this.onRegisterButtonClick.bind(this);
        this.onRadioChange = this.onRadioChange.bind(this);
        this.onAddOptionButtonClick = this.onAddOptionButtonClick.bind(this);
        this.registerRef = this.registerRef.bind(this);

        this.optionsList = {
            keys: [],
            values: []
        }

        this.state = {
            checkedRadio: 'on',
            renderedOptionsNumber: 0
        }
    }

    onRadioChange(changeEvent) {
        let selectedOptionVal = changeEvent.target.value;
        this.setState({ checkedRadio: selectedOptionVal });
    }

    onRegisterButtonClick() {
        let optionsObj = {};

        for (let index in this.optionsList.keys) {
            let key = this.optionsList.keys[index].value;
            let value = this.optionsList.values[index].value;
            Object.defineProperty(optionsObj, key, {
                value: value,
                writable: false,
                enumerable: true
            });
        }

        let newObj = {
            isTurnOff: !(this.state.checkedRadio === 'on'),
            settings: optionsObj
        }

        this.props.onAddButtonClick(newObj, this.registerButton);
    }

    onAddOptionButtonClick() {
        this.optionsList.keys = [];
        this.optionsList.values = [];
        this.setState({
            renderedOptionsNumber: this.state.renderedOptionsNumber + 1
        })
    }

    registerRef(val, isKey) {
        if (val == null)
            return;
        if (isKey)
            this.optionsList.keys.push(val);
        else
            this.optionsList.values.push(val);
    }

    render() {
        let options;
        if (this.state.renderedOptionsNumber === 0)
            options = null;
        else {
            let keys = [];
            let values = [];
            for (let i = 0; i < this.state.renderedOptionsNumber; i++) {
                keys.push(
                    <div className='field' key={i}>
                        <input className="input" type="text" placeholder="Nazwa opcji, np. Temperatura" ref={val => { this.registerRef(val, true) }} />
                    </div>);

                values.push(
                    <div className='field' key={i}>
                        <input className="input" type="text" placeholder="Wartość opcji, np. 25°C" ref={val => { this.registerRef(val, false) }} />
                    </div>);
            }
            options =
                <div className="columns">
                    <div className="column">
                        <label>Nazwa opcji:</label>
                        <br />
                        {keys}
                    </div>

                    <div className="column">
                        <label>Wartość opcji:</label>
                        <br />
                        {values}
                    </div>
                </div>
        }

        return (
            <Fragment>
                <label>Wybierz typ ustawienia:</label>

                <div className="control">
                    <label className="radio">
                        <input type="radio" checked={this.state.checkedRadio === 'on'} name="onoff" value="on" onChange={this.onRadioChange} />
                        Włączanie
                    </label>
                    <label className="radio">
                        <input type="radio" checked={this.state.checkedRadio === 'off'} name="onoff" value="off" onChange={this.onRadioChange} />
                        Wyłączanie
                    </label>
                </div>

                {options}


                <button className="button is-link is-primary" onClick={this.onRegisterButtonClick} ref={registerButton => this.registerButton = registerButton}>
                    Zarejestruj nowe ustawienie klimatyzatora
                </button>
                &emsp;
                <button className="button is-link is-success" onClick={this.onAddOptionButtonClick} ref={addOptionButton => this.addOptionButton = addOptionButton}>
                    Dodaj nową opcję
                </button>
            </Fragment>
        );
    }
}

export default AcSettingAdd;