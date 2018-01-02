import React, {Component, Fragment} from 'react';

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
                            onClick={e => this
                            .props
                            .onChangeErrorState()}/>
                    </div>
                    <div className="message-body">
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