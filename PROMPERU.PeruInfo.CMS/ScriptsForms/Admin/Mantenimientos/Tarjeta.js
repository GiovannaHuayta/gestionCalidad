$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarBloques();
    ListarIdiomas();
    ListarSubCategorias();

    $("#fileImagenDesktop").change(function (e) {
        e.preventDefault();

        const file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        const formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}tarjeta/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenDesktop").attr("src", `${baseURL}files/tarjeta/${response.file}`);
                    $("#txtImagenDesktop").val(response.file);
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
        $("#lblRegistro").html("Crear Tarjeta de subcategorías");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnTarjetaTraduccionId").val() === "") {
            RegistrarBloque();
        } else if ($("#hdnTarjetaId").val() !== "" && $btnGuardarBloque.val() === "Idioma") {
            RegistrarIdioma();
        } else {
            ActualizarBloque($("#hdnTarjetaTraduccionId").val());
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
        const vTarjetaId = $("#hdnTarjetaId").val().trim();
        const vIdiomaId = $("#ddlIdioma").val();
        if (vTarjetaId !== "") {
            $("#txtTitulo, #txtDescripcion, #txtNombreBoton, #txtLink").val("");
            $("#txtOrden").val(0);
            $("#chkActivo, #chkVentanaNueva").prop("checked", "checked");

            SeleccionarBloquePorIdioma(vTarjetaId, vIdiomaId);
        }
    });
    $('#ddlIdioma').trigger('change');
});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtTitulo, #txtDescripcion, #txtNombreBoton, #txtLink, #hdnTarjetaId, #hdnTarjetaTraduccionId").val("");
    $("#ddlIdioma, #ddlSubcategoria, #txtOrden").val("0");
    $("#chkVentanaNueva, #chkActivo").prop("checked", "checked");
    $("#hdnImagenId, #hdnImagenTraduccionId, #txtImagenDesktop, #txtImagenTextoAlternativo, #fileImagenDesktop").val("");
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
        /*        "order": [[6, "desc"]], // Sort by first column descending*/
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": `${baseURL}Tarjeta/listar`,
        "deferRender": true,
        "columns": [
            { "data": "TraduccionId" },
            { "data": "SubcategoriaNombre" },
            { "data": "IdiomaNombre" },
            { "data": "Titulo" },
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

    let ventanaNueva, activo;

    const idiomaId = $("#ddlIdioma").val();
    const subCategoriaId = $("#ddlSubcategoria").val();
    const titulo = $("#txtTitulo").val().trim();
    const descripcion = $("#txtDescripcion").val().trim();
    const nombreBoton = $("#txtNombreBoton").val().trim();
    const link = $("#txtLink").val().trim();
    const orden = $("#txtOrden").val();

    $("#chkVentanaNueva").prop("checked") ? ventanaNueva = true : ventanaNueva = false;
    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    /*Galeria*/
    const rutaDesktop = $("#txtImagenDesktop").val();
    const textoAlternativo = $("#txtImagenTextoAlternativo").val();

    return {
        IdiomaId: idiomaId,
        SubcategoriaId: subCategoriaId,
        Titulo: titulo,
        Descripcion: descripcion,
        NombreBoton: nombreBoton,
        Link: link,
        Orden: orden,
        VentanaNueva: ventanaNueva,
        Activo: activo,
        ImagenDesktop: rutaDesktop,
        ImagenTextoAlternativo: textoAlternativo
    };
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    $.ajax({
        url: `${baseURL}Tarjeta/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar la Tarjeta", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar la Tarjeta", "ERROR");
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

function SeleccionarBloque(TarjetaTraduccionId) {
    ShowLoader();

    $("#hdnTarjetaTraduccionId").val(TarjetaTraduccionId);

    const bloqueSeleccionado = {
        id: TarjetaTraduccionId,
        idiomaId: 0
    };

    $.ajax({
        url: `${baseURL}Tarjeta/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnTarjetaId").val(response.Id);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlSubcategoria").val(response.SubcategoriaId);
                $("#txtTitulo").val(response.Titulo);
                $("#txtDescripcion").val(response.Descripcion);
                $("#txtNombreBoton").val(response.NombreBoton);
                $("#txtLink").val(response.Link);
                $("#txtOrden").val(response.Orden);

                response.VentanaNueva ? $("#chkVentanaNueva").prop("checked", "checked") : $("#chkVentanaNueva").prop("checked", "");
                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");

                ListarGaleriaSubcategoria(response.IdiomaId, response.Id);
            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function SeleccionarBloquePorIdioma(TarjetaId, idioma) {
    ShowLoader();

    $("#hdnTarjetaId").val(TarjetaId);

    const bloqueSeleccionado = {
        id: TarjetaId,
        idiomaId: idioma
    };

    $.ajax({
        url: `${baseURL}Tarjeta/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnTarjetaTraduccionId").val(response.TraduccionId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlSubcategoria").val(response.SubcategoriaId);
                $("#txtTitulo").val(response.Titulo);
                $("#txtDescripcion").val(response.Descripcion);
                $("#txtNombreBoton").val(response.NombreBoton);
                $("#txtLink").val(response.Link);
                $("#txtOrden").val(response.Orden);

                response.VentanaNueva ? $("#chkVentanaNueva").prop("checked", "checked") : $("#chkVentanaNueva").prop("checked", "");
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
        $("#lblRegistro").html("Actualizar Tarjeta de subcategorías");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
        
    }
}

function ListarGaleriaSubcategoria(idiomaId, tarjetaId) {
    $.ajax({
        url: `${baseURL}tarjeta/galeria`,
        data: JSON.stringify({ idioma: idiomaId, tipo: 'Tarjeta', id: tarjetaId}),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                if (response.data.length > 0) {
                    response.data.forEach(item => {
                        console.log(item);
                        if (item.Uso === "portada") {
                            $("#hdnImagenId").val(item.Id);
                            $("#hdnImagenTraduccionId").val(item.TraduccionId);
                            $("#txtImagenDesktop").val(item.RutaDekstop);
                            $("#imgImagenDesktop").attr("src", `${baseURL}files/tarjeta/${item.RutaDekstop}`);
                            $("#txtImagenTextoAlternativo").val(item.TextoAlternativo);
                        }
                    })
                }
            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function ActualizarBloque(TarjetaTraduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = $("#hdnTarjetaId").val();
    actualizarBloque.TraduccionId = TarjetaTraduccionId;
    actualizarBloque.ImagenId = $("#hdnImagenId").val();
    actualizarBloque.ImagenTraduccionId = $("#hdnImagenTraduccionId").val();

    $.ajax({
        url: `${baseURL}Tarjeta/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                ListarBloques();
            } else {
                alert("No se pudo actualizar la Tarjeta", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar la Tarjeta", "ERROR");
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
                    url: `${baseURL}Tarjeta/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó la Tarjeta", "CHECK");
                        } else {
                            alert("No se pudo eliminar la Tarjeta", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarBloques();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar la Tarjeta", "ERROR");
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
    var maxLength = 400;
    var strLength = obj.value.length;
    var charRemain = (maxLength - strLength);

    if (charRemain < 0) {
        document.getElementById("charNum").innerHTML = '<span style="color: red;">Ha excedido el límite de ' + maxLength + ' caracteres</span>';
    } else {
        document.getElementById("charNum").innerHTML = charRemain + ' caracteres restantes';
    }
}

function countCharsSEO(obj) {
    var maxLength = 400;
    var strLength = obj.value.length;
    var charRemain = (maxLength - strLength);

    if (charRemain < 0) {
        document.getElementById("charNumSEO").innerHTML = '<span style="color: red;">Ha excedido el límite de ' + maxLength + ' caracteres</span>';
    } else {
        document.getElementById("charNumSEO").innerHTML = charRemain + ' caracteres restantes';
    }
}