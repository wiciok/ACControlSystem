import React, { Component } from 'react';

class AcSettingsSelect extends Component {
    constructor(props) {
        super(props);

        this.onSelectionChanged = this.onSelectionChanged.bind(this);
        this.initial = true;
    }

    componentDidUpdate() {
        if (this.initial)
            this.onSelectionChanged();
    }

    onSelectionChanged() {
        if (this.props.allAcSettings) {
            let selectedItemGuid = this.select.value;
            this.initial = false;
            this.props.onChange(selectedItemGuid);
        }
    }

    render() {
        let options;
        let select;
        if (this.props.allAcSettings) {
            options = this.props.allAcSettings.map((object, index) => {
                let readableName = "";
                if (object.settings) {
                    for (let key in object.settings) {
                        let value = object.settings[key];
                        readableName = readableName.concat(`${key} : ${value}`);
                    }
                }
                else 
                    readableName = readableName.concat("(Brak ustawień)");
                
                readableName = readableName.concat(`, id: ${object.uniqueId}`);


                return <option key={index} value={object.uniqueId}>{readableName}</option>
            })

            select = <select id='test' ref={select => this.select = select} onChange={this.onSelectionChanged}>
                {options}
            </select>
        }

        return (
            <div className="select">
                {this.props.allAcSettings ? select : "Brak dostępnych ustawień!"}
            </div>
        );
    }
}

export default AcSettingsSelect;