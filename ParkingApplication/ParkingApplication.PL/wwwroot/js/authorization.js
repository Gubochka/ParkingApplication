async function adminLogin() {
    const email = document.querySelector("#form-email-field")
    const password = document.querySelector("#form-pass-field")
    
    const response = await fetch("/login", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Email: email.value,
            Password: password.value
        })
    })
    if (response.ok === true) {
        const token = (await response.json()).value.bearer
        if(token !== null) {
            document.cookie = `bearer=${token}`
            document.location.href = "/"
        }
        else {
            email.style.borderColor = "var(--error-color)"
            password.style.borderColor = "var(--error-color)"
            document.querySelector('label[for="form-email-field"]').style.display = "block"
            document.querySelector('label[for="form-pass-field"]').style.display = "block"
        }
    }
    else {
        const error = await response.json()
        console.log(error.message)
    }
}