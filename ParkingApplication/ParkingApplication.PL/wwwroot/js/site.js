document.addEventListener("readystatechange", () => {
    const token = getCookie("bearer")
    if(token === "null") {
        document.location.href = "/authorization"
    }
})

function generateParkingSlots(floor, slotsCount=40) {
    const remainder = slotsCount % 4
    if (remainder < 2) slotsCount -= remainder
    else slotsCount += (4 - remainder)
    const slotsForOneRow = slotsCount / 4
    
    const container = document.querySelector(".parking-floor-rows")
    container.innerHTML = null
    
    let currentSlot = 1
    for(let row = 0; row < 2; row++) {
        const rowContainer = document.createElement("div")
        rowContainer.classList.add("parking-floor-row")
        
        for(let underRow = 0; underRow < 2; underRow++) {
            const underRowContainer = document.createElement("div")
            underRowContainer.classList.add("parking-floor-under-row")
            
            for(let slot = 0; slot < slotsForOneRow; slot++) {
                const slotContainer = document.createElement("abbr")
                slotContainer.classList.add("parking-floor-slot")
                
                const g = Math.floor(Math.random() * (2 - 0 + 1) + 0)
                if(g === 1) slotContainer.classList.add("busy")
                
                slotContainer.title = `Floor: ${floor}\nSlot: ${currentSlot}`
                slotContainer.dataset.floor = floor
                slotContainer.dataset.slot = currentSlot
                underRowContainer.insertAdjacentElement("beforeend", slotContainer)
                currentSlot++
            }
            rowContainer.insertAdjacentElement("beforeend", underRowContainer)
        }
        container.insertAdjacentElement("beforeend", rowContainer)
    }
}

function getSlotsByParkingFloor(floor) {
    generateParkingSlots(floor)
}

function selectFloor(selectedElement, floorsContainer, floorsCount, floorsMargin) {
    for (let i = 0; i < floorsContainer.children.length; i++) {
        const resetElement = floorsContainer.children[i]
        resetElement.style.marginBottom = `${(i-(floorsCount/2 - (floorsCount%2)))*floorsMargin}px`
        resetElement.classList.remove("selected")
    }

    const selectedIndex = +selectedElement.dataset.index
    selectedElement.classList.add("selected")
    const slicedElements = [...floorsContainer.children].slice(0, selectedIndex+1)

    slicedElements.forEach(element => {
        let marginBottom = parseInt(element.style.marginBottom)
        marginBottom -= 60
        element.style.marginBottom = `${marginBottom}px`
    })
    getSlotsByParkingFloor(+selectedElement.dataset.index+1)
}

function generateParkingFloors(floorsCount=4) {
    const floorsMargin = 70
    
    const floorsContainer = document.querySelector(".parking-floors-container")
    floorsContainer.innerHTML = null
    for(let floor = 0; floor < floorsCount; floor++) {
        const floorElement = document.createElement("div")
        floorElement.classList.add("parking-floor")
        floorElement.style.marginBottom = `${(floor-(floorsCount/2 - (floorsCount%2)))*floorsMargin}px`
        floorElement.dataset.index = floor
        
        floorElement.addEventListener("click", function () {
            selectFloor(this, floorsContainer, floorsCount, floorsMargin)
        });
        
        floorsContainer.insertAdjacentElement("beforeend", floorElement)
    }

    floorsContainer.children[0].classList.add("selected")
    selectFloor(floorsContainer.children[0], floorsContainer, floorsCount, floorsMargin)
}