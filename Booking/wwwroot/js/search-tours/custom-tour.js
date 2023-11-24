function onChangePrice() {
	const maxPrice = 20000000;
	let priceInput = document.getElementById('range-price');
	let priceLabel = document.getElementById('price-label');


	priceLabel.innerHTML = maxPrice / 100 * priceInput.value;
	priceLabel.append(' ')
}