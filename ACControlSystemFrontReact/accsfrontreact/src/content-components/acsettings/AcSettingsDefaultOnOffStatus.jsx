import React, { Fragment } from 'react';

const AcSettingsDefaultOnOffStatus = (props) => {
    let turnOnTag, turnOffTag;

    if (props.defaultOn)
        turnOnTag = <span className="tag is-success is-medium">{props.defaultOn.uniqueId}</span>
    else
        turnOnTag = <span className="tag is-danger is-medium" >Brak!</span>


    if (props.defaultOff)
        turnOffTag = <span className="tag is-success is-medium">{props.defaultOff.uniqueId}</span>
    else
        turnOffTag = <span className="tag is-danger is-medium">Brak!</span>

    return (
        <Fragment>
            <label>Ustawienie włączania:</label>&emsp;
            {turnOnTag}
            <br /><br />
            <label>Ustawienie wyłączania:</label>&emsp;
            {turnOffTag}
        </Fragment>
    );
}

export default AcSettingsDefaultOnOffStatus;