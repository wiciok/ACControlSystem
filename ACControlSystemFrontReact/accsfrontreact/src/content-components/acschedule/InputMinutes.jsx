import React, { Component } from 'react';
import 'bulma/css/bulma.css';

class InputMinutes extends Component {
    constructor(props) {
        super(props);

        this.onChanged = this.onChanged.bind(this);
    }

    onChanged() {
        let val = this.input.value;
        if(this.timeValidation(val)){
            this.input.classList.add("is-success");
            this.input.classList.remove("is-danger");

            this.props.onChanged(Number.parseInt(val, 10));
        }
        else{
            this.input.classList.remove("is-success");
            this.input.classList.add("is-danger");
        }
    }


    timeValidation(strTime) {
        var timeFormat = /^[0-5][0-9]/;
        return timeFormat.test(strTime);
    }


    render() {
        return (
            <div className="field">
                <div className="control">
                    <input type="text" className="input" ref={input => this.input = input} onChange={this.onChanged}/>
                </div>
            </div>
        );
    }
}

export default InputMinutes;