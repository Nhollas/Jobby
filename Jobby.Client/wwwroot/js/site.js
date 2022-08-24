

function showElement(id) {
    element = document.getElementById(`${id}`);
    element.classList.remove("hidden");
}

function hideElement(id) {
    element = document.getElementById(`${id}`);
    element.classList.add("hidden");
}