import React, { Component } from 'react';

class InputDayOfWeek extends Component {
    constructor(props) {
        super(props);

        this.onSelectChange = this.onSelectChange.bind(this);
    }

    onSelectChange() {
        let val = Number.parseInt(this.select.value, 10);
        this.props.onChanged(val);
    }

    render() {
        let options = this.props.weekDaysArray.map((item, index) =>
            <option value={index} key={index}>{item}</option>
        );

        return (
            <div className="select">
                <select onChange={this.onSelectChange} ref={sel => this.select = sel}>{options}</select>
            </div>
        );
    }
}

export default InputDayOfWeek;