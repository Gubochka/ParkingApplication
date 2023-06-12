document.addEventListener("readystatechange", async () => {
    if (document.readyState === "complete") {
        const token = sessionStorage.getItem("accessToken")
        if(!token) adminLogout()
        const isSuperAdmin = await checkAdmin()
        if(isSuperAdmin && !document.querySelector('.menu-btn[data-btn="settings"]')) {
            const btnsContainer = document.querySelector(".menu-opt-btns")
            btnsContainer.insertAdjacentHTML("beforeend", `
                <div class="disable-selection card-style menu-btn" data-btn="settings">
                    <span data-btn="settings">Settings</span>
                </div>
            `)
        } else if(!isSuperAdmin) {
            await getCurrentParkingForAdmin()
        }
        const selectedParking = sessionStorage.getItem("selectedParking")
        if(selectedParking) {
            const parkingData = JSON.parse(selectedParking)
            await generateParkingFloors(parkingData.floorsCount, parkingData.slotsCount, parkingData.parkingId)
        }
    }
})

const checkToken = () => {
    const token = sessionStorage.getItem("accessToken")
    if(!token) { 
        adminLogout();
        return
    }
    return token
}

function generateParkingSlots(floor, slotsCount, parkingId, slotsData) {
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
                
                slotContainer.title = `Floor: ${floor}\nSlot: ${currentSlot}`
                slotContainer.dataset.floor = floor
                slotContainer.dataset.slot = currentSlot
                slotContainer.dataset.parkingId = parkingId

                const slotData = slotsData.find(data=> data.slotNumber === currentSlot);
                let  slotIsBusy = false
                if(slotData) {
                    slotContainer.classList.add("busy")
                    slotIsBusy = true
                }
                
                slotContainer.onclick = async function () {
                    sessionStorage.setItem("selectedSlot", JSON.stringify({
                        floor: +this.dataset.floor,
                        slot: +this.dataset.slot,
                        busy: slotIsBusy
                    }))
                    const selectedBefore = document.querySelector("abbr.parking-floor-slot.selected")
                    if(selectedBefore) selectedBefore.classList.remove("selected")
                    this.classList.add("selected")
                    const tabType = sessionStorage.getItem("selectedTab")
                    if(tabType && tabType === "reservation" && slotIsBusy) {
                        const contentElement = document.querySelector(".menu-opt-content")
                        contentElement.innerHTML = `At the moment, the slot is occupied! To view detailed information, go to the INFO tab.`
                    } else if(tabType) {
                        await selectTab(tabType)
                    }
                }
                
                underRowContainer.insertAdjacentElement("beforeend", slotContainer)
                currentSlot++
            }
            rowContainer.insertAdjacentElement("beforeend", underRowContainer)
        }
        container.insertAdjacentElement("beforeend", rowContainer)
    }
}

async function getSlotsByParkingFloor(floor, slotsCount, parkingId) {

    const token = checkToken()
    const response = await fetch("/getParkingSlotsData", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            parkingId: +parkingId,
            floor: +floor
        })
    })
    if (response.ok === true) {
        const slotsData = (await response.json()).value
        generateParkingSlots(floor, slotsCount, parkingId, slotsData)
    }
}

async function selectFloor(selectedElement, floorsContainer, floorsCount, floorsMargin, slotsCount, parkingId) {
    for (let i = 0; i < floorsContainer.children.length; i++) {
        const resetElement = floorsContainer.children[i]
        resetElement.style.marginBottom = `${(i-(floorsCount/2 - (floorsCount%2)))*floorsMargin}px`
        resetElement.classList.remove("selected")
    }

    const selectedIndex = +selectedElement.dataset.index
    selectedElement.classList.add("selected")
    const slicedElements = [...floorsContainer.children].slice(0, selectedIndex+1)
    
    sessionStorage.setItem("selectedParking", JSON.stringify({
        parkingId: parkingId,
        floorsCount: floorsCount,
        slotsCount: slotsCount
    }))
    
    slicedElements.forEach(element => {
        let marginBottom = parseInt(element.style.marginBottom)
        marginBottom -= 60
        element.style.marginBottom = `${marginBottom}px`
    })
    sessionStorage.removeItem("selectedSlot")
    const tabType = sessionStorage.getItem("selectedTab")
    if(tabType && tabType !== "settings") await selectTab(tabType)
    await getSlotsByParkingFloor(+selectedElement.dataset.index+1, slotsCount, parkingId)
}

async function generateParkingFloors(floorsCount, slotsCount, parkingId) {
    const floorsMargin = 55
    
    const floorsContainer = document.querySelector(".parking-floors-container")
    floorsContainer.innerHTML = null
    for(let floor = 0; floor < floorsCount; floor++) {
        const floorElement = document.createElement("div")
        floorElement.classList.add("parking-floor")
        floorElement.style.marginBottom = `${(floor-(floorsCount/2 - (floorsCount%2)))*floorsMargin}px`
        floorElement.dataset.index = floor
        
        floorElement.addEventListener("click",  async function () {
            await selectFloor(this, floorsContainer, floorsCount, floorsMargin, slotsCount, parkingId)
        });
        
        floorsContainer.insertAdjacentElement("beforeend", floorElement)
    }

    floorsContainer.children[0].classList.add("selected")
    await selectFloor(floorsContainer.children[0], floorsContainer, floorsCount, floorsMargin, slotsCount, parkingId)
}

async function getAllParking($callback=null) {
    if(!(await checkAdmin())) return

    const token = checkToken()
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
        const parkingListContainer = document.querySelector(".container-content.parking")
        if(!parkingListContainer) return
        
        parkingListContainer.innerHTML = null
        parkingList.forEach(parking => {
            parkingListContainer.insertAdjacentHTML("beforeend", `
                <div class="card-style content-row">
                    <div class="disable-selection upload-parking card" data-id="${parking.id}" onclick="generateParkingFloors(${parking.floorsCount}, ${parking.slotsCount}, ${parking.id})">&#10531;</div>
                    <span>${parking.name}</span>
                    <span>${parking.floorsCount}</span>
                    <span>${parking.slotsCount}</span>
                    <span>${parking.price}$</span>
                    <span class="disable-selection delete-x" onclick="deleteParking(${parking.id})">&times;</span>
                </div>
            `)
        })
        if($callback && typeof $callback === "function") $callback(parkingList)
    }
}

async function addNewParking() {
    if(!(await checkAdmin())) return

    const token = checkToken()
    
    const parkingName = document.querySelector("#field-parking-name")
    const parkingPrice = document.querySelector("#field-price")
    if(!parkingName.value.trim() || !parkingPrice) return
    
    const response = await fetch("/addNewParking", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            Name: parkingName.value.trim(),
            FloorsCount: +document.querySelector("#field-floors-count").value,
            SlotsCount: +document.querySelector("#field-slots-count").value,
            Price: +parkingPrice.value,
        })
    })
    if (response.ok === true) {
        parkingName.value = null
        parkingPrice.value = null
        await getAllAdmins()
    }
}

async function deleteParking(parkingId) {
    if(!(await checkAdmin())) return

    const token = checkToken()

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
        await getAllAdmins()
    }
}

async function addNewAdmin() {
    if(!(await checkAdmin())) return

    const token = checkToken()

    const adminEmail = document.querySelector("#field-admin-email")
    const adminPass = document.querySelector("#field-admin-pass")
    if(!adminEmail.value.trim() || !isValidEmail(adminEmail.value.trim()) || !adminPass.value.trim()) return

    const response = await fetch("/addNewAdmin", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            Email: adminEmail.value.trim(),
            Password: adminPass.value.trim(),
            IsSuperAdmin: false,
        })
    })
    if (response.ok === true) {
        adminEmail.value = null
        adminPass.value = null
        await getAllAdmins()
    }
}

async function getAllAdmins() {
    if(!(await checkAdmin())) return

    const token = checkToken()
    const response = await fetch("/getAllAdmins", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        }
    })
    if (response.ok === true) {
        const adminsList = (await response.json()).value
        const adminsListContainer = document.querySelector(".container-content.admin")
        if(!adminsListContainer) return
        
        adminsListContainer.innerHTML = null
        adminsList.forEach(admin => {
            adminsListContainer.insertAdjacentHTML("beforeend", `
                <div class="card-style content-row">
                    <span contenteditable="true">${admin.email}</span>
                    <span contenteditable="true">${admin.password}</span>
                    <span>
                        <select onchange="addParkingToAdmin(${admin.id}, '${admin.email}', '${admin.password}', +this.value)" data-parking-id="${admin.parkingTemplateId}" class="admin-parking-select content-input"></select>
                    </span>
                    <span class="disable-selection delete-x" onclick="deleteAdmin(${admin.id})">&times;</span>
                </div>
            `)
        })
        await getAllParking((list) => {
            const rows = adminsListContainer.querySelectorAll("select.admin-parking-select[data-parking-id]")
            if(!rows) return
            
            rows.forEach(row => {
                row.innerHTML = `<option ${row.dataset.parkingId === "null" ? "selected" : ""} disabled>Select parking</option>`
                list.forEach(value => {
                    row.insertAdjacentHTML("beforeend", `
                        <option value="${value.id}" ${value.id == row.dataset.parkingId ? "selected" : ""}>${value.name}</option>
                    `)
                })
            })
        })
    }
}

async function deleteAdmin(adminId) {
    if (!(await checkAdmin())) return

    const token = checkToken()
    const response = await fetch("/deleteAdmin", {
        method: "DELETE",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: +adminId
    })
    if (response.ok === true) {
        await getAllAdmins()
    }
}

async function addParkingToAdmin(adminId, adminEmail, adminPass, parkingId) {
    if(!(await checkAdmin())) return

    const token = checkToken()
    const response = await fetch("/addParkingToAdmin", {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            Id: adminId,
            Email: adminEmail,
            Password: adminPass,
            ParkingTemplateId: parkingId,
            IsSuperAdmin: false
        })
    })
    if (response.ok === true) {
        await getAllAdmins()
    }
}

async function getPriceByDateTime(datetime, parkingId) {
    const token = checkToken()
    const response = await fetch("/getPriceByDateTime", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            ParkingId: +parkingId,
            StandsUntil: datetime
        })
    })
    if (response.ok === true) {
        document.querySelector("#field-reserv-price").innerHTML = (await response.json()).value.price.result
    }
}

async function getAllOwnersNames() {
    const token = checkToken()
    const response = await fetch("/getAllOwnersNames", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        }
    })
    if (response.ok === true) {
        const dataList = document.querySelector("#owners-names")
        if(!dataList) return
        dataList.innerHTML = null
        const responseData = (await response.json()).value

        responseData.forEach(data => dataList.insertAdjacentHTML("beforeend", `<option value="${data.fullName}">`))
    }
}

async function reservationCarToParking() {
    const token = checkToken()
    const selectedParking = sessionStorage.getItem("selectedParking")
    const selectedSlot = sessionStorage.getItem("selectedSlot")
    if(!selectedParking || !selectedSlot) return
    
    const parkingData = JSON.parse(selectedParking)
    const slotData = JSON.parse(selectedSlot)
    const ownerFullName = document.querySelector("#field-owner-fullname")
    const phoneNumber = document.querySelector("#field-owner-pnumber")
    const carName = document.querySelector("#field-car-name")
    const carNumber = document.querySelector("#field-car-number")
    const standsUntil = document.querySelector("#field-stands-until")
    const price = document.querySelector("#field-reserv-price")
    if(!ownerFullName.value.trim() || !phoneNumber.value.trim()
        || !carName.value.trim() || !carNumber.value.trim()
        || !standsUntil.value) return
    
    const response = await fetch("/reservationCarToParking", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            ownerData: {
                fullName: ownerFullName.value.trim(),
                phoneNumber: phoneNumber.value.trim(),
            },
            carData: {
                carName: carName.value.trim(),
                carNumber: carNumber.value.trim(),
            },
            parkingData: {
                parkingTemplateId: parkingData.parkingId,
                floorNumber: slotData.floor,
                slotNumber: slotData.slot,
                standsUntil: standsUntil.value,
                price: +price.innerHTML
            },
        })
    })
    if (response.ok === true) {
        ownerFullName.value = null
        phoneNumber.value = null
        carName.value = null
        carNumber.value = null
        await getAllOwnersNames()
        await getSlotsByParkingFloor(slotData.floor, parkingData.slotsCount, parkingData.parkingId)
    }
}

async function getCurrentParkingForAdmin() {
    const token = checkToken()
    const response = await fetch("/getCurrentParkingForAdmin", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        }
    })
    if (response.ok === true) {
        const responseData = (await response.json()).value
        responseData.parkingId = responseData.id
        delete responseData.id
        if(responseData) sessionStorage.setItem("selectedParking", JSON.stringify(responseData))
    }
}