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

function generateParkingFloors(floorsCount=4) {
    const floorsContainer = document.querySelector(".parking-floors-container")
    floorsContainer.innerHTML = null
    for(let floor = 0; floor < floorsCount; floor++) {
        const floorElement = document.createElement("div")
        floorElement.classList.add("parking-floor")
        floorElement.style.marginBottom = `${(floor-(floorsCount/2 - (floorsCount%2)))*60}px`
        floorElement.dataset.index = floor
        
        floorElement.addEventListener("click", function() {
            const selectedIndex = this.dataset.index //[...floorsContainer.children].indexOf(this.dataset.index)
            console.log(selectedIndex)
            
            if (selectedIndex !== -1) {
                const slicedElements = [...floorsContainer.children].slice(0, selectedIndex + 1);
                console.log(slicedElements)
                // for (let lowerFloor = selectedIndex; lowerFloor < floorsCount; lowerFloor++) {
                //     const lowerFloorElement = floorsContainer.children[lowerFloor] // [...floorsContainer.children].reverse()[lowerFloor];
                //    
                //     let marginBottom = parseInt(lowerFloorElement.style.marginBottom)
                //     marginBottom -= 60
                //     lowerFloorElement.style.marginBottom = `${marginBottom}px`
                // }
            }
        });
        
        floorsContainer.insertAdjacentElement("beforeend", floorElement)
    }
}