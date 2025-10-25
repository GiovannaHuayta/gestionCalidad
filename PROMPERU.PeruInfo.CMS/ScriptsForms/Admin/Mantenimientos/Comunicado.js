$(document).ready(function () {

    ListarBloques();
    ListarIdiomas();

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    $("#fileArchivoDescarga").change(function (e) {
        e.preventDefault();

        var file = $(this).prop("files")[0];

        var fileName = file.name;
        var fileExtension = fileName.split('.').pop().toLowerCase();
        if (fileExtension !== 'pdf') {
            alert('Solo se permiten archivos PDF.');
            $(this).val('');
            return; 
        }

        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}comunicado/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#btnArchivoDescarga").attr("href", `${baseURL}files/comunicado/${response.file}`);
                    $("#btnArchivoDescarga").html("Descargar Archivo");
                    $("#txtArchivoDescarga").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#txtDescripcion").ckeditor({
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
        $("#lblRegistro").html("Crear comunicado covid / alerta");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnComunicadoTraduccionId").val() === "") {
            RegistrarBloque();
        } else if ($("#hdnComunicadoId").val() !== "" && $btnGuardarBloque.val() === "Idioma") {
            RegistrarIdioma();
        } else {
            ActualizarBloque($("#hdnComunicadoTraduccionId").val());
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

    $('#ddlIdioma').change(function () {
        const vComunicadoId = $("#hdnComunicadoId").val().trim();
        const vIdiomaId = $("#ddlIdioma").val();
        if (vComunicadoId !== "") {
            $("#txtTitulo, #txtDescripcion, #fileArchivoDescarga, #txtArchivoDescarga, #txtKeyWords").val("");
            SeleccionarBloquePorIdioma(vComunicadoId, vIdiomaId);
        }
    });
    $('#ddlIdioma').trigger('change');
});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtFechaPublicacion, #txtTitulo, #txtDescripcion, #txtKeyWords, #hdnComunicadoId, #hdnComunicadoTraduccionId").val("");
    $("#ddlIdioma").val("0");
    $("#fileArchivoDescarga, #txtArchivoDescarga").val("");
    $("#btnArchivoDescarga").attr("href", "");
    $("#btnArchivoDescarga").html("");
    $("#chkActivo").prop("checked", "checked");
    $("#chkAlerta").prop("checked", "");
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
        "ajax": `${baseURL}comunicado/listar`,
        "deferRender": true,
        "columns": [
            { "data": "TraduccionId" },
            { "data": "IdiomaNombre" },
            { "data": "Titulo" },
            { "data": "ArchivoDescarga" },
            { "data": "Alerta" },
            { "data": "Activo" },
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
            data.Alerta === true ? $("td", row).eq(4).html("Si") : $("td", row).eq(4).html("No");
            data.Activo === true ? $("td", row).eq(5).html("Si") : $("td", row).eq(5).html("No");
            if (data.ArchivoDescarga !== null && data.ArchivoDescarga !== "") {
                $("td", row).eq(3).html(`<a href="${baseURL}files/comunicado/${data.ArchivoDescarga}" id="btnDescargarArchivo" title="Descargar" target="_blank">Descargar</a>`);
            }
            else {
                $("td", row).eq(3).html(`&nbsp;`);
            }
            $("td", row).eq(6).html(data.FechaCreacion.split("T")[0]);
        }
    });

    // Editar agenda
    $("#gridBloques tbody").on("click",
        "#EditarBloque",
        function () {
            const data = table.row($(this).parents("tr")).data();
            EditarBloque(data.TraduccionId);
        });

    // Eliminar agenda
    $("#gridBloques tbody").off("click", "#EliminarBloque").on("click", "#EliminarBloque", function () {
            const data = table.row($(this).parents("tr")).data();
            console.log(data);
            EliminarBloque(data.Id, data.TraduccionId);
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

    var activo, alerta;

    const idiomaId = $("#ddlIdioma").val();
    const archivoDescargs = $("#txtArchivoDescarga").val().trim();
    const fechaPublicacion = $("#txtFechaPublicacion").val().trim();
    const titulo = $("#txtTitulo").val().trim();
    const descripcion = $("#txtDescripcion").val().trim();
    const keyWords = $("#txtKeyWords").val().trim();

    $("#chkActivo").prop("checked") ? activo = true : activo = false;
    $("#chkAlerta").prop("checked") ? alerta = true : alerta = false;

    const bloqueObj = {
        IdiomaId: idiomaId,
        ArchivoDescarga: archivoDescargs,
        FechaPublicacion: fechaPublicacion,
        Titulo: titulo,
        Descripcion: descripcion,
        Keywords: keyWords,
        Activo: activo,
        Alerta: alerta
    };

    return bloqueObj;
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    if (nuevaBloque.ArchivoDescarga === ""){
        alert("Por favor, cargue un archivo PDF. Este campo es obligatorio.", "WARNING");
        HideLoader();
        return;
    }

    $.ajax({
        url: `${baseURL}comunicado/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar el comunicado", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar el comunicado", "ERROR");
            HideLoader();
        }
    });
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

function SeleccionarBloque(comunicadoTraduccionId) {
    ShowLoader();

    $("#hdnComunicadoTraduccionId").val(comunicadoTraduccionId);

    const bloqueSeleccionado = {
        id: comunicadoTraduccionId,
        idiomaId: 0
    };

    $.ajax({
        url: `${baseURL}comunicado/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnComunicadoId").val(response.Id);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#btnArchivoDescarga").attr("href", `${baseURL}files/comunicado/${response.ArchivoDescarga}`);
                $("#btnArchivoDescarga").html("Ver Archivo");
                $("#txtArchivoDescarga").val(response.ArchivoDescarga);
                $("#txtFechaPublicacion").val(response.FechaPublicacion.split("T")[0]);
                $("#txtTitulo").val(response.Titulo);
                $("#txtDescripcion").val(response.Descripcion);
                $("#txtKeyWords").val(response.Keywords);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");
                response.Alerta ? $("#chkAlerta").prop("checked", "checked") : $("#chkAlerta").prop("checked", "");

            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function SeleccionarBloquePorIdioma(comunicadoId, idioma) {
    ShowLoader();

    $("#hdnComunicadoId").val(comunicadoId);

    const bloqueSeleccionado = {
        id: comunicadoId,
        idiomaId: idioma
    };

    $.ajax({
        url: `${baseURL}comunicado/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnComunicadoTraduccionId").val(response.TraduccionId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#btnArchivoDescarga").attr("href", `${baseURL}files/comunicado/${response.ArchivoDescarga}`);
                $("#btnArchivoDescarga").html("Descargar Archivo");
                $("#txtArchivoDescarga").val(response.ArchivoDescarga);
                $("#txtFechaPublicacion").val(response.FechaPublicacion.split("T")[0]);
                $("#txtTitulo").val(response.Titulo);
                $("#txtDescripcion").val(response.Descripcion);
                $("#txtKeyWords").val(response.Keywords);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");
                response.Alerta ? $("#chkAlerta").prop("checked", "checked") : $("#chkAlerta").prop("checked", "");
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
        $("#lblRegistro").html("Actualizar comunicado covid / alerta");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
        
    }
}

function ActualizarBloque(comunicadoTraduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = $("#hdnComunicadoId").val();
    actualizarBloque.TraduccionId = comunicadoTraduccionId;

    const nuevaBloque = LlenarDatosBloque(); 

    if (nuevaBloque.ArchivoDescarga === "") {
        alert("Por favor, cargue un archivo PDF. Este campo es obligatorio.", "WARNING");
        HideLoader();
        return;
    }

    $.ajax({
        url: `${baseURL}comunicado/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                ListarBloques();
            } else {
                alert("No se pudo actualizar el comunicado", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar el comunicado", "ERROR");
            HideLoader();
        }
    });
}

function EliminarBloque(id, traduccionId) {

    confirm("¿Está seguro de eliminar el Bloque?",
        function (result) {
            if (result) {
                const eliminarBloque = LlenarDatosBloque();
                eliminarBloque.Id = id;
                eliminarBloque.TraduccionId = traduccionId;

                $.ajax({
                    url: `${baseURL}comunicado/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó el comunicado", "CHECK");
                        } else {
                            alert("No se pudo eliminar el comunicado", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarBloques();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar el comunicado", "ERROR");
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