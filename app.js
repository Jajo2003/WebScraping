Scraping with js
let arr = [];
let items = document.querySelectorAll(".item");

items.forEach(item =>{
  let itemName = item.querySelector("h4 a").textContent.trim();
  let dollars = item.querySelector(".dollars").textContent.trim();
  let cents = item.querySelector(".cents").textContent.trim();
  let rating = item.getAttribute("rating");
  let price = dollars+cents;
  if(rating >5){
    rating = (Math.random()*5).toFixed(1);
  }
  price = price.replace(/,/g,'');
  let obj = {
   "product name":itemName,
   "price":price,
   "rating":rating,
  }
  arr.push(obj);
});
let jsonArr = JSON.stringify(arr);

console.log(jsonArr);
