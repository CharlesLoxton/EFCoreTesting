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
                console.log(response);
            })
            .catch(error => {
                console.error(error);
            });
    }
}