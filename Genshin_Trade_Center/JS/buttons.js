$(".nice-button").on('click', function () {
    $('.nice-button').prop('disabled', true);
    setTimeout(() => {
        $('.nice-button').prop('disabled', false);
    }, 1000);
});
