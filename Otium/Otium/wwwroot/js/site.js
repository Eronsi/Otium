function validateName() {
    var name = $('input[name=name]').val();

    if (name.length > 3 && name.length < 20) {
        return true;
    }

    alert('Имя должно содержать от 3 до 20 символов.');
    return false;
}

function validatePhone() {
    var phone = $('input[name=phone]').val();
    var re = /^(\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$/;
    var valid = re.test(phone);

    if (valid) {
        return true;
    }

    alert('Номер телефона введен некорректно.');
    return false;
}

function validateForm() {
    if (!validateName()) {
        return false;
    }

    if (!validatePhone()) {
        return false;
    }

    return true;
}

function selectElements(findClass){
    var allElem, arrE = [], i;
    if (document.getElementsByClassName){
        return document.getElementsByClassName(findClass);
    }

    allElem = document.body.getElementsByTagName('*');

    i = allElem.length;
    while (i--) {
        if (allElem[i].className == findClass)
            arrE.push(allElem[i]);
    };

    return arrE;
}

function sumPrice(basePrice) {
    var price = basePrice;
    var arr = [];
    arr = selectElements('culcCategory');
    var trueArr = Array.from(arr);

    trueArr.forEach(function sumNumber( currentValue ) {
        var value = [].filter
            .call(currentValue.options, option => option.selected)
            .map(option => option.text);
        value = String(value).match(/\d+/);
        price = parseInt(price) + parseInt(value)
    });

    priceResult.textContent = price;
}

function isMobile() {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        return true;
    }
    else {
        return false;
    }
}

$(document).ready(function () {
    $("#callback").submit(function () {
        if (validateForm()) {
            var form = $('#' + $(this).attr('id'));
            $.ajax({
                type: "POST",
                url: '/Home/Callback/',
                data: form.serialize(),
                beforeSend: function () {
                    $(form).html('<p style="text-align:center">Отправка...</p>');
                },
                success: function (data) {
                    $(form).html('<p style="text-align:center">' + data + '</p>');
                },
                error: function (error) {
                    $(form).html('<p style="text-align:center">' + error + '</p>');
                }
            });
        }

        return false;
    });
});

$(document).ready(function(){
    $(".show-modal").click(function(){
        $("#modalCenter").modal("show");
    });
});

$(document).ready(function(){
    $(".show-modal-contact1").click(function(){
        $("a#phone").attr('href', 'tel:+79776483140');
        $("a#tg").attr('href', '');

        if (isMobile()) {
            $("a#wa").attr('href', 'https://wa.me/79776483140');
        }
        else {
            $("a#wa").attr('href', 'https://web.whatsapp.com/send?phone=79776483140&text&app_absent=0');
        }

        $("#modalCenterContact").modal("show");
    });
});

$(document).ready(function(){
    $(".show-modal-contact2").click(function(){
        $("a#phone").attr('href', 'tel:+79181766881');
        $("a#tg").attr('href', '');

        if (isMobile()) {
            $("a#wa").attr('href', 'https://wa.me/79181766881');
        }
        else {
            $("a#wa").attr('href', 'https://web.whatsapp.com/send?phone=79181766881&text&app_absent=0');
        }

        $("#modalCenterContact").modal("show");
    });
});