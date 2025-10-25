$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarBloques();
    ListarIdiomas();
    ListarTipo();
    ListarSubCategorias();

    $("#fileImagen").change(function (e) {
        e.preventDefault();

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}Persona/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagen").attr("src", `${baseURL}files/Persona/${response.file}`);
                    $("#txtImagen").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#fileImagen2").change(function (e) {
        e.preventDefault();

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}Persona/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagen2").attr("src", `${baseURL}files/Persona/${response.file}`);
                    $("#txtImagen2").val(response.file);
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
            url: `${baseURL}Persona/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagen3").attr("src", `${baseURL}files/Persona/${response.file}`);
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
        $("#lblRegistro").html("Crear Embajadores / Amigos del Perú");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnPersonaTraduccionId").val() === "") {
            RegistrarBloque();
        } else if ($("#hdnPersonaId").val() !== "" && $btnGuardarBloque.val() === "Idioma") {
            RegistrarIdioma();
        } else {
            ActualizarBloque($("#hdnPersonaTraduccionId").val());
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
        const vPersonaId = $("#hdnPersonaId").val().trim();
        const vIdiomaId = $("#ddlIdioma").val();
        if (vPersonaId !== "") {
            $("#txtResumen, #txtDetalle, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords").val("");

            SeleccionarBloquePorIdioma(vPersonaId, vIdiomaId);
        }
    });
    $('#ddlIdioma').trigger('change');

});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtNombre, #txtAltImagen, #txtFacebook, #txtTwitter, #txtInstagram, #txtYouTube, #txtResumen, #txtDetalle, #txtTituloSEO, #txtKeyWords, #txtDescripcionSEO, #hdnPersonaId, #hdnPersonaTraduccionId").val("");
    $("#ddlIdioma, #ddlSubcategoria, #txtAnio, #ddlTipo").val("0");
    $("#fileImagen, #txtImagen, #fileImagen2, #txtImagen2, #fileImagen3, #txtImagen3").val("");
    $("#imgImagen, #imgImagen2, #imgImagen3").attr("src", "");
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
/*        "order": [[6, "desc"]], // Sort by first column descending*/
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": `${baseURL}Persona/listar`,
        "deferRender": true,
        "columns": [
            { "data": "TraduccionId" },
            { "data": "SubcategoriaNombre" },
            { "data": "Nombre" },
            { "data": "Anio" },
            { "data": "Activo" },
            { "data": "IdiomaNombre" },
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
    const tipo = $("#ddlTipo").val();
    const imagen = $("#txtImagen").val().trim();
    const imagen2 = $("#txtImagen2").val().trim();
    const imagen3 = $("#txtImagen3").val().trim();
    const nombre = $("#txtNombre").val().trim();
    const anio = $("#txtAnio").val();
    const altImagen = $("#txtAltImagen").val().trim();
    const facebook = $("#txtFacebook").val().trim();
    const twitter = $("#txtTwitter").val().trim();
    const instagram = $("#txtInstagram").val().trim();
    const youTube = $("#txtYouTube").val().trim();
    const resumen = $("#txtResumen").val().trim();
    const detalle = $("#txtDetalle").val().trim();
    const tituloSEO = $("#txtTituloSEO").val().trim();
    const keyWords = $("#txtKeyWords").val().trim();
    const descripcionSEO = $("#txtDescripcionSEO").val().trim();

    $("#chkDestacado").prop("checked") ? destacado = true : destacado = false;
    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    const bloqueObj = {
        IdiomaId: idiomaId,
        SubcategoriaId: subCategoriaId,
        Tipo: tipo,
        Imagen: imagen,
        Imagen2: imagen2,
        Imagen3: imagen3,
        Nombre: nombre,
        Anio: anio,
        AltImagen: altImagen,
        Facebook: facebook,
        Twitter: twitter,
        Instagram: instagram,
        Youtube: youTube,
        Resumen: resumen,
        Detalle: detalle,
        TituloSeo: tituloSEO,
        Keywords: keyWords,
        DescripcionSeo: descripcionSEO,
        Destacado: destacado,
        Activo: activo
    };

    return bloqueObj;
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    $.ajax({
        url: `${baseURL}Persona/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar la Persona", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar la Persona", "ERROR");
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

function ListarTipo() {
    $.ajax({
        url: `${baseURL}personaTipo/listar`,
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

function SeleccionarBloque(PersonaTraduccionId) {
    ShowLoader();

    $("#hdnPersonaTraduccionId").val(PersonaTraduccionId);

    const bloqueSeleccionado = {
        id: PersonaTraduccionId,
        idiomaId: 0
    };

    $.ajax({
        url: `${baseURL}Persona/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnPersonaId").val(response.Id);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlSubcategoria").val(response.SubcategoriaId);
                $("#ddlTipo").val(response.Tipo);
                $("#txtImagen").val(response.Imagen);
                $("#imgImagen").attr("src", `${baseURL}files/persona/${response.Imagen}`);
                $("#txtImagen2").val(response.Imagen2);
                $("#imgImagen2").attr("src", `${baseURL}files/persona/${response.Imagen2}`);
                $("#txtImagen3").val(response.Imagen3);
                $("#imgImagen3").attr("src", `${baseURL}files/persona/${response.Imagen3}`);
                $("#txtNombre").val(response.Nombre);
                $("#txtAnio").val(response.Anio);
                $("#txtAltImagen").val(response.AltImagen);
                $("#txtFacebook").val(response.Facebook);
                $("#txtTwitter").val(response.Twitter);
                $("#txtInstagram").val(response.Instagram);
                $("#txtYouTube").val(response.Youtube);
                $("#txtResumen").val(response.Resumen);
                $("#txtDetalle").val(response.Detalle);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtKeyWords").val(response.Keywords);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);

                response.Destacado ? $("#chkDestacado").prop("checked", "checked") : $("#chkDestacado").prop("checked", "");
                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");

            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function SeleccionarBloquePorIdioma(PersonaId, idioma) {
    ShowLoader();

    $("#hdnPersonaId").val(PersonaId);

    const bloqueSeleccionado = {
        id: PersonaId,
        idiomaId: idioma
    };

    $.ajax({
        url: `${baseURL}Persona/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnPersonaTraduccionId").val(response.TraduccionId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlSubcategoria").val(response.SubcategoriaId);
                $("#ddlTipo").val(response.Tipo);
                $("#txtImagen").val(response.Imagen);
                $("#imgImagen").attr("src", `${baseURL}files/persona/${response.Imagen}`);
                $("#txtImagen2").val(response.Imagen2);
                $("#imgImagen2").attr("src", `${baseURL}files/persona/${response.Imagen2}`);
                $("#txtImagen3").val(response.Imagen3);
                $("#imgImagen3").attr("src", `${baseURL}files/persona/${response.Imagen3}`);
                $("#txtNombre").val(response.Nombre);
                $("#txtAnio").val(response.Anio);
                $("#txtAltImagen").val(response.AltImagen);
                $("#txtFacebook").val(response.Facebook);
                $("#txtTwitter").val(response.Twitter);
                $("#txtInstagram").val(response.Instagram);
                $("#txtYouTube").val(response.Youtube);
                $("#txtResumen").val(response.Resumen);
                $("#txtDetalle").val(response.Detalle);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtKeyWords").val(response.Keywords);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
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
        $("#lblRegistro").html("Actualizar Embajadores / Amigos del Perú");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
        
    }
}

function ActualizarBloque(PersonaTraduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = $("#hdnPersonaId").val();
    actualizarBloque.TraduccionId = PersonaTraduccionId;

    $.ajax({
        url: `${baseURL}Persona/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                ListarBloques();
            } else {
                alert("No se pudo actualizar la Persona", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar la Persona", "ERROR");
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
                    url: `${baseURL}Persona/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó el Persona", "CHECK");
                        } else {
                            alert("No se pudo eliminar la Persona", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarBloques();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar la Persona", "ERROR");
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