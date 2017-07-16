$().ready(function () {
    $('.carousel').carousel({
        interval: 2000
    });

    $(".navbar a").click(function (event) {
        if (this.hash !== "") {
            event.preventDefault();
            var hash = this.hash;
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 900, function () {
                window.location.hash = hash;
            });
        }
    });
    $("#more").click(function () {
        $(this).hide('fast', function () {
            $('#p2').show('fast');
        });
    });
});