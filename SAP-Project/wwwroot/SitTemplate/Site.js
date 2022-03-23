

$('.BackHistory').click(() => {
    history.back();
});


$(document).ready(function () {
    $("#elementId, .elementClass").persianDatepicker();

    $('.Select2').select2();
    $('[data-toggle="tooltip"]').tooltip();
    $('th').addClass('text-center');
});


