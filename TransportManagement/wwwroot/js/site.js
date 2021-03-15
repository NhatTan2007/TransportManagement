//Change image avatar when a file is selected
function changeAvatarImage(e) {
    let avatar = document.getElementById("avatar");
    avatar.src = URL.createObjectURL(e.files[0]);
    avatar.style.height = "250px";
    avatar.style.width = "250px";
}
//change pageSize pagination

const defaultPage = 1;
const defaultPageSize = document.getElementById("pageSize") != null ? document.getElementById("pageSize").value : 0;
const domain = window.location.origin;
const path = window.location.pathname;
let pathArr = path.split("/");

function changePageSize(el) {
    if (path == "/") {
        window.location.href = `/home/index/${defaultPage}/${el.value}`;
    } else if (pathArr[2] == null) {
        window.location.href = `${domain}/${pathArr[1]}/Index/${defaultPage}/${el.value}`;
    } else {
        window.location.href = `${domain}/${pathArr[1]}/${pathArr[2]}/${defaultPage}/${el.value}`;
    }
}

//Search function

function searchKeyWord(el) {
    if (path == "/") {
        window.location.href = `/home/index/${defaultPage}/${defaultPageSize}/${el.value}`;
    } else if (pathArr[2] == null) {
        window.location.href = `${domain}/${pathArr[1]}/Index/${defaultPage}/${defaultPageSize}/${el.value}`;
    } else {
        window.location.href = `${domain}/${pathArr[1]}/${pathArr[2]}/${defaultPage}/${defaultPageSize}/${el.value}`;
    }
}

//disable first last button at pagination

const pageLinks = document.getElementsByClassName("page-link");
if (pageLinks.length > 0) {

    const currentLocation = location.href;
    for (var i = 2; i < pageLinks.length - 2; i++) {
        pageLinks[i].parentElement.classList.remove("active");
        if (pageLinks[i].href === currentLocation) {
            pageLinks[i].parentElement.classList.add("active")
        }
    }

    if (currentLocation === pageLinks[0].href || currentLocation === `${window.location.origin}/`) {
        pageLinks[0].parentElement.classList.add("disabled")
        pageLinks[1].parentElement.classList.add("disabled")
    }

    if (currentLocation == pageLinks[pageLinks.length - 1].href) {
        pageLinks[pageLinks.length - 1].parentElement.classList.add("disabled")
        pageLinks[pageLinks.length - 2].parentElement.classList.add("disabled")
    }
}

