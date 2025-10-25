$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarBloques();
    ListarIdiomas();
    ListarPais();

    $("#fileImagen3").change(function (e) {
        e.preventDefault();

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}Negocio/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagen3").attr("src", `${baseURL}files/Negocio/${response.file}`);
                    $("#txtImagen3").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#txtDetalle").ckeditor({
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

    const $btnNuevoBloque = $("#btnNuevo");
    const $btnCancelar = $("#btnCancelar");
    const $btnGuardarBloque = $("#btnGuardar");

    $(document).keyup(function (event) {
        if (event.which === 27) {
            $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
            $("#txtFechaInicio").datetimepicker("hide");
        }
    });

    $btnNuevoBloque.click(function () {
        $.ajax({
            url: `${baseURL}/usuario/verificarSesion`,
            type: 'POST',
            success: function (response) {
                if (response.autenticado) {
                    $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
                    LimpiarPopupBloque();
                } else {
                    window.location.href = baseURL + 'usuario/login';
                }
            },
            error: function () {
                window.location.href = baseURL + 'usuario/login';
            }
        });
    });

    $btnCancelar.click(function () {
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
        LimpiarPopupBloque();
        $("#btnGuardar").val("");
        $("#lblRegistro").html("Crear Restaurante");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnNegocioId").val() === "") {
            RegistrarBloque();
        } else {
            ActualizarBloque($("#hdnNegocioId").val());
        }
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
});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtFechaPublicacion, #txtTitulo, #txtResumen, #txtDetalle, #txtAltImagen, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords, #txtDireccionWeb, #txtTelefono, #txtCiudad, #txtDireccion, #hdnNegocioId, #hdnNegocioTraduccionId").val("");
    $("#ddlIdioma, #ddlPais").val("0");
    $("#fileImagen3, #txtImagen3").val("");
    $("#imgImagen3").attr("src", "");
    $("#chkDestacado, #chkActivo").prop("checked", "checked");
    /*    $("#chkAlerta").prop("checked", "");*/
}

function ListarBloques() {
    ShowLoader();

    var table = $("#gridBloques").DataTable({
        "destroy": true,
        "paging": false,
        "searching": true,
        // orderCellsTop: true,
        "pagingType": "full_numbers",
        // fixedHeader: true,
        "order": [[6, "desc"]], // Sort by first column descending
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": `${baseURL}Negocio/listar`,
        "deferRender": true,
        "columns": [
            { "data": "Id" },
            { "data": "IdiomaNombre" },
            { "data": "PaisNombre" },
            { "data": "Titulo" },
            { "data": "Activo" },
            { "data": "Ciudad" },
            { "data": "FechaPublicacion" },
            {
                "defaultContent": `<div class="accion-box">
                            <a href="javascript:" id="EditarBloque" title="Editar">
                                <span class="fas fa-edit"></span>
                            </a>
                            <a href="javascript:" id="EliminarBloque" title="Eliminar">
                                <span class="fas icon-cross"></span>
                            </a>
                        </div>`
            }
        ],
        "createdRow": function (row, data, index) {
            data.Activo === true ? $("td", row).eq(4).html("Si") : $("td", row).eq(4).html("No");
            $("td", row).eq(6).html(data.FechaPublicacion.split("T")[0]);
        }
    });

    // Editar agenda
    $("#gridBloques tbody").on("click",
        "#EditarBloque",
        function () {
            const data = table.row($(this).parents("tr")).data();
            EditarBloque(data.Id);
        });

    // Eliminar agenda
    $("#gridBloques tbody").off("click", "#EliminarBloque").on("click", "#EliminarBloque", function () {
            const data = table.row($(this).parents("tr")).data();
            console.log(data);
            EliminarBloque(data.Id);
        });

    //$('#gridBloques thead tr').clone(true).appendTo('#gridBloques thead');
    $("#gridBloques thead tr:eq(0) th").each(function (i) {
        $("input", this).on("keyup change",
            function () {
                if (table.column(i).search() !== this.value) {
                    table
                        .column(i)
                        .search(this.value)
                        .draw();
                }
            });
    });

    HideLoader();
}

function LlenarDatosBloque() {

    var destacado, activo;

    const idiomaId = $("#ddlIdioma").val();
    const paisId = $("#ddlPais").val();
    const titulo = $("#txtTitulo").val().trim();
    const resumen = $("#txtResumen").val().trim();
    const detalle = $("#txtDetalle").val().trim();
    const imagen3 = $("#txtImagen3").val().trim();
    const altImagen = $("#txtAltImagen").val().trim();
    const fechaPublicacion = $("#txtFechaPublicacion").val().trim();
    const tituloSEO = $("#txtTituloSEO").val().trim();
    const keyWords = $("#txtKeyWords").val().trim();
    const descripcionSEO = $("#txtDescripcionSEO").val().trim();
    const direccionWeb = $("#txtDireccionWeb").val();
    const telefono = $("#txtTelefono").val().trim();
    const ciudad = $("#txtCiudad").val().trim();
    const direccion = $("#txtDireccion").val().trim();

    $("#chkDestacado").prop("checked") ? destacado = true : destacado = false;
    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    const bloqueObj = {
        IdiomaId: idiomaId,
        PaisId: paisId,
        Titulo: titulo,
        Resumen: resumen,
        Detalle: detalle,
        Imagen3: imagen3,
        AltImagen: altImagen,
        FechaPublicacion: fechaPublicacion,
        TituloSeo: tituloSEO,
        Keywords: keyWords,
        DescripcionSeo: descripcionSEO,
        DireccionWeb: direccionWeb,
        Telefono: telefono,
        Ciudad: ciudad,
        Direccion: direccion,
        Destacado: destacado,
        Activo: activo
    };

    return bloqueObj;
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    if (!ValidarDireccionWeb(nuevaBloque.DireccionWeb)) {
        alert("Por favor, incluye el protocolo (http:// o https://) en la dirección web.");
        $("#txtDireccionWeb").focus();
        HideLoader();
        return;
    }

    $.ajax({
        url: `${baseURL}Negocio/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar el Negocio", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar la Negocio", "ERROR");
            HideLoader();
        }
    });
}

function ValidarDireccionWeb(direccionWeb) {
    if (direccionWeb !== "") {
        return /^https?:\/\//i.test(direccionWeb);
    }
    return true;
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

function ListarPais() {
    $.ajax({
        url: `${baseURL}pais/ListarParaSelect`,
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                response.data.forEach(function (value) {
                    $("#ddlPais").append(`<option value="${value.Id}">${value.Nombre}</option>`);
                });
            }
        }
    });
}

function SeleccionarBloque(negocioId) {
    ShowLoader();

    $("#hdnNegocioId").val(negocioId);

    const bloqueSeleccionado = {
        id: negocioId
    };

    $.ajax({
        url: `${baseURL}Negocio/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlPais").val(response.PaisId);
                $("#txtTitulo").val(response.Titulo);
                $("#txtResumen").val(response.Resumen);
                $("#txtDetalle").val(response.Detalle);
                $("#txtImagen3").val(response.Imagen3);
                $("#imgImagen3").attr("src", `${baseURL}files/negocio/${response.Imagen3}`);
                $("#txtAltImagen").val(response.AltImagen);
                $("#txtFechaPublicacion").val(response.FechaPublicacion.split("T")[0]);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtKeyWords").val(response.Keywords);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtDireccionWeb").val(response.DireccionWeb);
                $("#txtTelefono").val(response.Telefono);
                $("#txtCiudad").val(response.Ciudad);
                $("#txtDireccion").val(response.Direccion);

                response.Destacado ? $("#chkDestacado").prop("checked", "checked") : $("#chkDestacado").prop("checked", "");
                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");

            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function EditarBloque(videoId) {
    if (videoId !== null && videoId !== "") {

        SeleccionarBloque(videoId);
        $("#btnGuardar").val("Actualizar");
        $("#lblRegistro").html("Actualizar Restaurante");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
    }
}

function ActualizarBloque(negocioId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = negocioId;

    const nuevaBloque = LlenarDatosBloque();

    if (!ValidarDireccionWeb(nuevaBloque.DireccionWeb)) {
        alert("Por favor, incluye el protocolo (http:// o https://) en la dirección web.");
        $("#txtDireccionWeb").focus();
        HideLoader();
        return;
    }

    $.ajax({
        url: `${baseURL}Negocio/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                ListarBloques();
            } else {
                alert("No se pudo actualizar el Negocio", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar el Negocio", "ERROR");
            HideLoader();
        }
    });
}

function EliminarBloque(id) {

    confirm("¿Está seguro de eliminar el Bloque?",
        function (result) {
            if (result) {
                const eliminarBloque = LlenarDatosBloque();
                eliminarBloque.Id = id;

                $.ajax({
                    url: `${baseURL}Negocio/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó el Negocio", "CHECK");
                        } else {
                            alert("No se pudo eliminar el Negocio", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarBloques();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar el Negocio", "ERROR");
                    }
                });
            } else {
                return false;
            }

            return false;
        });

    $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });

    ListarBloques();
}

function countChars(obj) {
    var maxLength = 500;
    var strLength = obj.value.length;
    var charRemain = (maxLength - strLength);

    if (charRemain < 0) {
        document.getElementById("charNum").innerHTML = '<span style="color: red;">Ha excedido el límite de ' + maxLength + ' caracteres</span>';
    } else {
        document.getElementById("charNum").innerHTML = charRemain + ' caracteres restantes';
    }
}

function countCharsSEO(obj) {
    var maxLength = 255;
    var strLength = obj.value.length;
    var charRemain = (maxLength - strLength);

    if (charRemain < 0) {
        document.getElementById("charNumSEO").innerHTML = '<span style="color: red;">Ha excedido el límite de ' + maxLength + ' caracteres</span>';
    } else {
        document.getElementById("charNumSEO").innerHTML = charRemain + ' caracteres restantes';
    }
}