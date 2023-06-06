window.addEventListener("click", event => {
    const btnType = event.target.dataset.btn
    switch(btnType) {
        case "info":
            console.log("click!")
            break
        case "reservation":
            console.log("click!")
            break
        case "history":
            console.log("click!")
            break
        case "settings":
            console.log("click!")
            break
    } 
})