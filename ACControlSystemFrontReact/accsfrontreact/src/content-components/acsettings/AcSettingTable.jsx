import React from 'react';
import "./AcSettingTable.css";

const AcSettingTable = (props) => {

    if (props.acSetting) {
        let customSettings = [];

        if (props.acSetting.settings) {
            let i = 0;
            for (let key in props.acSetting.settings) {
                let value = props.acSetting.settings[key];
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
                            <th>Opcja</th>
                            <th>Wartość</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>Id</th>
                            <th>{props.acSetting.uniqueId}</th>
                        </tr>
                        <tr>
                            <th>Wł./wył.</th>
                            <th>{props.acSetting.isTurnOff ? "Wyłączanie" : "Włączanie"}</th>
                        </tr>
                        {customSettings}

                    </tbody>
                </table>
            </div>
        );
    }
    else 
        return <h4>Brak opcji do wyświetlenia!</h4>
}

export default AcSettingTable;