function MinusQuantityOfAdult(adult, child) {
    var preValue = document.getElementById("quantityAdult").value;
    let totalParticipation = document.querySelector("#totalParticipation").value;

    if (parseInt(preValue) != 0) {
        document.getElementById("quantityAdult").value = parseInt(preValue) - 1;
        document.querySelector("#totalParticipation").value = parseInt(totalParticipation) - 1;
    }

    let quantityChild = document.querySelector("#quantityChild").value;
    let quantityAdult = document.querySelector("#quantityAdult").value;
    
        let price = adult * parseInt(quantityAdult) + child * parseInt(quantityChild);
        document.querySelector("#sumPrice").value = parseFloat(price);

}

function MinusQuantityOfChild(adult, child) {
    var preValue = document.getElementById("quantityChild").value;
    let totalParticipation = document.querySelector("#totalParticipation").value;
   
    if (parseInt(preValue) != 0) {
        document.getElementById("quantityChild").value = parseInt(preValue) - 1;
        document.querySelector("#totalParticipation").value = parseInt(totalParticipation) - 1;
    }

    let quantityChild = document.querySelector("#quantityChild").value;
    let quantityAdult = document.querySelector("#quantityAdult").value;
    

        let price = adult * parseInt(quantityAdult) + child * parseInt(quantityChild);
        document.querySelector("#sumPrice").value = parseFloat(price);
    
}

function PlusQuantityOfChild(adult, child) {
    let preValue = document.querySelector("#quantityChild").value;
    document.querySelector("#quantityChild").value = parseInt(preValue) + 1;
    let totalParticipation = document.querySelector("#totalParticipation").value;
    document.querySelector("#totalParticipation").value = parseInt(totalParticipation) + 1;
    let quantityChild = document.querySelector("#quantityChild").value;
    let quantityAdult = document.querySelector("#quantityAdult").value;

    let price = adult * parseInt(quantityAdult) + child * parseInt(quantityChild);
    document.querySelector("#sumPrice").value = parseFloat(price);
}

function PlusQuantityOfAdult(adult, child) {
    let preValue = document.querySelector("#quantityAdult").value;
    let totalParticipation = document.querySelector("#totalParticipation").value;
    document.querySelector("#totalParticipation").value = parseInt(totalParticipation) + 1;
    document.querySelector("#quantityAdult").value = parseInt(preValue) + 1;
    
    let quantityChild = document.querySelector("#quantityChild").value;
    let quantityAdult = document.querySelector("#quantityAdult").value;

    let price = adult * parseInt(quantityAdult) + child * parseInt(quantityChild);
    document.querySelector("#sumPrice").value = parseFloat(price);
   
}


