$('input.number').keyup(function () {
    if (
        ($(this).val().length > 0) && ($(this).val().substr(0, 4) != '+994')
        || ($(this).val() == '')
    ) {
        $(this).val('+994');
    }
});