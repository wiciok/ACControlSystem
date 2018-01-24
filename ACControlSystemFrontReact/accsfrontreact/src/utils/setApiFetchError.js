let setApiFetchError = function (error) {
    let errorMessage = `${error.message}`;

    if (error.statusCode)
        errorMessage = `Błąd ${error.statusCode}: `.concat(errorMessage).concat('\n');

    if (error.errorMessage)
        errorMessage += "Dodatkowe informacje: " + error.errorMessage;

    this.setState({
        error: {
            isError: true,
            errorMessage: errorMessage
        }
    });
}

export default setApiFetchError;