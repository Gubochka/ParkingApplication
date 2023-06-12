window.addEventListener("click", async event => {
    const tabType = event.target.dataset.btn
    await selectTab(tabType)
})

async function selectTab(tabType) {
    const contentElement = document.querySelector(".menu-opt-content")
    const targetsList = ["info", "reservation", "history", "settings"]
    const selectedSlot = sessionStorage.getItem("selectedSlot")
    const selectedParking = sessionStorage.getItem("selectedParking")
    let parkingData;
    if(targetsList.includes(tabType)) {
        contentElement.innerHTML = null
        let callback = () => null
        sessionStorage.setItem("selectedTab", tabType)
        switch (tabType) {
            case "info":
                 callback = async () => {
                     if(!selectedSlot || !selectedParking) return
                     const slotData = JSON.parse(selectedSlot)
                     parkingData = JSON.parse(selectedParking)
                     if(slotData.busy)
                         await getSlotData(slotData.floor, slotData.slot, parkingData.parkingId)
                 }
                break
            case "reservation":
                callback = getAllOwnersNames
                break
            case "history":
                callback = async () => {
                    const selectedFloor = document.querySelector("div.parking-floor.selected")
                    if(!selectedFloor || !selectedParking) return
                    parkingData = JSON.parse(selectedParking)
                    await getHistoryForFloor(selectedFloor.dataset.index+1, parkingData.parkingId)
                }
                break
            case "settings":
                callback = async () => {
                    await getAllParking()
                    await getAllAdmins()
                }
                break
        }
        
        observe(() => {
            callback()
        }, contentElement, {childList: true}, 5000)
    }
    if (contentElement) {
        switch (tabType) {
            case "info":
                if(!selectedSlot) {
                    contentElement.innerHTML = `You have not selected any slot!`
                    return
                }
                if(!JSON.parse(selectedSlot).busy) {
                    contentElement.innerHTML = `At the moment, there is no information about this slot! It's not busy!`
                    return
                }
                contentElement.insertAdjacentHTML("afterbegin", `
                    <div class="data-container none-border info" style="width: 100%;">
                        <div class="info header">
                            <div class="field-container">
                                <label class="content-input-label">Owner: <span id="info-owner-name" class="info-span"></span></label>
                            </div>
                            <div class="field-container">
                                <label class="content-input-label">Phone number: <span id="info-owner-pnumber" class="info-span"></span></label>
                            </div>
                            <div class="field-container">
                                <label class="content-input-label">Car name: <span id="info-car-name" class="info-span"></span></label>
                            </div>
                            <div class="field-container">
                                <label class="content-input-label">Car number: <span id="info-car-number" class="info-span"></span></label>
                            </div>
                        </div>
                        <div class="info footer"> 
                            <div class="field-container">
                                <label class="content-input-label">Floor: <span id="info-floor" class="info-span"></span></label>
                                &nbsp;&nbsp;&nbsp;
                                <label class="content-input-label">Slot: <span id="info-slot" class="info-span"></span></label>
                            </div>
                            <div class="field-container">
                                <label class="content-input-label">Stands Until: <span id="info-stands-until" class="info-span"></span></label>
                            </div>
                            <div class="field-container">
                                <label class="content-input-label">Price: <span id="info-price" class="info-span"></span></label>
                            </div>
                        </div>
                    </div>
                `)
                break
            case "reservation":
                if(!selectedSlot) {
                    contentElement.innerHTML = `You have not selected any slot!`
                    return
                }
                if(JSON.parse(selectedSlot).busy) {
                    contentElement.innerHTML = `At the moment, the slot is busy! To view detailed information, go to the INFO tab.`
                    return
                }

                contentElement.insertAdjacentHTML("afterbegin", `
                    <div class="data-container none-border" style="width: 50%">
                        <div class="field-container">
                            <label for="field-owner-fullname" class="content-input-label">Owner fullname:</label>
                            <input list="owners-names" id="field-owner-fullname" class="content-input" type="text" placeholder="John Doe">
                            <datalist id="owners-names">
                              <option value="Alice">
                              <option value="Bob">
                              <option value="Charlie">
                            </datalist>
                        </div>
                        <div class="field-container">
                            <label for="field-owner-pnumber" class="content-input-label">Phone number:</label>
                             <input id="field-owner-pnumber" class="content-input" type="text" placeholder="380000000000">
                        </div>
                        <div class="field-container">
                            <label for="field-car-name" class="content-input-label">Car name:</label>
                             <input id="field-car-name" class="content-input" type="text" placeholder="Tesla Model X">
                        </div>
                        <div class="field-container">
                            <label for="field-car-number" class="content-input-label">Car number:</label>
                             <input id="field-car-number" class="content-input" type="text" placeholder="XX 0000 XX">
                        </div>
                        <div class="field-container">
                            <label for="field-stands-until" class="content-input-label">Stands until:</label>
                             <input id="field-stands-until" class="content-input" type="datetime-local" value="${getTodayDateTime()}"
                              onchange="getPriceByDateTime(this.value, JSON.parse(sessionStorage.getItem('selectedParking')).parkingId)">
                        </div>
                        <label>Price: <span id="field-reserv-price" style="font-style: italic">0</span> $</label>
                        <div class="header-btn card-style" style="width: 100%; height: 30px" onclick="reservationCarToParking()">
                            <span>Reservation car</span>
                        </div>
                    </div>
                `)
                break
            case "history":
                const selectedParking = sessionStorage.getItem("selectedParking")
                if(!selectedParking) {
                    contentElement.innerHTML = `You have not selected a parking!`
                    return
                }
                contentElement.insertAdjacentHTML("afterbegin", `
                    <div class="data-container none-border" style="width: 100%;">
                        <div class="list-container" style="margin-top: 0">
                            <div class="card-style container-header history">
                                <span>Car name</span>
                                <span>Car number</span>
                                <span>Floor</span>
                                <span>Slot</span>
                                <span>Price</span>
                                <span>Stands Until</span>
                            </div>
                            <div class="container-content history"></div>
                        </div>
                    </div>
                `)
                break
            case "settings":
                const isSuperAdmin = await checkAdmin()
                if (isSuperAdmin) {
                    contentElement.insertAdjacentHTML("afterbegin", `
                        <div style="display: grid; grid-template-columns: repeat(2, 50%); height: 100%; width: 100%">
                            <div class="data-container">
                                <div class="field-container">
                                    <label for="field-parking-name" class="content-input-label">Parking name:</label>
                                    <input id="field-parking-name" class="content-input" type="text" placeholder="My Parking">
                                </div>
                                <div class="field-container">
                                    <label for="field-floors-count" class="content-input-label">Floors count:</label>
                                    <input id="field-floors-count" class="content-input" type="number" min="1" max="40" value="1" step="1">
                                </div>
                                <div class="field-container">
                                    <label for="field-slots-count" class="content-input-label">Slots count:</label>
                                    <input id="field-slots-count" class="content-input" type="number" min="4" value="28" step="4">
                                </div>
                                <div class="field-container">
                                    <label for="field-price" class="content-input-label">Price per hour:</label>
                                    <input id="field-price" class="content-input" type="number" min="0" placeholder="$">
                                </div>
                                <div class="header-btn card-style" style="width: 100%; height: 38px" onclick="addNewParking()">
                                    <span>Add new parking</span>
                                </div>
                                <div class="list-container">
                                    <div class="card-style container-header parking">
                                        <span></span>
                                        <span>Parking name</span>
                                        <span>Floors</span>
                                        <span>Slots</span>
                                        <span>Price</span>
                                    </div>
                                    <div class="container-content parking"></div>
                                </div>
                            </div>
                            <div class="data-container none-border">
                                <div class="field-container">
                                    <label for="field-admin-email" class="content-input-label">Admin email:</label>
                                    <input id="field-admin-email" class="content-input" type="text" placeholder="secret@gmail.com">
                                </div>
                                <div class="field-container">
                                    <label for="field-admin-pass" class="content-input-label">Admin password:</label>
                                    <input id="field-admin-pass" class="content-input" type="text" placeholder="supeRsecreTpassw0rD">
                                </div>
                                <div class="header-btn card-style" style="width: 100%; height: 38px" onclick="addNewAdmin()">
                                    <span>Add new admin</span>
                                </div>
                                <div class="list-container">
                                    <div class="card-style container-header admin">
                                        <span>Admin email</span>
                                        <span>Password</span>
                                        <span>Parking</span>
                                    </div>
                                    <div class="container-content admin"></div>
                                </div>
                            </div>
                        </div>
                    `)
                }
                break
        }
    }
}