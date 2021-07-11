$(document).on("keyup", "#searchInput", function (e) {
    let search = $(this).val().trim();
        if (search.length > 0) {
            $.ajax({
                url: 'Home/Search?search=' + search,
                type: 'Get',
                success: function (res) {
                    if ($(".result ul li").length == 10 || e.key == "Backspace" || e.key == " ") {
                        $(".result ul li").remove();
                        $(".result ul").append(res);
                    }
                    else {
                        $(".result ul li").remove();
                        $(".result ul").append(res);
                    }
                }
            })
    }
    if (search.length == 0) {

        $(".result ul li").remove();

    }
    
 
})