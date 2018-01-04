import React, {Component, Fragment} from 'react';

class AcDeviceAddEditForm extends Component {
    render() {
        let setActiveButton = <div className="control">
            <button className="button is-link is-success">Ustaw jako aktywny</button>
        </div>

        let idLabel = <label className="label">Id: {this.props.editedDeviceData ? this.props.editedDeviceData.id : null}</label>

        return (
            <Fragment>
                <h4 className="title is-4">
                    {this.props.isEdit
                        ? "Edytuj klimatyzator:"
                        : "Dodaj klimatyzator"}
                </h4>
                
                {this.props.isEdit ? idLabel : null}

                <div className="field">
                    <label className="label">Marka:</label>
                    <div className="control">
                        <input className="input" type="text" placeholder="Wpisz tekst"/>
                    </div>
                </div>

                <div className="field">
                    <label className="label">Model:</label>
                    <div className="control">
                        <input className="input" type="text" placeholder="Wpisz tekst"/>
                    </div>
                </div>

                <div className="field is-grouped">
                    <div className="control">
                        <button className="button is-link">Zapisz</button>
                    </div>
                    {this.props.isEdit
                        ? setActiveButton
                        : null}

                </div>
            </Fragment>
        );
    }
}

export default AcDeviceAddEditForm;