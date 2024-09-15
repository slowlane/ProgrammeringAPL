// script.js

document.addEventListener('DOMContentLoaded', () => {
    const carousel = document.querySelector('.carousel');
    let items = Array.from(carousel.querySelectorAll('.carousel-item'));
    const totalItems = items.length;

    // Indices for left, middle, and right items
    let leftIndex, middleIndex, rightIndex;

    // Initialize indices
    middleIndex = 0;
    updateIndices();

    function updateIndices() {
        leftIndex = (middleIndex - 1 + totalItems) % totalItems;
        rightIndex = (middleIndex + 1) % totalItems;
    }

    function renderCarousel() {
        // Clear existing classes
        items.forEach((item, index) => {
            item.classList.remove('left', 'middle', 'right', 'focused');
            if (index === leftIndex) {
                item.classList.add('left');
            } else if (index === middleIndex) {
                item.classList.add('middle', 'focused');
            } else if (index === rightIndex) {
                item.classList.add('right');
            } else {
                item.classList.add('hidden');
            }
        });
    }

    // Initial render
    renderCarousel();

    // Event listeners
    items.forEach((item, index) => {
        item.addEventListener('click', () => {
            if (item.classList.contains('middle')) {
                // Rotate items
                middleIndex = (middleIndex - 1 + totalItems) % totalItems;
                updateIndices();
                renderCarousel();
            } else if (item.classList.contains('left') || item.classList.contains('right')) {
                // Make the clicked item the middle item
                middleIndex = index;
                updateIndices();
                renderCarousel();
            }
        });
    });
});
