const isValidEmail = email => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)
const setErrorField = (field, label, isError=true) => {
    if(isError) {
        field.style.borderColor = "var(--error-color)"
        label.style.display = "block"
    } 
    else {
        field.style.borderColor = "rgba(0,0,0,0)"
        label.style.display = "none"
    }
}
async function adminLogin() {
    const emailField = document.querySelector("#form-email-field")
    const passwordField = document.querySelector("#form-pass-field")
    
    if(isValidEmail(emailField.value.trim())) {
        const response = await fetch("/login", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                Email: emailField.value.trim(),
                Password: passwordField.value.trim()
            })
        })
        if (response.ok === true) {
            const token = (await response.json()).value.bearer
            switch (token) {
                case "EMAIL":
                    setErrorField(emailField, document.querySelector('label[for="form-email-field"]'), true)
                    setErrorField(passwordField, document.querySelector('label[for="form-pass-field"]'), false)
                    break
                case "PASSWORD":
                    setErrorField(emailField, document.querySelector('label[for="form-email-field"]'), false)
                    setErrorField(passwordField, document.querySelector('label[for="form-pass-field"]'), true)
                    break
                default:
                    setErrorField(emailField, document.querySelector('label[for="form-email-field"]'), false)
                    setErrorField(passwordField, document.querySelector('label[for="form-pass-field"]'), false)
                    document.cookie = `bearer=${token}`
                    setTimeout(() => document.location.href = "/", 1000)
                    break
            }
        }
        else {
            const error = await response.json()
            console.log(error.message)
        }
    }
    else {
        setErrorField(emailField, document.querySelector('label[for="form-email-field"]'), true)
    }
}