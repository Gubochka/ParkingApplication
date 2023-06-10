window.addEventListener("click", async event => {
    const btnType = event.target.dataset.btn
    const contentElement = document.querySelector(".menu-opt-content")
    if (contentElement) {
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
                observe(async () => {
                    await getAllParking()
                    await getAllAdmins()
                }, contentElement, {childList: true}, 10000)

                if (isSuperAdmin) {
                    contentElement.insertAdjacentHTML("afterbegin", `
                <div style="display: grid; grid-template-columns: repeat(2, 50%); height: 100%; width: 100%">
                    <div class="data-container">
                        <div class="field-container">
                            <label for="field-parking-name" class="content-input-label">Parking name:</label>
                            <input id="field-parking-name" class="content-input" type="text" placeholder="">
                        </div>
                        <div class="field-container">
                            <label for="field-floors-count" class="content-input-label">Floors count:</label>
                            <input id="field-floors-count" class="content-input" type="number" min="1" max="40" value="1" step="1">
                        </div>
                        <div class="field-container">
                            <label for="field-slots-count" class="content-input-label">Slots count:</label>
                            <input id="field-slots-count" class="content-input" type="number" min="4" value="28" step="4">
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
                            </div>
                            <div class="container-content parking"></div>
                        </div>
                    </div>
                    <div class="data-container">
                        <div class="field-container">
                            <label for="field-admin-email" class="content-input-label">Admin email:</label>
                            <input id="field-admin-email" class="content-input" type="text">
                        </div>
                        <div class="field-container">
                            <label for="field-admin-pass" class="content-input-label">Admin password:</label>
                            <input id="field-admin-pass" class="content-input" type="text">
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
})