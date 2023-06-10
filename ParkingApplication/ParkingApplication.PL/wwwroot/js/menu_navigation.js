window.addEventListener("click", async event => {
    const btnType = event.target.dataset.btn
    const contentElement = document.querySelector(".menu-opt-content")
    switch (btnType) {
        case "info":
            contentElement.innerHTML = null
            observe(() => {
                // get all info from server here
            }, contentElement, {childList: true}, 10000)

            break
        case "reservation":
            contentElement.innerHTML = null

            break
        case "history":
            contentElement.innerHTML = null
            observe(() => {
                // get all history from server here
            }, contentElement, {childList: true}, 10000)

            break
        case "settings":
            contentElement.innerHTML = null
            const isSuperAdmin = await checkAdmin()
            observe(() => {
                // get all settings from server here
            }, contentElement, {childList: true}, 10000)
            if (isSuperAdmin) {
                contentElement.insertAdjacentHTML("afterbegin", `
                <div class="parking-create-container">
                    <div>
                        <label for="field-parking-name" class="content-input-label">Parking name:</label>
                        <input id="field-parking-name" class="content-input" type="text" placeholder="">
                    </div>
                    <div>
                        <label for="field-floors-count" class="content-input-label">Floors count:</label>
                        <input id="field-floors-count" class="content-input" type="number" min="1" max="40" value="1" step="1">
                    </div>
                    <div>
                        <label for="field-slots-count" class="content-input-label">Slots count:</label>
                        <input id="field-slots-count" class="content-input" type="number" min="4" value="28" step="4">
                    </div>
                    <div class="header-btn card-style" style="width: 100%; height: 30px" onclick="addNewParking()">
                        <span>Add new parking</span>
                    </div>
                </div>
                <!--<select id="select-current-parking">
                    <option selected disabled>Select parking</option>
                </select>-->
                `)
            }
            break
    }
})