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
function sweetalert() {
    Swal.fire("Təbriklər!", "Sifarişiniz uğurla nəticələndi!", "success");
    document.querySelector(".swal2-confirm.swal2-styled").innerHTML = "Bitdi";
}