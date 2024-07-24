
function getPageTitle() {
    return document.title;
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