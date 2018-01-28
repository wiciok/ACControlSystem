import React, { Fragment } from 'react';
import "./AcSettingTable.css";

const AcSettingTable = (props) => {

    if (!props.acSetting)
        return null;
        

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

    let removeButton =
        <button className="button is-link is-danger" onClick={props.onDeleteButtonClick}>
            Usuń
            </button>

    let setAsDefaultOnOffSettingButton =
        <button className="button is-link is-primary" onClick={props.onSetAsDefaultButtonClick}>
            {props.acSetting.isTurnOff
                ? "Ustaw jako domyślne ust. wyłączania"
                : "Ustaw jako domyślne ust. włączania"}
        </button>

    return (
        <Fragment>
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
            <span className="control">
                {removeButton}&emsp;
                    {setAsDefaultOnOffSettingButton}
            </span>
        </Fragment>
    );
}

export default AcSettingTable;