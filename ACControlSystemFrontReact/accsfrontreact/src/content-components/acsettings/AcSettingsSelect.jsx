import React, { Component, Fragment } from 'react';

class AcSettingsSelect extends Component {
    constructor(props) {
        super(props);

        this.onSelectionChanged = this.onSelectionChanged.bind(this);
    }

    onSelectionChanged(event) {
        if (this.props.allAcSettings) {
            let selectedItemGuid = event.target.value;
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
                    readableName = readableName.concat("(Brak opcji)");

                readableName = readableName.concat(`, id: ${object.uniqueId}`);


                return <option key={index} value={object.uniqueId}>{readableName}</option>
            })

            if(this.props.dummyInitialValue)
                select =
                    <div className="select">
                        <select id='test' onChange={this.onSelectionChanged}>
                            <option value={null} disabled selected>Wybierz ustawienie...</option>
                            {options}
                        </select>
                    </div>
            else
                select =
                <div className="select">
                    <select id='test' onChange={this.onSelectionChanged}>
                        {options}
                    </select>
                </div>
        }

        return (
            <Fragment>
                {select || "Brak ustawień do wyboru!"}
            </Fragment>
        );
    }
}

export default AcSettingsSelect;