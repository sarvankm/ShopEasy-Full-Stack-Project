let skip = 4;
$(document).on('click', '#loadmore', function () {
    //alert("Button Isledi");
    $.ajax({
        url: 'Home/Load?skip=' + skip,
        type: 'GET',
        success: function (res) {
            console.log(res);
            $("#productlist").append(res);
            skip += 4;
            let productcount = $("#productcount").val();
            //console.log(productcount)
            if (skip >= productcount) {
                $("#loadmore").remove();
            }
        }
    });
});
