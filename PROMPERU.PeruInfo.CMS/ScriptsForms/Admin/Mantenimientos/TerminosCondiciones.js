$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarIdiomas();

    $("#txtContenido").ckeditor({
        toolbar:
            [
                ['Source'],
                ['Bold', 'Italic', 'Underline', 'Strike'],
                ['Format'],
                ['RemoveFormat'],
                ['NumberedList', 'BulletedList', 'Blockquote', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                ['Image', 'Table', '-', 'Link', 'Iframe'],
                ['Undo', 'Redo']
            ]
    });

    const $btnGuardarBloque = $("#btnGuardar");

    $(document).keyup(function (event) {
        if (event.which === 27) {
            $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
            $("#txtFechaTerminosCondiciones").datetimepicker("hide");
        }
    });

    $btnGuardarBloque.click(function () {
        ActualizarBloque($("#hdnBloqueTraduccionId").val());
    });

    $("#btnMenu").click(function () {
        $(".side-menu").toggleClass("slide-menu");
        if ($("#divOverlayPopUp").length === 0) {
            $("body").append('<div class="overlayPopup" id="divOverlayPopUp"></div>');
        }

        $("#divOverlayPopUp").width(screen.width * 4 + "px");
        $("#divOverlayPopUp").height(screen.height * 4 + "px");
        $("#divOverlayPopUp").css({ 'z-index': (200), 'margin-top': 60 });
        $("#divOverlayPopUp").toggle();
    });
    $(".menu-option").click(function () {
        $(".side-menu").toggleClass("slide-menu");
        $("#divOverlayPopUp").hide();
    });

    $('#ddlIdioma').change(function () {
        const vIdiomaId = $("#ddlIdioma").val();
        LimpiarFormTerminosCondiciones();
        SeleccionarBloque(vIdiomaId);
    });
    $('#ddlIdioma').trigger('change');

    EditarBloque(1);
});

var baseURL = $("base").attr("href");

function LimpiarFormTerminosCondiciones() {
    $("#chkActivo").prop("checked", "checked");
    $("#txtContenido").val("");
}

function LlenarDatosBloque() {

    var activo;

    const id = $("#hdnBloqueId").val().trim();
    const idiomaId = $("#ddlIdioma").val();
    const contenido = $("#txtContenido").val().trim();

    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    const bloqueObj = {
        Id: id,
        Activo: activo,
        IdiomaId: idiomaId,
        Contenido: contenido
    };

    return bloqueObj;
}

function ListarIdiomas() {
    $.ajax({
        url: `${baseURL}idioma/listar`,
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                response.data.forEach(function (value) {
                    $("#ddlIdioma").append(`<option value="${value.Id}">${value.Nombre}</option>`);
                });
            }
        }
    });
}

function SeleccionarBloque(idiomaId) {
    ShowLoader();

    const bloqueSeleccionado = {
        idiomaId: idiomaId
    };

    $.ajax({
        url: `${baseURL}TerminosCondiciones/SeleccionarPorIdioma`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnBloqueId").val(response.Id);
                $("#hdnBloqueTraduccionId").val(response.TraduccionId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#txtContenido").val(response.Contenido);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");
            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function EditarBloque(idiomaId) {
    if (idiomaId !== null && idiomaId !== "") {
        SeleccionarBloque(idiomaId);
    }
}

function ActualizarBloque(traduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.TraduccionId = traduccionId;

    $.ajax({
        url: `${baseURL}TerminosCondiciones/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                alert("Se actualizó el registro", "SUCCESS");
            } else {
                alert("No se pudo actualizar los Términos y Condiciones", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar los Términos y Condiciones", "ERROR");
            HideLoader();
        }
    });
}