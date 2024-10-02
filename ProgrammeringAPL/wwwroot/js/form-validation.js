function validateForm() {

    const name = document.getElementById("name").value.trim();
    const email = document.getElementById("email").value.trim();
    const message = document.getElementById("message").value.trim();


    if (name.length < 2) {
        alert("Ditt namn måste vara minst 2 tecken långt.");
        return false;
    }

 
    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailPattern.test(email)) {
        alert("Ange en giltig e-postadress.");
        return false;
    }

  
    if (message.length < 10) {
        alert("Meddelandet måste vara minst 10 tecken långt.");
        return false;
    }

    
    alert("Formuläret skickades korrekt!");
    return true; 
}
