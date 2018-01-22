import React, { Component } from 'react';


class ActiveAcDeviceBox extends Component {

    render() {
        let activeAcDeviceSpan;
        if (this.props.activeAcDevice)
            activeAcDeviceSpan = <span className="tag is-success is-medium">{`${this.props.activeAcDevice.brand} ${this.props.activeAcDevice.model} Id: ${this.props.activeAcDevice.id}`}</span>
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
}

export default ActiveAcDeviceBox;