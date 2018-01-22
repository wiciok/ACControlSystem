import React from 'react';

const ActiveAcDeviceBox = (props) => {
    let activeAcDeviceSpan;
    if (props.activeAcDevice)
        activeAcDeviceSpan = <span className="tag is-success is-medium">{`${props.activeAcDevice.brand} ${props.activeAcDevice.model} Id: ${props.activeAcDevice.id}`}</span>
    else
        activeAcDeviceSpan = <span className="tag is-danger is-medium">Brak</span>

    return (
        <div className="box">
            <span>
                <span className="title is-4">Aktywny klimatyzator: </span>
                {activeAcDeviceSpan}
            </span>
        </div>
    );
}

export default ActiveAcDeviceBox;