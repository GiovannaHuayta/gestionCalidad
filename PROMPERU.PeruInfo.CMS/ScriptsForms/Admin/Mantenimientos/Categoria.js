$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarBloques();
    ListarIdiomas();

    $("#fileImagenDesktop").change(function (e) {
        e.preventDefault();

        const file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        const formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}categoria/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenDesktop").attr("src", `${baseURL}files/categoria/${response.file}`);
                    $("#txtImagenDesktop").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#fileImagenMovil").change(function (e) {
        e.preventDefault();

        const file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        const formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}categoria/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenMovil").attr("src", `${baseURL}files/categoria/${response.file}`);
                    $("#txtImagenMovil").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#txtBloqueHTML").ckeditor({
        toolbar:
            [
                ["Source"],
                ["Bold", "Italic", "Underline", "Strike"],
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
            /*$("#txtFechaInicio").datetimepicker("hide");*/
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
        $("#lblRegistro").html("Crear Categoría");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnCategoriaTraduccionId").val() === "") {
            RegistrarBloque();
        } else if ($("#hdnCategoriaId").val() !== "" && $btnGuardarBloque.val() === "Idioma") {
            RegistrarIdioma();
        } else {
            ActualizarBloque($("#hdnCategoriaTraduccionId").val());
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
        const vCategoriaId = $("#hdnCategoriaId").val().trim();
        const vIdiomaId = $("#ddlIdioma").val();
        if (vCategoriaId !== "") {
            $("#txtNombre, #txtTitulo, #txtSubtitulo, #txtDescripcion, #txtBloqueHTML, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords").val("");
            $("#chkActivo, #chkVisibleBuscador, #chkVisibleHome").prop("checked", "checked");
            SeleccionarBloquePorIdioma(vCategoriaId, vIdiomaId);
        }
    });
    $('#ddlIdioma').trigger('change');
});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtNombre, #txtTitulo, #txtSubtitulo, #txtDescripcion, #txtBloqueHTML, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords, #hdnCategoriaId, #hdnCategoriaTraduccionId, #txtImagenDesktop, #txtImagenMovil, #txtImagenTextoAlternativo").val("");
    $("#ddlIdioma").val("0");
    $("#chkActivo, #chkVisibleBuscador, #chkVisibleHome").prop("checked", "checked");
    $("#imgImagenDesktop, #imgImagenMovil").attr("src", "");
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
/*        "order": [[4, "desc"]], // Sort by first column descending*/
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": `${baseURL}Categoria/listar`,
        "deferRender": true,
        "columns": [
            { "data": "TraduccionId" },
            { "data": "IdiomaNombre" },
            { "data": "Nombre" },
            { "data": "Titulo" },
            { "data": "Subtitulo" },
            { "data": "Activo" },
            {
                "defaultContent": `<div class="accion-box">
                            <a href="javascript:" id="EditarBloque" title="Editar">
                                <span class="fas fa-edit"></span>
                            </a>
                            <a href="javascript:" id="EliminarBloque" title="Eliminar" style="display:none">
                                <span class="fas icon-cross"></span>
                            </a>
                        </div>`
            }
        ],
        "createdRow": function (row, data, index) {
            data.Activo === true ? $("td", row).eq(5).html("Si") : $("td", row).eq(5).html("No");
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

    var activo, visibleBuscador, visibleHome;

    const idiomaId = $("#ddlIdioma").val();
    const nombre = $("#txtNombre").val();
    const titulo = $("#txtTitulo").val().trim();
    const subtitulo = $("#txtSubtitulo").val().trim();
    const descripcion = $("#txtDescripcion").val().trim();
    const bloqueHtml = $("#txtBloqueHTML").val().trim();
    const tituloSeo = $("#txtTituloSEO").val().trim();
    const descripcionSeo = $("#txtDescripcionSEO").val().trim();
    const keyWords = $("#txtKeyWords").val().trim();

    $("#chkActivo").prop("checked") ? activo = true : activo = false;
    $("#chkVisibleBuscador").prop("checked") ? visibleBuscador = true : visibleBuscador = false;
    $("#chkVisibleHome").prop("checked") ? visibleHome = true : visibleHome = false;

    /*Galeria*/
    const entidadTipo = "Categoria";
    const rutaDesktop = $("#txtImagenDesktop").val();
    const rutaMovil = $("#txtImagenMovil").val();
    const uso = "portada";
    const textoAlternativo = $("#txtImagenTextoAlternativo").val();

    return {
        IdiomaId: idiomaId,
        Nombre: nombre,
        Titulo: titulo,
        Subtitulo: subtitulo,
        Descripcion: descripcion,
        BloqueHtml: bloqueHtml,
        TituloSeo: tituloSeo,
        DescripcionSeo: descripcionSeo,
        Keywords: keyWords,
        Activo: activo,
        VisibleBuscador: visibleBuscador,
        VisibleHome: visibleHome,
        RutaDekstop: rutaDesktop,
        RutaMovil: rutaMovil,
        EntidadTipo: entidadTipo,
        Uso: uso,
        TextoAlternativo: textoAlternativo
    };
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    $.ajax({
        url: `${baseURL}Categoria/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar el Categoria", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar la Categoria", "ERROR");
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

function SeleccionarBloque(CategoriaTraduccionId) {
    ShowLoader();

    $("#hdnCategoriaTraduccionId").val(CategoriaTraduccionId);

    const bloqueSeleccionado = {
        id: CategoriaTraduccionId,
        idiomaId: 0
    };

    $.ajax({
        url: `${baseURL}Categoria/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnCategoriaId").val(response.Id);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#txtNombre").val(response.Nombre);
                $("#txtTitulo").val(response.Titulo);
                $("#txtSubtitulo").val(response.Subtitulo);
                $("#txtDescripcion").val(response.Descripcion);
                $("#txtBloqueHTML").val(response.BloqueHtml);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtKeyWords").val(response.Keywords);
                
                /* Galeria */
                $("#hdnImagenId").val(response.ImagenId);
                $("#hdnImagenTraduccionId").val(response.ImagenTraduccionId);
                $("#txtImagenDesktop").val(response.ImagenDesktop);
                $("#imgImagenDesktop").attr("src", `${baseURL}files/categoria/${response.ImagenDesktop}`);
                $("#txtImagenMovil").val(response.ImagenMovil);
                $("#imgImagenMovil").attr("src", `${baseURL}files/categoria/${response.ImagenMovil}`);
                $("#txtImagenTextoAlternativo").val(response.ImagenTextoAlternativo);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");
                response.VisibleBuscador ? $("#chkVisibleBuscador").prop("checked", "checked") : $("#chkVisibleBuscador").prop("checked", "");
                response.VisibleHome ? $("#chkVisibleHome").prop("checked", "checked") : $("#chkVisibleHome").prop("checked", "");

            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function SeleccionarBloquePorIdioma(CategoriaId, idioma) {
    ShowLoader();

    $("#hdnCategoriaId").val(CategoriaId);

    const bloqueSeleccionado = {
        id: CategoriaId,
        idiomaId: idioma
    };

    $.ajax({
        url: `${baseURL}Categoria/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnCategoriaTraduccionId").val(response.TraduccionId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#txtNombre").val(response.Nombre);
                $("#txtTitulo").val(response.Titulo);
                $("#txtSubtitulo").val(response.Subtitulo);
                $("#txtDescripcion").val(response.Descripcion);
                $("#txtBloqueHTML").val(response.BloqueHtml);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtKeyWords").val(response.Keywords);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");
                response.VisibleBuscador ? $("#chkVisibleBuscador").prop("checked", "checked") : $("#chkVisibleBuscador").prop("checked", "");
                response.VisibleHome ? $("#chkVisibleHome").prop("checked", "checked") : $("#chkVisibleHome").prop("checked", "");
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
        $("#lblRegistro").html("Actualizar Categoría");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });

    }
}

function ActualizarBloque(categoriaTraduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = $("#hdnCategoriaId").val();
    actualizarBloque.TraduccionId = categoriaTraduccionId;
    actualizarBloque.ImagenId = $("#hdnImagenId").val();
    actualizarBloque.ImagenTraduccionId = $("#hdnImagenTraduccionId").val();

    $.ajax({
        url: `${baseURL}Categoria/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                ListarBloques();
            } else {
                alert("No se pudo actualizar la Categoría", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar la Categoría", "ERROR");
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
                    url: `${baseURL}Categoria/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó la Categoria", "CHECK");
                        } else {
                            alert("No se pudo eliminar la Categoría", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarBloques();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar la Categoría", "ERROR");
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