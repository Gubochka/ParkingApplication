function changeAuthType(type) {
    const imgElement = document.querySelector(".top-img")
    const infoElement = imgElement.querySelector(".info-content")
    const leftContainer = document.querySelector(".authorization-container .left .content").parentNode
    const rightContainer = document.querySelector(".authorization-container .right .content").parentNode

    imgElement.dataset.authType = type
    infoElement.dataset.authType = type
    leftContainer.dataset.authType = type
    rightContainer.dataset.authType = type
}