

document.addEventListener('DOMContentLoaded', () => {
    const carousel = document.querySelector('.carousel');
    if (!carousel) {
        console.error('Carousel element not found!');
        return;
    }

    let items = Array.from(carousel.querySelectorAll('.carousel-item'));
    const totalItems = items.length;
    const intervalTime = 5000;

    if (totalItems === 0) {
        console.warn('No carousel items found!');
        return;
    }


    let leftIndex, middleIndex, rightIndex;


    middleIndex = 0;
    updateIndices();

    function updateIndices() {
        leftIndex = (middleIndex - 1 + totalItems) % totalItems;
        rightIndex = (middleIndex + 1) % totalItems;
    }

    function renderCarousel() {

        items.forEach((item, index) => {
            item.classList.remove('left', 'middle', 'right', 'focused', 'hidden', 'carousel-caption-primary');
            if (index === leftIndex) {
                item.classList.add('left');
            } else if (index === middleIndex) {
                item.classList.add('middle', 'focused', 'carousel-caption-primary');
            } else if (index === rightIndex) {
                item.classList.add('right');
            } else {
                item.classList.add('hidden');
            }
        });
    }

  
    renderCarousel();

   
    items.forEach((item, index) => {
        item.addEventListener('click', () => {
            if (item.classList.contains('middle')) {

                rotateCarousel();
            } else if (item.classList.contains('left') || item.classList.contains('right')) {

                middleIndex = index;
                updateIndices();
                renderCarousel();
            }
        });
    });

    function rotateCarousel() {
        middleIndex = (middleIndex + 1) % totalItems; 
        updateIndices();
        renderCarousel();
    }


    setInterval(rotateCarousel, intervalTime); 


    function showPrevItem() {
        middleIndex = (middleIndex - 1 + totalItems) % totalItems;
        updateIndices();
        renderCarousel();
    }

    function showNextItem() {
        rotateCarousel(); 
    }

});
