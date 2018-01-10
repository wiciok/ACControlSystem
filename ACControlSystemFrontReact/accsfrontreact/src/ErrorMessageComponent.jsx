import React, {Component} from 'react';
import './ErrorMessageComponent.css';

class ErrorMessageComponent extends Component {
    constructor(props) {
        super(props);
        this.headerText = this.props.headerText
            ? this.props.headerText
            : "Błąd";
    }

    render() {
        if (this.props.isVisible === true) {
            return (
                <article className="message is-danger">
                    <div className="message-header">
                        <p>{this.headerText}</p>
                        <button
                            className="delete"
                            aria-label="delete"
                            onClick={this.props.onChangeErrorState}/>
                    </div>
                    <div className="message-body whitespace">
                        {this.props.bodyText}
                    </div>
                </article>
            );
        } else {
            return null;
        }
    }
}

export default ErrorMessageComponent;