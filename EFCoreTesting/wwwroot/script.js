window.onload = function () {

    const urlParams = new URLSearchParams(window.location.search);
    const code = urlParams.get('code');
    const realmId = urlParams.get('realmId');

    fetch("/api/Integration/SaveCode", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            code: code,
            CompanyId: realmId
        })
    })
    .then(response => {
        console.log(response);
    })
    .catch(error => {
        console.error(error);
    });
}