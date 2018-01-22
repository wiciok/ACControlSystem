import React from 'react';
import './ErrorMessageComponent.css';

const ErrorMessageComponent = (props) => {
    if (props.isVisible === true)
        return (
            <article className="message is-danger">
                <div className="message-header">
                    <p>{props.headerText ? props.headerText : "Błąd" }</p>
                    <button
                        className="delete"
                        aria-label="delete"
                        onClick={props.onChangeErrorState} />
                </div>
                <div className="message-body whitespace">
                    {props.bodyText}
                </div>
            </article>
        );
    else
        return null;
}

export default ErrorMessageComponent;