
// form-validation.js - Hanterar validering av kontaktformuläret
function validateForm() {

    const name = document.getElementById("name").value.trim();
    const email = document.getElementById("email").value.trim();
    const message = document.getElementById("message").value.trim();

    // Validerar att namnet är minst 2 tecken långt
    if (name.length < 2) {
        alert("Ditt namn måste vara minst 2 tecken långt.");
        return false;
    }

    // Validerar att e-postadressen har en korrekt struktur
    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailPattern.test(email)) {
        alert("Ange en giltig e-postadress.");
        return false;
    }

    // Validerar att meddelandet är minst 10 tecken långt
    if (message.length < 10) {
        alert("Meddelandet måste vara minst 10 tecken långt.");
        return false;
    }

    
    alert("Formuläret skickades korrekt!");
    return true; 
}
