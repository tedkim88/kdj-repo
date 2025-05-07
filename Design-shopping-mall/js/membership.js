const toggleButtons = document.querySelectorAll('.toggle-btn');

toggleButtons.forEach(toggleButtons => {
    console.log('debugging')
    toggleButtons.addEventListener('click', function (event) {
        event.stopPropagation();
        this.nextElementSibling.classList.toggle('expanded');
    });
});