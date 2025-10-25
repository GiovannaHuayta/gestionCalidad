$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarBloques();
    ListarIdiomas();
    ListarCampanias();

    $("#fileImagenDesktop").change(function (e) {
        e.preventDefault();

        const file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        const formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}galeriacampania/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenDesktop").attr("src", `${baseURL}files/galeria/${response.file}`);
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
            url: `${baseURL}galeriacampania/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenMovil").attr("src", `${baseURL}files/galeria/${response.file}`);
                    $("#txtImagenMovil").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    const $btnNuevoBloque = $("#btnNuevo");
    const $btnCancelar = $("#btnCancelar");
    const $btnGuardarBloque = $("#btnGuardar");

    $(document).keyup(function (event) {
        if (event.which === 27) {
            $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
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
        $("#lblRegistro").html("Crear Galería");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnImagenId").val() === "") {
            RegistrarBloque();
        } else if ($("#hdnImagenId").val() !== "" && $btnGuardarBloque.val() === "Idioma") {
            RegistrarIdioma();
        } else {
            ActualizarBloque($("#hdnImagenId").val());
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

    $("#ddlCampania").change(function () {
        const vCampaniaId = $("#hdnImagenId").val();
        const vIdiomaId = $("#ddlIdioma").val();
        if (vCampaniaId !== "") {
            $("#txtNombre, #txtTitulo, #txtSubtitulo, #txtDescripcion, #txtBloqueHTML, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords").val("");
            $("#chkActivo, #chkVisibleBuscador, #chkVisibleHome").prop("checked", "checked");
            SeleccionarBloquePorIdioma(vCampaniaId, vIdiomaId);
        }
    });
    $("#ddlCampania").trigger("change");
});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtCodigoYoutube, #hdnCampaniaId, #hdnImagenId, #hdnImagenTraduccionId, #txtImagenDesktop, #txtImagenTextoAlternativo").val("");
    $("#ddlIdioma, #ddlCampania").val("0");
    $("#imgImagenDesktop").attr("src", "");
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
        "ajax": `${baseURL}galeriacampania/listar`,
        "deferRender": true,
        "columns": [
            { "data": "Id" },
            { "data": "IdiomaId" },
            { "data": "CampaniaNombre" },
            { "data": "CodigoYoutube" },
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
            data.IdiomaId === 1 ? $("td", row).eq(1).html("Español") : $("td", row).eq(1).html("Inglés");
        }
    });

    // Editar agenda
    $("#gridBloques tbody").on("click",
        "#EditarBloque",
        function () {
            const data = table.row($(this).parents("tr")).data();
            EditarBloque(data.Id, data.IdiomaId);
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

    const idiomaId = $("#ddlIdioma").val();
    const entidadId = $("#ddlCampania").val();
    const codigoYoutube = $("#txtCodigoYoutube").val().trim();

    /*Galeria*/
    const entidadTipo = "Campania";
    const rutaDesktop = $("#txtImagenDesktop").val();
    const uso = "galeria";
    const textoAlternativo = $("#txtImagenTextoAlternativo").val();

    return {
        IdiomaId: idiomaId,
        EntidadId: entidadId,
        RutaDekstop: rutaDesktop,
        EntidadTipo: entidadTipo,
        CodigoYoutube: codigoYoutube,
        Uso: uso,
        TextoAlternativo: textoAlternativo
    };
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    $.ajax({
        url: `${baseURL}galeriacampania/insertar`,
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

function SeleccionarBloque(galeriaId, idiomaId) {
    ShowLoader();

    $("#hdnImagenId").val(galeriaId);

    const bloqueSeleccionado = {
        id: galeriaId,
        idiomaId: idiomaId
    };

    $.ajax({
        url: `${baseURL}galeriacampania/seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#ddlCampania").val(response.CampaniaId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#txtCodigoYoutube").val(response.CodigoYoutube);

                /* Galeria */
                $("#hdnImagenId").val(response.Id);
                $("#hdnImagenTraduccionId").val(response.TraduccionId);
                $("#hdnCampaniaId").val(response.CampaniaId);
                $("#txtImagenDesktop").val(response.RutaDekstop);
                $("#imgImagenDesktop").attr("src", `${baseURL}files/galeria/${response.RutaDekstop}`);
                $("#txtImagenTextoAlternativo").val(response.TextoAlternativo);
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
        url: `${baseURL}galeriacampania/Seleccionar`,
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

function EditarBloque(galeriaId, idiomaId) {
    if (galeriaId !== null && galeriaId !== "") {
        SeleccionarBloque(galeriaId, idiomaId);
        $("#btnGuardar").val("Actualizar");
        $("#lblRegistro").html("Actualizar Galería");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });

    }
}

function ActualizarBloque(categoriaTraduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = $("#hdnImagenId").val();
    actualizarBloque.TraduccionId = $("#hdnImagenTraduccionId").val();

    $.ajax({
        url: `${baseURL}galeriacampania/actualizar`,
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
                    url: `${baseURL}galeriacampania/eliminar`,
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

function ListarCampanias() {
    $.ajax({
        url: `${baseURL}campania/listar`,
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                response.data.forEach(function (value) {
                    $("#ddlCampania").append(`<option value="${value.Id}">${value.Nombre}</option>`);
                });
            }
        }
    });
}