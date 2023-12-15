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
    let total = document.querySelector("#totalParticipation").value;

    let parentCard = document.querySelector("#card-info");

    // Check if the parent element exists
    if (parentCard) {
        // Remove all child nodes
        while (parentCard.firstChild) {
            parentCard.removeChild(parentCard.firstChild);
        }
        let childs = [];
        for (var i = 0; i < parseInt(total); i++) {
            let childElement = `
            <div class="col-lg-12 col-12 d-flex justify-content-center ">
    <!--chi tiet lien he-->
    <!-- Card start -->
    <div class="col-sm-6 col-12 ">
        <div class="card-border bg-color sd">
            <div class="card-border-title">Chi tiết liên hệ - Du khách ${i+1} (vé)</div>
            <div class="card-border-body">

                <div class="row">
                    <div class="col-sm-12 col-12">
                        <div class="mb-3">
                            <label class="form-label">Họ <span class="text-red">*</span></label>
                            <input type="text" class="form-control" placeholder="">
                        </div>
                    </div>
                    <div class="col-sm-12 col-12">
                        <div class="mb-3">
                            <label class="form-label">Tên <span class="text-red">*</span></label>
                            <input type="text" class="form-control" placeholder="">
                        </div>
                    </div>
                    <div class="col-sm-12 col-12">
                        <div class="mb-0">
                            <label class="form-label">Yêu cầu thêm(tùy chọn) <span class="text-red">*</span></label>
                            <textarea rows="4" class="form-control"
                                      placeholder=""></textarea>
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
        parentCard.innerHTML = childs.join("");

    }


}
