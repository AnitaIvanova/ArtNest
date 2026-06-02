document.addEventListener('DOMContentLoaded', function () {

    // Auto-hide success messages after 5 seconds
    document.querySelectorAll('.message-success, .flash-success').forEach(function (box) {
        setTimeout(function () {
            box.style.transition = 'opacity 0.6s';
            box.style.opacity = '0';
            setTimeout(function () { box.style.display = 'none'; }, 600);
        }, 5000);
    });

    // Mobile nav toggle — add hamburger if needed on small screens
    const nav = document.querySelector('.site-nav');
    const navLinks = document.querySelector('.nav-links');
    if (nav && navLinks) {
        // Collapse nav gracefully (CSS handles most of this via media query)
    }

    // Confirm on all delete/remove forms that don't have onsubmit already
    document.querySelectorAll('form[data-confirm]').forEach(function (form) {
        form.addEventListener('submit', function (e) {
            if (!confirm(form.dataset.confirm)) {
                e.preventDefault();
            }
        });
    });

});
