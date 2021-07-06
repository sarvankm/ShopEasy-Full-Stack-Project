const name = document.querySelectorAll(".name");
const count = document.querySelectorAll(".countproduct");
const price = document.querySelectorAll(".price");
let totalamount = document.querySelector(".totalamount");
if (!JSON.parse(localStorage.getItem("basket"))) {
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
    for (var i = 0; i < basket.length; i++) {
        count[i].innerHTML = basket[i].productCount;

    }
    localStorage.setItem("basket", JSON.stringify(basket));
    totalamount.innerHTML = localStorage.getItem("totalamount");
}


function myfunction() {
    const basket = JSON.parse(localStorage.getItem("basket"));
    for (var i = 0; i < basket.length; i++) {
        if (event.target.nextElementSibling.innerHTML === basket[i].productCount) {
            event.target.nextElementSibling.innerHTML = (parseInt(basket[i].productCount) + 1).toString();
            totalamount.innerHTML = parseInt(totalamount.innerHTML) + parseInt(basket[i].productPrice);
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