let skip = 4;
$(document).on('click', '#loadmore', function () {
    $.ajax({
        url: 'Home/Load?skip=' + skip,
        type: 'GET',
        success: function (res) {
            $("#productlist").append(res);
            skip += 4;
            let productcount = $("#productcount").val();
            if (skip >= productcount) {
                $("#loadmore").remove();
            }
        }
    });
});
let skip2 = 9;
$(document).on('click', '#loadmoreCategory', function () {
    $.ajax({
        url: 'http://localhost:14381/Category/Load?skip=' + skip2,
        type: 'GET',
        success: function (res) {
            $("#productlist2").append(res);
            skip2 += 6;
            let productcount = $("#productcount").val();
            if (skip2 >= productcount) {
                $("#loadmoreCategory").remove();
            }
        }
    });
});
let skip3 = 8;
$(document).on('click', '#loadmoreFavorite', function () {
    $.ajax({
        url: 'http://localhost:14381/Home/LoadForFavorite?skip=' + skip3,
        type: 'GET',
        success: function (res) {
            $("#productlist3").append(res);
            skip3 += 4;
            let productcount = $("#productcount").val();
            if (skip3 >= productcount) {
                $("#loadmoreFavorite").remove();
            }
        }
    });
});