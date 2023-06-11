const isValidEmail = email => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)
const getTodayDate = () => new Date().toISOString().split("T")[0]
const observe = ($callback, target, config, timeout = 1000) => {
    const observer = new MutationObserver($callback)
    observer.observe(target, config)
    setTimeout(() => {
        observer.disconnect()
    }, timeout)
}

function adminLogout() {
    sessionStorage.removeItem("accessToken")
    document.location.href = "/authorization"
}

async function checkAdmin() {
    let token = sessionStorage.getItem("accessToken")
    if(!token) {
        adminLogout()
        return false
    }
    const response = await fetch("/checkAdmin", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },

    })
    if (response.ok === true) {
        const responseObject = await response.json()
        return responseObject.value.superAdmin
    }
    return false
}
