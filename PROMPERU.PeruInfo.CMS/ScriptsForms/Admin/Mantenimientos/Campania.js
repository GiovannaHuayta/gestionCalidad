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
            url: `${baseURL}campania/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenDesktop").attr("src", `${baseURL}files/campania/${response.file}`);
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
            url: `${baseURL}campania/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenMovil").attr("src", `${baseURL}files/campania/${response.file}`);
                    $("#txtImagenMovil").val(response.file);
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
        $("#lblRegistro").html("Crear Campaña");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnCampaniaTraduccionId").val() === "") {
            RegistrarBloque();
        } else if ($("#hdnCampaniaId").val() !== "" && $btnGuardarBloque.val() === "Idioma") {
            RegistrarIdioma();
        } else {
            ActualizarBloque($("#hdnCampaniaTraduccionId").val());
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
        const vCampaniaId = $("#hdnCampaniaId").val().trim();
        const vIdiomaId = $("#ddlIdioma").val();
        if (vCampaniaId !== "") {
            $("#txtNombre, #txtPublico, #txtUbicacion, #txtDescripcion, #txtLink, #txtDescripcionSEO, #txtTituloSEO, #txtKeyWords, #hdnCategoriaId, #hdnCategoriaTraduccionId, #txtImagenDesktop, #txtImagenMovil, #txtImagenTextoAlternativo").val("");
            $("#txtAnio").val(0);
            $("#imgImagenDesktop, #imgImagenMovil").attr("src", "");
            SeleccionarBloquePorIdioma(vCampaniaId, vIdiomaId);
        }
    });
    $('#ddlIdioma').trigger('change');
});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtNombre, #txtPublico, #txtUbicacion, #txtDescripcion, #txtLink, #txtDescripcionSEO, #txtTituloSEO, #txtKeyWords, #hdnCampaniaId, #hdnCampaniaTraduccionId, #hdnCategoriaId, #hdnCategoriaTraduccionId, #txtImagenDesktop, #txtImagenMovil, #txtImagenTextoAlternativo").val("");
    $("#ddlIdioma").val("0");
    $("#chkActivo").prop("checked", "checked");
    $("#imgImagenDesktop, #imgImagenMovil").attr("src", "");
}

function ListarBloques() {
    ShowLoader();

    var urlWeb = $("#txtUrlWeb").val().trim();

    console.log(urlWeb);

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
        "ajax": `${baseURL}Campania/listar`,
        "deferRender": true,
        "columns": [
            { "data": "TraduccionId" },
            { "data": "IdiomaNombre" },
            { "data": "Nombre" },
            { "data": "Anio" },
            { "data": "Publico" },
            { "data": "Slug" },
            { "data": "Activo" },
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
            var idiomaPrefijo = "";
            if (data.IdiomaId === 1) {
                idiomaPrefijo = "es-pe";
            }
            else {
                idiomaPrefijo = "en-us";
            }
            data.Activo === true ? $("td", row).eq(6).html("Si") : $("td", row).eq(6).html("No");
            $("td", row).eq(5).html(`<a href="${urlWeb}${idiomaPrefijo}/marca-peru/detalle-campanas/${data.Id}/${data.Slug}" target="_blank">Ver Detalle</a>`);
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

    var activo;

    const idiomaId = $("#ddlIdioma").val();
    const nombre = $("#txtNombre").val().trim();
    const anio = $("#txtAnio").val();
    const publico = $("#txtPublico").val().trim();
    const ubicacion = $("#txtUbicacion").val().trim();
    const descripcion = $("#txtDescripcion").val().trim();
    const link = $("#txtLink").val().trim();
    const descripcionSeo = $("#txtDescripcionSEO").val().trim();
    const tituloSeo = $("#txtTituloSEO").val().trim();
    const keyWords = $("#txtKeyWords").val().trim();

    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    /*Galeria*/
    const entidadTipo = "Campania";
    const rutaDesktop = $("#txtImagenDesktop").val();
    const rutaMovil = $("#txtImagenMovil").val();
    const uso = "portada";
    const textoAlternativo = $("#txtImagenTextoAlternativo").val();

    const bloqueObj = {
        IdiomaId: idiomaId,
        Nombre: nombre,
        Anio: anio,
        Publico: publico,
        Ubicacion: ubicacion,
        Descripcion: descripcion,
        Link: link,
        DescripcionSeo: descripcionSeo,
        TituloSeo: tituloSeo,
        Keywords: keyWords,
        Activo: activo,
        RutaDekstop: rutaDesktop,
        RutaMovil: rutaMovil,
        EntidadTipo: entidadTipo,
        Uso: uso,
        TextoAlternativo: textoAlternativo
    };

    return bloqueObj;
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    $.ajax({
        url: `${baseURL}Campania/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar el Campania", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar la Campania", "ERROR");
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

function SeleccionarBloque(campaniaTraduccionId) {
    ShowLoader();

    $("#hdnCampaniaTraduccionId").val(campaniaTraduccionId);

    const bloqueSeleccionado = {
        id: campaniaTraduccionId,
        idiomaId: 0
    };

    $.ajax({
        url: `${baseURL}Campania/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnCampaniaId").val(response.Id);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#txtNombre").val(response.Nombre);
                $("#txtAnio").val(response.Anio);
                $("#txtPublico").val(response.Publico);
                $("#txtUbicacion").val(response.Ubicacion);
                $("#txtDescripcion").val(response.Descripcion);
                $("#txtLink").val(response.Link);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtKeyWords").val(response.Keywords);

                /* Galeria */
                $("#hdnImagenId").val(response.ImagenId);
                $("#hdnImagenTraduccionId").val(response.ImagenTraduccionId);
                $("#txtImagenDesktop").val(response.ImagenDesktop);
                $("#imgImagenDesktop").attr("src", `${baseURL}files/campania/${response.ImagenDesktop}`);
                $("#txtImagenMovil").val(response.ImagenMovil);
                $("#imgImagenMovil").attr("src", `${baseURL}files/campania/${response.ImagenMovil}`);
                $("#txtImagenTextoAlternativo").val(response.ImageTextoAlternativo);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");

            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function SeleccionarBloquePorIdioma(campaniaId, idioma) {
    ShowLoader();

    $("#hdnCampaniaId").val(campaniaId);

    const bloqueSeleccionado = {
        id: campaniaId,
        idiomaId: idioma
    };

    $.ajax({
        url: `${baseURL}Campania/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnCampaniaTraduccionId").val(response.TraduccionId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#txtNombre").val(response.Nombre);
                $("#txtAnio").val(response.Anio);
                $("#txtPublico").val(response.Publico);
                $("#txtUbicacion").val(response.Ubicacion);
                $("#txtDescripcion").val(response.Descripcion);
                $("#txtLink").val(response.Link);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtKeyWords").val(response.Keywords);
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
        $("#lblRegistro").html("Actualizar Campaña");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
        
    }
}

function ActualizarBloque(campaniaTraduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = $("#hdnCampaniaId").val();
    actualizarBloque.TraduccionId = campaniaTraduccionId;
    actualizarBloque.ImagenId = $("#hdnImagenId").val();
    actualizarBloque.ImagenTraduccionId = $("#hdnImagenTraduccionId").val();

    $.ajax({
        url: `${baseURL}Campania/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                ListarBloques();
            } else {
                alert("No se pudo actualizar la Campaña", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar la Campaña", "ERROR");
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
                    url: `${baseURL}Campania/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó la Campania", "CHECK");
                        } else {
                            alert("No se pudo eliminar la Campaña", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarBloques();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar la Campaña", "ERROR");
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