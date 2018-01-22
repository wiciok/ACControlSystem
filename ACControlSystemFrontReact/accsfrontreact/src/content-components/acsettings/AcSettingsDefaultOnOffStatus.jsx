import React, { Component, Fragment } from 'react';

class AcSettingsDefaultOnOffStatus extends Component {
    render() {
        let turnOnTag;
        let turnOffTag;

        if (this.props.defaultOn)
            turnOnTag = <span className="tag is-success is-medium">{this.props.defaultOn.uniqueId}</span>
        else
            turnOnTag = <span className="tag is-danger is-medium" >Brak!</span>


        if (this.props.defaultOff) 
            turnOffTag = <span className="tag is-success is-medium">{this.props.defaultOff.uniqueId}</span>
        else
            turnOffTag = <span className="tag is-danger is-medium">Brak!</span>


        console.log(this.props.defaultOff, this.props.defaultOn);

        return (
            <Fragment>
                <label>Ustawienie włączania:</label>&emsp;
                {turnOnTag}
                <br/><br/>
                <label>Ustawienie wyłączania:</label>&emsp;
                {turnOffTag}
            </Fragment>
        );
    }
}

export default AcSettingsDefaultOnOffStatus;