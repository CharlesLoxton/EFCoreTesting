const connectBtn = document.getElementById("connectBtn");
const companyOptions = document.getElementById("companyOptions");
const inputSection = document.getElementById("inputSection");
const dropDown = document.getElementById("DropDown");
const errorMessage = document.getElementById("errorMessage");
let encodedString;

// Add an event listener to the button
connectBtn.addEventListener("click", function () {
    const username = document.getElementById("username");
    const password = document.getElementById("password");

    if (!username.value || !password.value) {
        return;
    }
    encodedString = btoa(username.value + ":" + password.value);

    fetch(`/api/Integration/GetCompanyList/${encodedString}`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Error occured");
            }
            return response.json();
        })
        .then(data => {
            const results = data.Results;

            // Loop through the results and add options to the select element
            results.forEach(result => {
                const option = document.createElement("option");
                option.value = result.ID;
                option.text = result.Name;
                companyOptions.add(option);
            });

            inputSection.style.display = "none";
            dropDown.style.display = "block";

            connectBtn.innerHTML = "Connect";

            connectBtn.onclick = Connect;
        })
        .catch(error => {
            // Your code to handle the error goes here
            console.log("An error happened");
            errorMessage.style.display = "block";
            username.value = "";
            password.value = "";
        });
});

function Connect() {
    const selectedCompanyId = document.getElementById("companyOptions").value;

    fetch("/api/Integration/SaveCode", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            code: encodedString,
            CompanyId: selectedCompanyId,
            ProviderName: 'Sage',
        })
    })
    .then(response => {
        window.location.href = "/";
    })
    .catch(error => {
         console.error(error);
    });
}