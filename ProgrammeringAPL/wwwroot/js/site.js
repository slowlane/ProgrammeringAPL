// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




// site.js - Hanterar temaomkopplaren för läge mellan dag och natt

if (localStorage.getItem('theme') === 'dark') {
    document.getElementById('dark-mode-style').disabled = false;
    document.getElementById('theme-toggle').innerHTML = '<span class="iconify" data-icon="mdi:weather-night"></span>';
} else {
    document.getElementById('dark-mode-style').disabled = true;
    document.getElementById('theme-toggle').innerHTML = '<span class="iconify" data-icon="mdi:weather-sunny"></span>';
}

// Lägg till händelsehanterare för att byta mellan ljus- och mörkt läge, sparar användarens val i localstorage
document.getElementById('theme-toggle').addEventListener('click', function () {
    const darkModeStyle = document.getElementById('dark-mode-style');
    if (darkModeStyle.disabled) {
        darkModeStyle.disabled = false;
        localStorage.setItem('theme', 'dark');
        this.innerHTML = '<span class="iconify" data-icon="mdi:weather-night"></span>';
    } else {
        darkModeStyle.disabled = true;
        localStorage.setItem('theme', 'light');
        this.innerHTML = '<span class="iconify" data-icon="mdi:weather-sunny"></span>';
    }
});
