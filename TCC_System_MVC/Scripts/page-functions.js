function ShowSpinningButtons() {
    $(".command-button").hide();
    $(".spinning-button").show();
}
function ShowCommandButtons() {
    $('.spinning-button').hide();
    $('.command-button').show();
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
        ShowSpinningButtons();
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
                ShowCommandButtons();
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
        ShowSpinningButtons();

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
                ShowCommandButtons();
            }
        });
        return false;
    }
    else {
        $('.needs-validation').addClass("was-validated");
    }
}
function PostData(data, url, successCallback) {

    ShowSpinningButtons();

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
            ShowCommandButtons();
        }

    });
}

function PostDataSearch(url, data, successCallback) {

    ShowSpinningButtons();

    $.ajax({
        url: url,
        type: 'POST',
        data: new FormData(data),
        contentType: false,
        processData: false,
        cache: false,
        success: function (result) {

            successCallback(result);
            $(data.childNodes[1].childNodes[3]).collapse("hide")


        },
        error: function (xhr) {
            toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
        },
        complete: function () {
            ShowCommandButtons();
        }

    });
}
function GetData(url, data, successCallback) {
    $.ajax({
        url: url,
        type: 'GET',
        data: data,
        cache: false,
        success: function (result) {
            successCallback(result);
        },
        error: function (xhr) {
            toastr["error"]("An error occured: " + xhr.status + " " + xhr.statusText);
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
function DisableButtonForDefaultTables(url, btnValue) {
    var row;

    $(document).on("click", ".cancel-item", function () {
        row = $(this).closest('tr');
        toastr["warning"]('<div><p>Deseja realmente realizar o Procedimento ?</p><button type="button" id="DelButton" data-id="' + $(this).data('id') + '" class="btn btn-sm btn-danger">' + btnValue + '</button></div>')
    });

    $(document).on("click", "#DelButton", function () {
        PostData(url, { id: $(this).data('id') }, HideTableButtonsForInactiveRow)
    });

    function HideTableButtonsForInactiveRow(result) {
        if (ShowJsonResult(result)) {
            row.find('td').eq(0).html("&nbsp;");
            row.find('td').eq(1).html("&nbsp;");
            row.find("input[class='check-box'").prop('checked', true);
        }
    }
}

function DesableButtonTable(botao, url) {

    var a = { Id: botao.dataset.id }

    PostData(url, a, Disable);

    function Disable(result) {

        ShowJsonResult(result)

        if (botao != null) {
            b = document.querySelectorAll('[data-id="' + botao.dataset.id + '"]')

            b.forEach((botao, index, array) => {
                DesableLineTable(botao)
            })
        }

    };
}

function DesableLineTable(btn) {
    btn.classList.remove("btn-danger")
    btn.classList.remove("btn-success")
    btn.classList.remove("btn-warning")
    btn.classList.remove("btn-info")

    if (btn.tagName == "A") {
        btn.classList.add("disabled")
    }

    btn.classList.add("btn-outline-danger")

    if (btn.id != "TargetModalAxis") {
        btn.setAttribute("disabled", "")
    }

}

function WaitingBtn(btn) {
    btn.classList.remove("btn-danger")
    btn.classList.remove("btn-success")
    btn.classList.add("btn-warning")
    btn.removeAttribute("disabled")

}
function ActiveBtn(btn) {
    btn.classList.remove("btn-warning")
    btn.classList.remove("btn-danger")
    btn.classList.add("btn-success")
    btn.removeAttribute("disabled")

}
function DisabledBtn(btn) {

    btn.classList.remove("btn-danger")
    btn.classList.remove("btn-warning")

    btn.classList.add("btn-success")

    btn.setAttribute("disabled", "")
}
function DisabledDangerBtn(btn) {

    btn.classList.remove("btn-danger")
    btn.classList.remove("btn-warning")

    btn.classList.add("btn-danger")

    btn.setAttribute("disabled", "")
}

function abilityDrawing(form) {
    form.removeAttribute("disabled")
    form.classList.remove("btn-danger")
    form.classList.add("btn-warning")
}

function validacaoCampoNum(campo) {
    campo.addEventListener("keypress", function (e) {

        const keyCode = (e.keyCode ? e.keyCode : e.wich);

        if (!((keyCode >= 48 && keyCode <= 57) || keyCode == 44 || keyCode == 46)) {
            e.preventDefault();
        }
    })
}

function ActiveDesctiveButtonTable(botao) {
    var idBotao = { Id: botao.dataset.id }

    if (botao != null) {
        targetBotao = document.querySelectorAll('[data-id="' + idBotao.Id + '"]')

        targetBotao.forEach((botao, index, array) => {
            if (botao.id == "OpenDesative") {
                targetBotao[0].classList.add("disabled")
                botao.classList.remove("btn-danger")
                botao.classList.add("btn-success")
                botao.innerHTML = '<i class="fas fa-check"></i>'
                botao.id = "OpenActive"
                return;
            }

            if (botao.id == "OpenActive") {
                targetBotao[0].classList.remove("disabled")
                botao.classList.remove("btn-success")
                botao.classList.add("btn-danger")
                botao.innerHTML = '<i class="fas fa-trash"></i>'
                botao.id = "OpenDesative"
                return;
            }

        })
    }
}

function DesctiveButtonTable(botao) {
    var idBotao = { Id: botao.dataset.id }

    if (botao != null) {
        targetBotao = document.querySelectorAll('[data-id="' + idBotao.Id + '"]')

        targetBotao.forEach((botao, index, array) => {
            if (botao.id == "OpenDesative") {
                botao.setAttribute("disabled", "disabled")
            }
        })
    }
}

function verificaCampoPorRadio(campo, radio) {

    if (radio[0].checked) {
        campo.removeAttribute('disabled', 'disabled')
    }
    else{
        campo.setAttribute('disabled', 'disabled')
    }

    $(radio).on("change", function () {
        if (radio[0].checked) {
            campo.removeAttribute('disabled', 'disabled')
        }
        else {
            campo.setAttribute('disabled', 'disabled')
        }
    })
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

