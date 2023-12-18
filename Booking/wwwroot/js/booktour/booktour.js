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
    RenderCardInfo();
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
    RenderCardInfo();
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

    RenderCardInfo();
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
    RenderCardInfo();
}

function RenderCardInfo() {

    let quantityChild = document.querySelector("#quantityChild").value;
    let quantityAdult = document.querySelector("#quantityAdult").value;
    let total = document.querySelector("#totalParticipation").value;

    let parentCard = document.querySelector("#card-info");

    // Check if the parent element exists
    if (parentCard) {
        // Remove all child nodes
        while (parentCard.firstChild) {
            parentCard.removeChild(parentCard.firstChild);
        }
        let childs = [];
        for (var i = 1; i <= parseInt(quantityAdult); i++) {
            let childElement = `
            <div class="col-lg-12 col-12 d-flex justify-content-center ">
    <!--chi tiet lien he-->
    <!-- Card start -->
    <div class="col-sm-6 col-12 ">
        <div class="card-border bg-color sd">
            <div class="card-border-title">Du khách ${i} - NGƯỜI LỚN</div>
            <div class="card-border-body">

                <div class="row">
                    <div class="col-sm-12 col-12">
                        <div class="mb-3">
                            <label class="form-label">Họ <span class="text-red">*</span></label>
                            <input type="text" class="form-control" name="firstNameAdult-${i}" id="firstNameAdult-${i}" />
                        </div>
                    </div>
                    <div class="col-sm-12 col-12">
                        <div class="mb-3">
                            <label class="form-label">Tên <span class="text-red">*</span></label>
                            <input type="text" class="form-control" name="lastNameAdult-${i}" id="lastNameAdult-${i}" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Card end -->
</div>
            `
            childs.push(childElement);
        }

        if (parseInt(quantityChild) > 0) {
            for (var i = 1; i <= parseInt(quantityChild); i++) {
                let childElement = `
            <div class="col-lg-12 col-12 d-flex justify-content-center ">
    <!--chi tiet lien he-->
    <!-- Card start -->
    <div class="col-sm-6 col-12 ">
        <div class="card-border bg-color sd">
            <div class="card-border-title">Du khách ${parseInt(quantityAdult) + i} - TRẺ EM</div>
            <div class="card-border-body">

                <div class="row">
                    <div class="col-sm-12 col-12">
                        <div class="mb-3">
                            <label class="form-label">Họ <span class="text-red">*</span></label>

                            <input type="text" class="form-control" name="firstNameChild-${i}" id="firstNameChild-${i}" />
                        </div>
                    </div>
                    <div class="col-sm-12 col-12">
                        <div class="mb-3">
                            <label class="form-label">Tên <span class="text-red">*</span></label>
                            <input type="text" class="form-control" name="lastNameChild-${i}" id="lastNameChild-${i}" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Card end -->
</div>
            `
                childs.push(childElement);
            }
        }

        parentCard.innerHTML = childs.join("");

    }


}
