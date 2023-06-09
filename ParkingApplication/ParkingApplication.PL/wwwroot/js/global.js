function adminLogout() {
    sessionStorage.removeItem("accessToken")
    document.location.href = "/authorization"
}

async function checkAdmin() {
    let token = sessionStorage.getItem("accessToken")
    console.log(token)
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
    else {
        return false
    }
}