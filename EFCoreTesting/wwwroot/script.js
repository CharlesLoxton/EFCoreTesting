﻿window.onload = function () {

    const urlParams = new URLSearchParams(window.location.search);
    const code = urlParams.get('code');
    const realmId = urlParams.get('realmId');
    let providerName = "Xero";

    if (code != null) {

        if (realmId != null) {
            providerName = "QuickBooks";
        }
        
        fetch("/api/Integration/SaveCode", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                code: code,
                CompanyId: realmId,
                ProviderName: providerName,
            })
        })
            .then(response => {
                if (response.status === 500) {
                    alert("The server encountered an unexpected condition that prevented it from fulfilling the request");
                } else if (response.status === 400) {
                    alert("Unauthorized access. Please make sure you enter the correct email and password when authenticating with your accounting pacakge");
                } else {
                    alert("Succesfully connected to " + providerName);
                } 
            })
            .catch(error => {
                console.log(error)
            });
    }
}

function disconnect() {

    fetch("/api/Integration/Disconnect", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
            }
        })
            .then(response => {
            if (response.status === 500 || response.status === 400) {
                alert("You must be connected to an accounting package to disconnect");
            } else {
                alert("Successfully disconnected");;
            }  
            })
            .catch(error => {
            console.log(error)
            }); 
}

function refreshToken() {

    fetch("/api/Integration/RefreshToken", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (response.status === 500 || response.status === 400) {
                alert("You must be connected to an accounting package to refresh a token");
            } else {
                alert("Successfully refreshed token");
            }
        })
        .catch(error => {
           console.log(error)
        });

}