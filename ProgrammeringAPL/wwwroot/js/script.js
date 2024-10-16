
// script.js - Hanterar logiken för bildkarusellen
document.addEventListener('DOMContentLoaded', () => {
    const carousel = document.querySelector('.carousel');
    if (!carousel) {
        console.error('Carousel element not found!');
        return;
    }

    let items = Array.from(carousel.querySelectorAll('.carousel-item'));
    const totalItems = items.length;
    const intervalTime = 5000; // Tidsintervall för karusellen

    if (totalItems === 0) {
        console.warn('No carousel items found!');
        return;
    }


    let leftIndex, middleIndex, rightIndex;


    middleIndex = 0;
    updateIndices();

    // Uppdaterar index för de tre positionerna (left, middle, right) i karusellen
    function updateIndices() {
        leftIndex = (middleIndex - 1 + totalItems) % totalItems;
        rightIndex = (middleIndex + 1) % totalItems;
    }

    // Renderar karusellen baserat på aktuella index
    function renderCarousel() {

        items.forEach((item, index) => {
            item.classList.remove('left', 'middle', 'right', 'focused', 'hidden', 'carousel-caption-primary');
            if (index === leftIndex) {
                item.classList.add('left'); // Sätter klassen "left" på föregående objekt
            } else if (index === middleIndex) {
                item.classList.add('middle', 'focused', 'carousel-caption-primary'); // Sätter "middle" för aktuellt objekt
            } else if (index === rightIndex) {
                item.classList.add('right'); // Sätter klassen "right" på nästa objekt
            } else {
                item.classList.add('hidden'); // Gömmer övriga objekt
            } 
        });
    }

  
    renderCarousel();

    // Lägger till klickhändelser för varje karusellobjekt
    items.forEach((item, index) => {
        item.addEventListener('click', () => {
            if (item.classList.contains('middle')) { 

                rotateCarousel(); // Om "middle" klickas, rotera karusellen
            } else if (item.classList.contains('left') || item.classList.contains('right')) {

                middleIndex = index; // Om "left" eller "right" klickas, uppdatera middleIndex, uppdatera alla index och rendera
                updateIndices();
                renderCarousel();
            }
        });
    });


    // Roterar karusellen till nästa objekt
    function rotateCarousel() {
        middleIndex = (middleIndex + 1) % totalItems; 
        updateIndices();
        renderCarousel();
    }

    // Startar automatiskt rotation av karusellen med ett specifikt tidsintervall
    setInterval(rotateCarousel, intervalTime); 


    //function showPrevItem() {
    //    middleIndex = (middleIndex - 1 + totalItems) % totalItems;
    //    updateIndices();
    //    renderCarousel();
    //}

    //function showNextItem() {
    //    rotateCarousel(); 
    //}

});
