import Cookies from 'js-cookie';

 let sendAuth = function (refreshCallback, logoutCallback) {
    let endpointAddress = `${window.apiAddress}/auth`;
    let authObj = {
        EmailAddress: Cookies.get('userEmail') || null,
        PasswordHash: Cookies.get('userPasswordHash') || ""
    };

    let fetchObj = {
        method: 'post',
        body: JSON.stringify(authObj),
        headers: new Headers({ "Content-Type": "application/json" })
    };

    fetch(endpointAddress, fetchObj).then(response => {
        if (response.status === 200) {
            response.json().then(token => {
                Cookies.set('token', token);
                if(refreshCallback)
                    refreshCallback();
            }) 
        }
        else if (response.status === 401) {
            logoutCallback();
            console.log("logging out - server returned unauthorized")
        }
    })
}

export default sendAuth;