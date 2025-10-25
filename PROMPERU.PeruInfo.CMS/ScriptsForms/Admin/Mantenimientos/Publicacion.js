$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarBloques();
    ListarIdiomas();
    ListarTipos();
    ListarSubCategorias();

    $("#fileImagen").change(function (e) {
        e.preventDefault();

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}publicacion/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagen").attr("src", `${baseURL}files/publicacion/${response.file}`);
                    $("#txtImagen").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#fileImagen3").change(function (e) {
        e.preventDefault();

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}publicacion/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagen3").attr("src", `${baseURL}files/publicacion/${response.file}`);
                    $("#txtImagen3").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#fileDescubreImagen").change(function (e) {
        e.preventDefault();

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}publicacion/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgDescubreImagen").attr("src", `${baseURL}files/publicacion/${response.file}`);
                    $("#txtDescubreImagen").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#fileImagenContenido").change(function (e) {
        e.preventDefault();

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}publicacion/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#linkArchivoContenido").text(`${baseURL}files/publicacion/${response.file}`);
                    $("#txtImagenContenido").val(response.file);
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
        $("#lblRegistro").html("Crear Noticias y Blog");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnPublicacionTraduccionId").val() === "") {
            RegistrarBloque();
        } else if ($("#hdnPublicacionId").val() !== "" && $btnGuardarBloque.val() === "Idioma") {
            RegistrarIdioma();
        } else {
            ActualizarBloque($("#hdnPublicacionTraduccionId").val());
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
        const vPublicacionId = $("#hdnPublicacionId").val().trim();
        const vIdiomaId = $("#ddlIdioma").val();
        if (vPublicacionId !== "") {
            $("#txtTitulo, #txtResumen, #txtDetalle, #txtAltImagen, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords, #txtDescubreNombre, #txtDescubreLink").val("");
            $("#fileDescubreImagen, #txtDescubreImagen").val("");
            $("#imgDescubreImagen").attr("src", "");
            $("#chkActivo").prop("checked", "checked");
            $("#txtSlug").val("");

            SeleccionarBloquePorIdioma(vPublicacionId, vIdiomaId);
        }
    });
    $('#ddlIdioma').trigger('change');
});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtFechaPublicacion, #txtTitulo, #txtResumen, #txtDetalle, #txtAltImagen, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords, #txtDescubreNombre, #txtDescubreLink, #txtImagenFuente, #hdnPublicacionId, #hdnPublicacionTraduccionId").val("");
    $("#ddlIdioma, #ddlSubcategoria, #ddlTipo").val("0");
    $("#fileImagen, #txtImagen, #fileImagen3, #txtImagen3, #fileDescubreImagen, #txtDescubreImagen").val("");
    $("#imgImagen, #imgImagen3, #imgDescubreImagen").attr("src", "");
    $("#chkDestacado, #chkActivo").prop("checked", "checked");
    $("#txtSlug").val("");
    $("#txtSlug").css("display", "none");
    $("#lblUrl").css("display", "none");    
}

function ListarBloques() {
    ShowLoader();

    var table = $("#gridBloques").DataTable({
        "responsive": true,
        "destroy": true,
        "paging": true,
        "searching": true,
        // orderCellsTop: true,
        "pagingType": "full_numbers",
        // fixedHeader: true,
        "order": [[4, "desc"]], // Sort by first column descending
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": `${baseURL}Publicacion/listar`,
        "deferRender": true,
        "pageLength": 20,
        "columns": [
            { "data": "TraduccionId" },
            { "data": "SubcategoriaNombre" },
            { "data": "TipoNombre" },
            { "data": "Titulo" },
            { "data": "Destacado" },
            { "data": "FechaPublicacion" },
            { "data": "IdiomaNombre" },
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
        "info": true,
        "createdRow": function (row, data, index) {
            data.Destacado === true ? $("td", row).eq(4).html("Si") : $("td", row).eq(4).html("No");
            data.Activo === true ? $("td", row).eq(7).html("Si") : $("td", row).eq(7).html("No");
/*            $("td", row).eq(1).html(data.SubcategoriaNombre);*/
            $("td", row).eq(5).html(data.FechaPublicacion.split("T")[0]);
            $("td", row).eq(1).html(data.CategoriaNombre + " - " + data.SubcategoriaNombre);
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

    var destacado, activo;

    const idiomaId = $("#ddlIdioma").val();
    const subCategoriaId = $("#ddlSubcategoria").val();
    const tipoId = $("#ddlTipo").val();
    const imagen = $("#txtImagen").val().trim();
    const imagen3 = $("#txtImagen3").val().trim();
    const imagenFuente = $("#txtImagenFuente").val().trim();
    const fechaPublicacion = $("#txtFechaPublicacion").val().trim();
    const titulo = $("#txtTitulo").val().trim();

    const slug = $("#txtSlug").val().trim();

    const resumen = $("#txtResumen").val().trim();
    const detalle = $("#txtDetalle").val().trim();
    const altImagen = $("#txtAltImagen").val().trim();
    const tituloSEO = $("#txtTituloSEO").val().trim();
    const descripcionSEO = $("#txtDescripcionSEO").val().trim();
    const keyWords = $("#txtKeyWords").val().trim();
    const descubreNombre = $("#txtDescubreNombre").val().trim();
    const descubreLink = $("#txtDescubreLink").val().trim();
    const descubreImagen = $("#txtDescubreImagen").val().trim();

    $("#chkDestacado").prop("checked") ? destacado = true : destacado = false;
    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    const bloqueObj = {
        IdiomaId: idiomaId,
        SubcategoriaId: subCategoriaId,
        TipoId: tipoId,
        Imagen: imagen,
        Imagen3: imagen3,
        ImagenFuente: imagenFuente,
        FechaPublicacion: fechaPublicacion,
        Titulo: titulo,

        Slug: slug,

        Resumen: resumen,
        Detalle: detalle,
        AltImagen: altImagen,
        TituloSeo: tituloSEO,
        DescripcionSeo: descripcionSEO,
        Keywords: keyWords,
        DescubreNombre: descubreNombre,
        DescubreLink: descubreLink,
        DescubreImagen: descubreImagen,
        Destacado: destacado,
        Activo: activo
    };

    return bloqueObj;
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    $.ajax({
        url: `${baseURL}Publicacion/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar el Publicacion", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar la Publicacion", "ERROR");
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

function ListarTipos() {
    $.ajax({
        url: `${baseURL}tipo/ListarParaSelect`,
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                response.data.forEach(function (value) {
                    $("#ddlTipo").append(`<option value="${value.Id}">${value.Nombre}</option>`);
                });
            }
        }
    });
}

function ListarSubCategorias() {
    $.ajax({
        url: `${baseURL}subcategoria/ListarParaSelect`,
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                response.data.forEach(function (value) {
                    $("#ddlSubcategoria").append(`<option value="${value.Id}">${value.CategoriaNombre} - ${value.Nombre}</option>`);
                });
            }
        }
    });
}

function SeleccionarBloque(PublicacionTraduccionId) {
    ShowLoader();

    $("#hdnPublicacionTraduccionId").val(PublicacionTraduccionId);

    const bloqueSeleccionado = {
        id: PublicacionTraduccionId,
        idiomaId: 0
    };

    $.ajax({
        url: `${baseURL}Publicacion/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnPublicacionId").val(response.Id);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlSubcategoria").val(response.SubcategoriaId);
                $("#ddlTipo").val(response.TipoId);
                $("#txtImagen").val(response.Imagen);
                $("#imgImagen").attr("src", `${baseURL}files/publicacion/${response.Imagen}`);
                $("#txtImagen3").val(response.Imagen3);
                $("#imgImagen3").attr("src", `${baseURL}files/publicacion/${response.Imagen3}`);
                $("#txtImagenFuente").val(response.ImagenFuente);
                $("#txtDescubreImagen").val(response.DescubreImagen);
                $("#imgDescubreImagen").attr("src", `${baseURL}files/publicacion/${response.DescubreImagen}`);
                $("#txtFechaPublicacion").val(response.FechaPublicacion.split("T")[0]);
                $("#txtTitulo").val(response.Titulo);
                $("#txtResumen").val(response.Resumen);
                $("#txtDetalle").val(response.Detalle);
                $("#txtAltImagen").val(response.AltImagen);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtKeyWords").val(response.Keywords);
                $("#txtDescubreNombre").val(response.DescubreNombre);
                $("#txtDescubreLink").val(response.DescubreLink);
                $("#txtSlug").val(response.Slug);

                $("#txtSlug").css("display", "block");
                $("#lblUrl").css("display", "block");

                response.Destacado ? $("#chkDestacado").prop("checked", "checked") : $("#chkDestacado").prop("checked", "");
                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");

            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function SeleccionarBloquePorIdioma(PublicacionId, idioma) {
    ShowLoader();

    $("#hdnPublicacionId").val(PublicacionId);

    const bloqueSeleccionado = {
        id: PublicacionId,
        idiomaId: idioma
    };

    $.ajax({
        url: `${baseURL}Publicacion/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnPublicacionTraduccionId").val(response.TraduccionId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlSubcategoria").val(response.SubcategoriaId);
                $("#ddlTipo").val(response.TipoId);
                $("#txtImagen").val(response.Imagen);
                $("#imgImagen").attr("src", `${baseURL}files/publicacion/${response.Imagen}`);
                $("#txtImagen3").val(response.Imagen3);
                $("#imgImagen3").attr("src", `${baseURL}files/publicacion/${response.Imagen3}`);
                $("#txtImagenFuente").val(response.ImagenFuente);
                $("#txtDescubreImagen").val(response.DescubreImagen);
                $("#imgDescubreImagen").attr("src", `${baseURL}files/publicacion/${response.DescubreImagen}`);
                $("#txtFechaPublicacion").val(response.FechaPublicacion.split("T")[0]);
                $("#txtTitulo").val(response.Titulo);
                $("#txtResumen").val(response.Resumen);
                $("#txtDetalle").val(response.Detalle);
                $("#txtAltImagen").val(response.AltImagen);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtKeyWords").val(response.Keywords);
                $("#txtDescubreNombre").val(response.DescubreNombre);
                $("#txtDescubreLink").val(response.DescubreLink);
                $("#txtSlug").val(response.Slug);

                $("#txtSlug").css("display", "block");
                $("#lblUrl").css("display", "block");
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
        $("#lblRegistro").html("Actualizar Noticias y Blog");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
        
    }
}

function ActualizarBloque(PublicacionTraduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = $("#hdnPublicacionId").val();
    actualizarBloque.TraduccionId = PublicacionTraduccionId;

    $.ajax({
        url: `${baseURL}Publicacion/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo actualizar el Publicacion", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar el Publicacion", "ERROR");
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
                    url: `${baseURL}Publicacion/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó el Publicacion", "CHECK");
                        } else {
                            alert("No se pudo eliminar el Publicacion", "WARNING");
                        }
                        ListarBloques();
                    },
                    complete: function () {
                        HideLoader();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar el Publicacion", "ERROR");
                    }
                });
            } else {
                return false;
            }

            return false;
        });

    $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
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