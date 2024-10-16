function Load(botao) {
    try {
        botao.children[0].classList.toggle("visually-hidden");
        botao.children[1].classList.toggle("visually-hidden");
    } catch (e) {

    }
}
function ShowJsonResult(result) {
    try {
        if (result.data.Status === "SUCCESS") {

            toastr["success"](result.data.Msg);
            return true;
        }
        else if (result.data.Status === "ERROR") {
            for (i = 0; i < result.data.ErrorList.length; i++) {
                toastr["error"](result.data.ErrorList[i]);
            }
            return false;
        }
        else {
            toastr["error"]("Algo não deu certo, Entre em contato com o Time de suporte");
        }
    }
    catch (ex) {
        if (result.includes("DOCTYPE")) {
            toastr["error"]("Voce não tem permissão para essa ação");
            return false;
        }
    }
}

function PostForm(form, url, successCallback, resetForm) {

    $(".needs-validation").removeClass("was-validated");

    if (form.checkValidity()) {
        Load(form.Save)
        $.ajax({
            url: url,
            type: 'POST',
            data: new FormData(form),
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                successCallback(result);
                if (resetForm) {
                    form.reset();
                }
            },
            error: function (xhr) {
                toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
            },
            complete: function () {
                Load(form.Save)
            }
        });
        return false;

    }
    else {
        $('.needs-validation').addClass("was-validated");
    }
}
function PostFormFile(form, url, successCallback, resetForm) {

    $(".needs-validation").removeClass("was-validated");
    if (form.checkValidity()) {
        Load(form.Save);

        let data = new FormData($(form)[0]);
        data.append("file", $("#FileByte").get(0).files[0]);

        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                successCallback(result);
                if (resetForm) {
                    form.reset();
                }
            },
            error: function (xhr) {
                toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
            },
            complete: function () {
                Load(form.Save);
            }
        });
        return false;
    }
    else {
        $('.needs-validation').addClass("was-validated");
    }
}
function PostData(data, url, botao, successCallback) {

    Load(botao);

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        cache: false,
        success: function (result) {

            successCallback(result);

        },
        error: function (xhr) {
            toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
        },
        complete: function () {
            Load(botao);
        }

    });
}

function PutData(data, url, botao, successCallback) {

    Load(botao);

    $.ajax({
        url: url,
        type: 'PUT',
        data: data,
        cache: false,
        success: function (result) {
            if (successCallback != null) {
                successCallback(result);
            }

        },
        error: function (xhr) {
            toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
        },
        complete: function () {
            Load(botao);
        }

    });
}
function PutForm(form, url, successCallback, resetForm) {

    $(".needs-validation").removeClass("was-validated");

    if (form.checkValidity()) {
        Load(form.Save)
        $.ajax({
            url: url,
            type: 'PUT',
            data: new FormData(form),
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                successCallback(result);
                if (resetForm) {
                    form.reset();
                }
            },
            error: function (xhr) {
                toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
            },
            complete: function () {
                Load(form.Save)
            }
        });
        return false;

    }
    else {
        $('.needs-validation').addClass("was-validated");
    }
}
function GetData(url, data, successCallback, errorCallback) {

    $.ajax({
        url: url,
        type: 'GET',
        data: data,
        cache: false,
        success: function (result) {
            if (successCallback != null) {
                successCallback(result);
            }
        },
        error: function (xhr) {
            toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
            if (errorCallback != null) {
                errorCallback();
            }
        }
    });
}
function GetAllData(url, successCallback) {
    $.ajax({
        url: url,
        type: 'GET',
        cache: false,
        success: function (result) {
            successCallback(result);
        },
        error: function (xhr) {
            toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
        }
    });
}
function RequestPartialView(url, requestType, data, successCallback, replaceTarget) {
    ShowSpinningButtons();
    replaceTarget.html("&nbsp;");

    $.ajax({
        url: url,
        type: requestType,
        data: data,
        cache: false,
        success: function (result) {
            successCallback(result, replaceTarget);
        },
        error: function (xhr) {
            toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
        },
        complete: function () {
            ShowCommandButtons();
        }
    });
}
function RenderPartial(result, replaceTarget) {
    $(replaceTarget).html(result);
}

function mascaraMoeda(campo, evento) {
    var tecla = (!evento) ? window.event.keyCode : evento.which;
    var valor = campo.value.replace(/[^\d]+/gi, '').reverse();
    var resultado = "";
    var mascara = "############,##".reverse();
    for (var x = 0, y = 0; x < mascara.length && y < valor.length;) {
        if (mascara.charAt(x) != '#') {
            resultado += mascara.charAt(x);
            x++;
        } else {
            resultado += valor.charAt(y);
            y++;
            x++;
        }
    }
    campo.value = resultado.reverse();
}

