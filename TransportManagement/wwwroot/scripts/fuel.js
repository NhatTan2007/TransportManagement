const formartter = new Intl.NumberFormat('vi-VN');
const fuelPrice = document.getElementById("FuelPrice");
function formartCurrency({ target }) {
    target.value = target.value.replace(/[^\d]/, '');
    let number = +target.value.replaceAll('.', '');
    target.value = formartter.format(number);
    if (target.id = "ShowEditFuelPrice") {
        document.getElementById("EditFuelPrice").value = number;
    } else {
        fuelPrice.value = number;
    }
}

const listFuelPrice = document.getElementsByClassName("fuel-price");
for (var i = 0; i < listFuelPrice.length; i++) {
    listFuelPrice[i].innerHTML = formartter.format(listFuelPrice[i].innerHTML) + " VNĐ/lít";
}

var fuel = fuel || {}
const editFuelName = document.getElementById("EditFuelName");
const editFuelId = document.getElementById("EditFuelId");
const editFuelPrice = document.getElementById("EditFuelPrice");
const showEditFuelPrice = document.getElementById("ShowEditFuelPrice")
fuel.edit = async function (id) {
    await $.ajax({
        url: '/Fuel/GetFuel',
        method: 'GET',
        contentType: 'application/json',
        data: { fuelId: id },
        dataType: 'json'
    }).done(function (data) {
        editFuelName.value = data.fuelName;
        editFuelId.value = data.fuelId;
        editFuelPrice.value = data.fuelPrice;
        if (data.fuelPrice > 0) {
            showEditFuelPrice.value = formartter.format(data.fuelPrice);
        }
    });
    $("#editFuel").modal();
}