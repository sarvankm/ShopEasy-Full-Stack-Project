const name = document.querySelectorAll(".name");
const count = document.querySelectorAll(".countproduct");
const price = document.querySelectorAll(".price");
let totalamount = document.querySelector(".totalamount");
for (var i = 0; i < count.length; i++) {
    var arrr = [];
    arrr.push(count[i]);
}
if (!JSON.parse(localStorage.getItem("basket"))) {
    console.log("rrrrrrr");
    const arr = [];
    for (var i = 0; i < name.length; i++) {
        const obj = { productName: name[i].textContent, productCount: count[i].innerHTML, productPrice: price[i].innerHTML };
        arr.push(obj);
        localStorage.setItem("basket", JSON.stringify(arr));
        if (totalamount.innerHTML == "") {
            totalamount.innerHTML = 0;
        }
        totalamount.innerHTML = parseInt(totalamount.innerHTML) + parseInt(price[i].innerHTML);
        localStorage.setItem("totalamount", totalamount.innerHTML)
    }
    const basket = JSON.parse(localStorage.getItem("basket"));
    basket.map((prod) => {
        prod.productCount = 1;
        for (var i = 0; i < count.length; i++) {
            count[i].innerHTML = prod.productCount;
        }
    })
    localStorage.setItem("basket", JSON.stringify(basket));
}
else {
    const basket = JSON.parse(localStorage.getItem("basket"));

    for (var i = 0; i < name.length; i++) {

        if (basket.length == name.length) {
            count[i].innerHTML = basket[i].productCount;
            totalamount.innerHTML = localStorage.getItem("totalamount");

        }
        else {
            const arr = [];
            for (var i = 0; i < basket.length; i++) {
                arr.push(basket[i]);
            }
            console.log(arr);
            const obj = { productName: name[i].innerHTML, productCount: 1, productPrice: price[i].innerHTML };
            arr.push(obj);
            localStorage.setItem("basket", JSON.stringify(arr));
            console.log(parseInt(obj.productPrice));
            totalamount.innerHTML = parseInt(localStorage.getItem("totalamount")) + parseInt(obj.productPrice);
            localStorage.setItem("totalamount", totalamount.innerHTML)
            const basket2 = JSON.parse(localStorage.getItem("basket"));

            for (var i = 0; i < basket2.length; i++) {
                count[i].innerHTML = basket2[i].productCount;

            }
        }
    }

}

function myfunction() {
    const basket = JSON.parse(localStorage.getItem("basket"));
    for (var i = 0; i < basket.length; i++) {
        if (event.target.parentElement.parentElement.children[2].children[0].innerHTML == basket[i].productName) {
            event.target.nextElementSibling.innerHTML = (parseInt(basket[i].productCount) + 1).toString();
            totalamount.innerHTML = parseInt(localStorage.getItem("totalamount")) + parseInt(basket[i].productPrice);
            localStorage.setItem("totalamount", totalamount.innerHTML)
        }
    }

    const arr = [];
    for (let i = 0; i < name.length; i++) {
        const obj = { productName: name[i].textContent, productCount: count[i].innerHTML, productPrice: price[i].innerHTML };
        arr.push(obj);
        localStorage.setItem("basket", JSON.stringify(arr));
    }
};
function myfunc() {
    const basket = JSON.parse(localStorage.getItem("basket"));
    const deletedProduct = basket.find(prod => prod.productName == event.target.parentElement.parentElement.nextElementSibling.nextElementSibling.children[0].innerHTML);
    totalamount.innerHTML = parseInt(totalamount.innerHTML) - parseInt(deletedProduct.productPrice);
    localStorage.setItem("totalamount", totalamount.innerHTML)
    basket.splice(basket.indexOf(deletedProduct),1)
    localStorage.setItem("basket", JSON.stringify(basket));
    
}