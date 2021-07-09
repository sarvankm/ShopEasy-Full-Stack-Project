$(document).ready(function () {
    $(".color-choose input").on("click", function () {
        var headphonesColor = $(this).attr("data-image");
        $(".active").removeClass("active");
        var dataimg = $(".left-column img");
        for (var i = 0; i < dataimg.length; i++) {
            if (dataimg[i].getAttribute("data-image") == headphonesColor) {
                dataimg[i].classList.add("active");
            }
        }
        $(this).addClass("active");
    });
});
