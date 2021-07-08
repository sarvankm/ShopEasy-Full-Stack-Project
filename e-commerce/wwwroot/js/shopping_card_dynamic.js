const name = document.querySelectorAll(".name");
const count = document.querySelectorAll(".countproduct");
const price = document.querySelectorAll(".price");
let totalamount = document.querySelector(".totalamount");

totalamount.innerHTML = 0;

if (!JSON.parse(localStorage.getItem("basket"))) {
    console.log("rrrrrrr");
    const arr = [];
    for (let i = 0; i < name.length; i++) {
        const obj = { productName: name[i].textContent, productCount: count[i].innerHTML, productPrice: price[i].innerHTML };
        arr.push(obj);
        localStorage.setItem("basket", JSON.stringify(arr));

        totalamount.innerHTML = parseInt(totalamount.innerHTML) + parseInt(price[i].innerHTML);
        localStorage.setItem("totalamount", totalamount.innerHTML)
    }
    const basket = JSON.parse(localStorage.getItem("basket"));
    basket.map((prod) => {
        prod.productCount = 1;
        for (let i = 0; i < count.length; i++) {
            count[i].innerHTML = prod.productCount;
        }
    })
    localStorage.setItem("basket", JSON.stringify(basket));
}
else {
    const basket = JSON.parse(localStorage.getItem("basket"));
    const arr = [];

    for (let i = 0; i < name.length; i++) {
        console.log(name.length);

        if (basket.length === name.length) {
            console.log("girdi");

            count[i].innerHTML = basket[i].productCount;
            totalamount.innerHTML = localStorage.getItem("totalamount");

        }
        else {
            console.log("girdi2");
            for (let i = 0; i < name.length+1; i++) {
                if (arr.length == 0) {
                    for (let i = 0; i < basket.length; i++) {
                        arr.push(basket[i]);
                    }
                }
                if (!arr.find(a => a.productName == name[i].innerHTML)) {
                    console.log(arr);
                    const obj = { productName: name[i].innerHTML, productCount: 1, productPrice: price[i].innerHTML };
                    arr.push(obj);
                    localStorage.setItem("basket", JSON.stringify(arr));
                    totalamount.innerHTML = parseInt(localStorage.getItem("totalamount")) + parseInt(obj.productPrice);
                    localStorage.setItem("totalamount", totalamount.innerHTML);
                    const basket2 = JSON.parse(localStorage.getItem("basket"));
                    for (let i = 0; i < basket2.length; i++) {
                        count[i].innerHTML = basket2[i].productCount;

                    }
                }
            }

           
        }
    }

}

function increase() {
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
function decrease() {
    const basket = JSON.parse(localStorage.getItem("basket"));
    for (var i = 0; i < basket.length; i++) {
        if (event.target.parentElement.parentElement.children[2].children[0].innerHTML == basket[i].productName) {
            event.target.previousElementSibling.innerHTML = (parseInt(basket[i].productCount) - 1).toString();
            totalamount.innerHTML = parseInt(localStorage.getItem("totalamount")) - parseInt(basket[i].productPrice);
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
    basket.splice(basket.indexOf(deletedProduct), 1)
    if (basket.length===0) {
        localStorage.clear();
    }
    else {
        localStorage.setItem("basket", JSON.stringify(basket));
    }
    
}