window.onload = function () {

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
                alert("Successfully connected to " + providerName + "!");
            })
            .catch(error => {
                alert(error);
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
            alert("Successfully disconnected!");
        })
        .catch(error => {
            alert(error);
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
            alert("Successfully refreshed the token!");
        })
        .catch(error => {
            alert(error);
        });

}