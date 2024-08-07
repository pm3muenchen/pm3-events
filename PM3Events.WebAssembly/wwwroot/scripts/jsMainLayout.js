
function getPageTitle() {
    const titleElements = document.head ? document.head.getElementsByTagName('title') : [];

    if (titleElements.length === 0) {
        return null;
    }

    return titleElements[titleElements.length - 1].textContent;
}

function setMainContainerClass(expanded) {
    let mainEl = document.getElementById('main-body');
    if (mainEl) {
        if (expanded) {
            mainEl.classList.remove('main-full-container');
            mainEl.classList.add('main-container');
        } else {
            mainEl.classList.remove('main-container');
            mainEl.classList.add('main-full-container');
        }
    }
}