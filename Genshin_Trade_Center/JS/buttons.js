$(".nice-button").on('click', function() {
    setTimeout(function()
    {
        $('.nice-button').prop('disabled', true);
    }, 0)
    
    setTimeout(() => {
        $('.nice-button').prop('disabled', false);
    }, 1000);
});
