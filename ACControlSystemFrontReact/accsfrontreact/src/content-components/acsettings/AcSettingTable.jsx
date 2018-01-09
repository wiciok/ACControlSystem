import React, { Component } from 'react';
import "./AcSettingTable.css";

class AcSettingTable extends Component {
    render() {
        if (this.props.acSetting) {
            let customSettings = [];
            if (this.props.acSetting.settings) {
                let i = 0;
                for (let key in this.props.acSetting.settings) {
                    let value = this.props.acSetting.settings[key];
                    customSettings.push(
                        <tr key={i++}>
                            <td>{key}</td>
                            <td>{value}</td>
                        </tr>);
                }
            }
            else
                customSettings = null;

            return (
                <div className="overflow">
                    <table className="table">
                        <thead>
                            <tr>
                                <th>Ustawienie</th>
                                <th>Wartość</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th>Id</th>
                                <th>{this.props.acSetting.uniqueId}</th>
                            </tr>
                            <tr>
                                <th>Wł./wył.</th>
                                <th>{this.props.acSetting.isTurnOff ? "Wyłączanie" : "Włączanie"}</th>
                            </tr>
                            {customSettings}

                        </tbody>
                    </table>
                </div>
            );
        }
        else {
            return <h4>Brak ustawień do wyświetlenia!</h4>
        }
    }
}

export default AcSettingTable;