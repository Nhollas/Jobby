

function showElement(id) {
    element = document.getElementById(id);
    element.classList.remove("hidden");
}

function hideElement(id) {
    element = document.getElementById(id);
    element.classList.add("hidden");
}

function openTab(event, tabName) {
    var i, tabcontent, tablinks;

    // Update the url with the new tabName.
    updateUrl(tabName);
  
    // Get all elements with class="tabcontent" and hide them.
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].classList.remove("active");
    }
  
    // Get all elements with class="tablink" and hide them.
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].classList.remove("active");
    }
  
    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(tabName).classList.add("active");
    event.currentTarget.classList.add("active");
}

function updateUrl(tabName) {

    var currentUrl = window.location.href;

    var modifiedUrl = currentUrl.split('/').slice(0, -1).join('/') + '/' + tabName;

    window.history.pushState('test', null, modifiedUrl);
}