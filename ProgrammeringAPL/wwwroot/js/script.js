//// script.js

//document.addEventListener('DOMContentLoaded', () => {
//    const carousel = document.querySelector('.carousel');
//    let items = Array.from(carousel.querySelectorAll('.carousel-item'));
//    const totalItems = items.length;
//    const intervalTime = 5000;

//    // Indices for left, middle, and right items
//    let leftIndex, middleIndex, rightIndex;

//    // Initialize indices
//    middleIndex = 0;
//    updateIndices();

//    function updateIndices() {
//        leftIndex = (middleIndex - 1 + totalItems) % totalItems;
//        rightIndex = (middleIndex + 1) % totalItems;
//    }

//    function renderCarousel() {
//        // Clear existing classes
//        items.forEach((item, index) => {
//            item.classList.remove('left', 'middle', 'right', 'focused');
//            if (index === leftIndex) {
//                item.classList.add('left');
//            } else if (index === middleIndex) {
//                item.classList.add('middle', 'focused');
//            } else if (index === rightIndex) {
//                item.classList.add('right');
//            } else {
//                item.classList.add('hidden');
//            }
//        });
//    }

//    // Initial render
//    renderCarousel();

//    // Event listeners
//    items.forEach((item, index) => {
//        item.addEventListener('click', () => {
//            if (item.classList.contains('middle')) {
//                // Rotate items
//                rotateCarousel();
//                //middleIndex = (middleIndex - 1 + totalItems) % totalItems;
//                //updateIndices();
//                //renderCarousel();
//            } else if (item.classList.contains('left') || item.classList.contains('right')) {
//                // Make the clicked item the middle item
//                middleIndex = index;
//                updateIndices();
//                renderCarousel();
//            }
//        });
//    });

//    function rotateCarousel() {
//        middleIndex = (middleIndex - 1 + totalItems) % totalItems;
//        updateIndices();
//        renderCarousel();
//    }

//    setInterval(rotateCarousel, intervalTime);

//    const prevButton = document.createElement('button');
//    prevButton.innerText = 'Prev';
//    prevButton.addEventListener('click', showPrevItem);
//    document.querySelector('.carousel-container').appendChild(prevButton);

//    const nextButton = document.createElement('button');
//    nextButton.innerText = 'Next';
//    nextButton.addEventListener('click', showNextItem);
//    document.querySelector('.carousel-container').appendChild(nextButton);
//});
// script.js

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
                // Rotate items
                rotateCarousel();
            } else if (item.classList.contains('left') || item.classList.contains('right')) {
                // Make the clicked item the middle item
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
