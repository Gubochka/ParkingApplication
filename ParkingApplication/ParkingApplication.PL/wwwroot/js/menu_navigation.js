window.addEventListener("click", event => {
    const btnType = event.target.dataset.btn
    switch(btnType) {
        case "info":
            break
        case "reservation":
            break
        case "history":
            break
        case "settings":
            const isSuperAdmin = checkAdmin()
            if(isSuperAdmin) {
                
            }
            break
    } 
})