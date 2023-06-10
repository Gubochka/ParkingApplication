﻿document.addEventListener("readystatechange", () => {
    const token = sessionStorage.getItem("accessToken")
    if(!token) adminLogout()
})

async function getAllParking() {
    if(!(await checkAdmin())) return

    let token = sessionStorage.getItem("accessToken")
    if(!token) {
        adminLogout()
        return
    }
    const response = await fetch("/getAllParking", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        }
    })
    if (response.ok === true) {
        const parkingList = (await response.json()).value
        const parkingListContainer = document.querySelector(".container-content")
        parkingListContainer.innerHTML = null
        parkingList.forEach(parking => {
            parkingListContainer.insertAdjacentHTML("beforeend", `
                <div class="card-style content-row">
                    <div class="disable-selection upload-parking card" data-id="${parking.id}" onclick="generateParkingFloors(${parking.floorsCount}, ${parking.slotsCount})">&#10531;</div>
                    <span>${parking.name}</span>
                    <span>${parking.floorsCount}</span>
                    <span>${parking.slotsCount}</span>
                    <span class="disable-selection delete-x" onclick="deleteParking(${parking.id})">&times;</span>
                </div>
            `)
        })
    }
}

function generateParkingSlots(floor, slotsCount) {
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

function getSlotsByParkingFloor(floor, slotsCount) {
    generateParkingSlots(floor, slotsCount)
}

function selectFloor(selectedElement, floorsContainer, floorsCount, floorsMargin, slotsCount) {
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
    getSlotsByParkingFloor(+selectedElement.dataset.index+1, slotsCount)
}

function generateParkingFloors(floorsCount, slotsCount) {
    const floorsMargin = 70
    
    const floorsContainer = document.querySelector(".parking-floors-container")
    floorsContainer.innerHTML = null
    for(let floor = 0; floor < floorsCount; floor++) {
        const floorElement = document.createElement("div")
        floorElement.classList.add("parking-floor")
        floorElement.style.marginBottom = `${(floor-(floorsCount/2 - (floorsCount%2)))*floorsMargin}px`
        floorElement.dataset.index = floor
        
        floorElement.addEventListener("click", function () {
            selectFloor(this, floorsContainer, floorsCount, floorsMargin, slotsCount)
        });
        
        floorsContainer.insertAdjacentElement("beforeend", floorElement)
    }

    floorsContainer.children[0].classList.add("selected")
    selectFloor(floorsContainer.children[0], floorsContainer, floorsCount, floorsMargin, slotsCount)
}

async function addNewParking() {
    if(!(await checkAdmin())) return 
    
    const token = sessionStorage.getItem("accessToken")
    if(!token) { adminLogout(); return }
    
    const parkingName = document.querySelector("#field-parking-name")
    if(!parkingName.value) return
    
    const response = await fetch("/addNewParking", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            Name: parkingName.value,
            FloorsCount: +document.querySelector("#field-floors-count").value,
            SlotsCount: +document.querySelector("#field-slots-count").value,
        })
    })
    if (response.ok === true) {
        parkingName.value = null
        await getAllParking()
    }
}

async function deleteParking(parkingId) {
    if(!(await checkAdmin())) return

    const token = sessionStorage.getItem("accessToken")
    if(!token) { adminLogout(); return }

    const response = await fetch("/deleteParking", {
        method: "DELETE",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: +parkingId
    })
    if (response.ok === true) {
        await getAllParking()
    }
}