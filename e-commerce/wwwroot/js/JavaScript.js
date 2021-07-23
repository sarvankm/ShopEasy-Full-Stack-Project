$(".input1").mouseup(function () {
    var value = this.value;
    var value2 = this.nextElementSibling.value;
    var id = document.querySelector(".hiddeninput").value;
    var childid = document.querySelector(".hiddeninput2").value;
    var brendid = document.querySelector(".hiddeninput3").value;


    var hostname = "http://localhost:14381/Category/Index/" + id;
    var hostname2 = "http://localhost:14381/Category/Index/" + id + "?childid=" + childid;
    var hostname3 = "http://localhost:14381/Category/Index/" + id + "?brendid=" + brendid + "&childid=" + childid;

    var minvalue = "?minvalue=" + value;
    var minvalue2 = "&minvalue=" + value;
    var maxvalue = "&maxvalue=" + value2;


    
    if (window.location.href == hostname || window.location.href == hostname2 || window.location.href == hostname3 || (window.location.href).includes("minvalue")) {
        if ((window.location.href).includes("minvalue") && (window.location.href).includes("childid") && !(window.location.href).includes("brendid")) {
            window.location.href = hostname2 + minvalue2 + maxvalue
        }
        else if (!(window.location.href).includes("minvalue") && (window.location.href).includes("childid") && (window.location.href).includes("brendid")) {
            window.location.href = hostname3 + minvalue2 + maxvalue
        }
        else if (!(window.location.href).includes("minvalue") && (window.location.href).includes("childid") && !(window.location.href).includes("brendid")) {
            window.location.href = hostname2 + minvalue2 + maxvalue
        }
        else if ((window.location.href).includes("minvalue") && (window.location.href).includes("childid") && (window.location.href).includes("brendid")) {
            window.location.href = hostname3 + minvalue2 + maxvalue
        }
        else {
            window.location.href = hostname + minvalue + maxvalue
        }
     
    }
})

$(".input2").mouseup(function () {
    var value2 = this.value;
    var value = this.previousElementSibling.value;
    var id = document.querySelector(".hiddeninput").value;
    var childid = document.querySelector(".hiddeninput2").value;
    var brendid = document.querySelector(".hiddeninput3").value;


    var hostname = "http://localhost:14381/Category/Index/" + id;
    var hostname2 = "http://localhost:14381/Category/Index/" + id + "?childid=" + childid;
    var hostname3 = "http://localhost:14381/Category/Index/" + id + "?brendid=" + brendid + "&childid=" + childid;

    var minvalue = "?minvalue=" + value;
    var minvalue2 = "&minvalue=" + value;
    var maxvalue = "&maxvalue=" + value2;



    if (window.location.href == hostname || window.location.href == hostname2 || window.location.href == hostname3 || (window.location.href).includes("minvalue")) {
        if ((window.location.href).includes("minvalue") && (window.location.href).includes("childid") && !(window.location.href).includes("brendid")) {
            window.location.href = hostname2 + minvalue2 + maxvalue
        }
        else if (!(window.location.href).includes("minvalue") && (window.location.href).includes("childid") && (window.location.href).includes("brendid")) {
            window.location.href = hostname3 + minvalue2 + maxvalue
        }
        else if (!(window.location.href).includes("minvalue") && (window.location.href).includes("childid") && !(window.location.href).includes("brendid")) {
            window.location.href = hostname2 + minvalue2 + maxvalue
        }
        else if ((window.location.href).includes("minvalue") && (window.location.href).includes("childid") && (window.location.href).includes("brendid")) {
            window.location.href = hostname3 + minvalue2 + maxvalue
        }
        else {
            window.location.href = hostname + minvalue + maxvalue
        }

    }
})

$(document).on("click", ".fav", function () {
    $(this).removeClass("fav");
    document.querySelector(".numberofproduct.fav").innerHTML = +document.querySelector(".numberofproduct.fav").innerHTML - 1;
});
$(document).on("click", ".non-fav", function () {
    $(this).removeClass("non-fav")
    document.querySelector(".numberofproduct.fav").innerHTML = +document.querySelector(".numberofproduct.fav").innerHTML + 1;
});
$(document).on("click", ".addbasket", function () {
    $(this).removeClass("addbasket");
    document.querySelector(".numberofproduct.basket").innerHTML = +document.querySelector(".numberofproduct.basket").innerHTML + 1;
});
