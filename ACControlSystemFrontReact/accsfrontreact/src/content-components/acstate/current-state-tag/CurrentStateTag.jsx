import React from 'react';
import "./CurrentStateTag.css"

const CurrentStateTag = (props) =>{
    switch (props.tagState) {
        case "on":
            this.tagCssClass = "tag is-success is-large";
            this.tagText = "Włączony";
            break;
        case "off":
            this.tagCssClass = "tag is-danger is-large";
            this.tagText = "Wyłączony";
            break;
        default:
            this.tagCssClass = "tag is-warning is-large";
            this.tagText = "Nieokreślony";
            break;
    };

    return (
        <a onClick={props.onClick} title="Kliknij aby odświeżyć">
            <span className={this.tagCssClass}>
                {this.tagText}
            </span>
        </a>
    );
}

export default CurrentStateTag;