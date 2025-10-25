$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarBloques();
    ListarIdiomas();
    ListarCategorias();

    $("#fileImagenDesktop").change(function (e) {
        e.preventDefault();

        const file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        const formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}subcategoria/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenDesktop").attr("src", `${baseURL}files/subcategoria/${response.file}`);
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
            url: `${baseURL}subcategoria/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenMovil").attr("src", `${baseURL}files/subcategoria/${response.file}`);
                    $("#txtImagenMovil").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#fileImagenBloqueCategoria").change(function (e) {
        e.preventDefault();

        const file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        const formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}subcategoria/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenBloqueCategoria").attr("src", `${baseURL}files/subcategoria/${response.file}`);
                    $("#txtImagenBloqueCategoria").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
            }
        });
    });

    $("#txtDescripcionBloqueCategoria").ckeditor({
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
        $("#lblRegistro").html("Crear Subcategoría");
    });

    $btnGuardarBloque.click(function () {
        if ($("#hdnSubcategoriaTraduccionId").val() === "") {
            RegistrarBloque();
        } else if ($("#hdnSubcategoriaId").val() !== "" && $btnGuardarBloque.val() === "Idioma") {
            RegistrarIdioma();
        } else {
            ActualizarBloque($("#hdnSubcategoriaTraduccionId").val());
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
        const vSubcategoriaId = $("#hdnSubcategoriaId").val().trim();
        const vIdiomaId = $("#ddlIdioma").val();
        if (vSubcategoriaId !== "") {
            $("#txtNombre, #txtSubtitulo, #txtTituloBloqueCategoria, #txtDescripcionBloqueCategoria, #txtNombreBotonBloqueCategoria, #txtLinkExterno, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords").val("");
            $("#txtOrdenBloqueCategoria").val(0);
            $("#chkActivo, #chkVisibleBuscador").prop("checked", "checked");

            SeleccionarBloquePorIdioma(vSubcategoriaId, vIdiomaId);
        }
    });
    $('#ddlIdioma').trigger('change');

    $('#ddlCategoria').change(function () {
        ListarSubCategorias($(this).val(), 0);
    });
    $('#ddlCategoria').trigger('change');
});

var baseURL = $("base").attr("href");

function LimpiarPopupBloque() {
    $("#txtNombre, #txtSubtitulo, #txtTituloBloqueCategoria, #txtDescripcionBloqueCategoria, #txtNombreBotonBloqueCategoria, #txtLinkExterno, #txtTituloSEO, #txtDescripcionSEO, #txtKeyWords, #hdnSubcategoriaId, #hdnSubcategoriaTraduccionId").val("");
    $("#ddlIdioma, #ddlCategoria, #ddlSeccionPadre, #txtOrdenBloqueCategoria").val("0");
    $("#chkActivo, #chkVisibleBuscador").prop("checked", "checked");
    $("#hdnImagenId, #hdnImagenTraduccionId, #txtImagenDesktop, #txtImagenMovil, #txtImagenTextoAlternativo").val("");
    $("#hdnImagenBloqueCategoriaId, #hdnImagenBloqueCategoriaTraduccionId, #txtImagenBloqueCategoria, #txtImagenBloqueCategoriaTextoAlternativo").val("");
    $("#imgImagenDesktop, #imgImagenMovil, #imgImagenBloqueCategoria").attr("src", "");
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
        "ajax": `${baseURL}Subcategoria/listar`,
        "deferRender": true,
        "columns": [
            { "data": "TraduccionId" },
            { "data": "CategoriaNombre" },
            { "data": "IdiomaNombre" },
            { "data": "Nombre" },
            { "data": "Activo" },
            { "data": "VisibleBuscador" },
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
            data.VisibleBuscador === true ? $("td", row).eq(5).html("Si") : $("td", row).eq(5).html("No");
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

    let visibleBuscador, activo;

    const idiomaId = $("#ddlIdioma").val();
    const categoriaId = $("#ddlCategoria").val();
    const idPadre = $("#ddlSeccionPadre").val();
    const nombre = $("#txtNombre").val().trim();
    const subtitulo = $("#txtSubtitulo").val().trim();
    const tituloBloqueCategoria = $("#txtTituloBloqueCategoria").val().trim();
    const descripcionBloqueCategoria = $("#txtDescripcionBloqueCategoria").val();
    const nombreBotonBloqueCategoria = $("#txtNombreBotonBloqueCategoria").val().trim();
    const linkExterno = $("#txtLinkExterno").val().trim();
    const ordenBloqueCategoria = $("#txtOrdenBloqueCategoria").val();
    const tituloSeo = $("#txtTituloSEO").val().trim();
    const descripcionSeo = $("#txtDescripcionSEO").val().trim();
    const keywords = $("#txtKeyWords").val().trim();

    $("#chkActivo").prop("checked") ? activo = true : activo = false;
    $("#chkVisibleBuscador").prop("checked") ? visibleBuscador = true : visibleBuscador = false;

    /*Galeria*/
    const rutaDesktop = $("#txtImagenDesktop").val();
    const rutaMovil = $("#txtImagenMovil").val();
    const textoAlternativo = $("#txtImagenTextoAlternativo").val();
    const rutaBloqueCategoria = $("#txtImagenBloqueCategoria").val();
    const textoAlternativoBloqueCategoria = $("#txtImagenBloqueCategoriaTextoAlternativo").val();

    return {
        IdiomaId: idiomaId,
        CategoriaId: categoriaId,
        IdPadre: idPadre,
        Nombre: nombre,
        Subtitulo: subtitulo,
        TituloBloqueCategoria: tituloBloqueCategoria,
        DescripcionBloqueCategoria: descripcionBloqueCategoria,
        NombreBotonBloqueCategoria: nombreBotonBloqueCategoria,
        LinkExterno: linkExterno,
        OrdenBloqueCategoria: ordenBloqueCategoria,
        TituloSeo: tituloSeo,
        DescripcionSeo: descripcionSeo,
        Keywords: keywords,
        Activo: activo,
        VisibleBuscador: visibleBuscador,
        ImagenDesktop: rutaDesktop,
        ImagenMovil: rutaMovil,
        ImagenTextoAlternativo: textoAlternativo,
        ImagenBloqueCategoria: rutaBloqueCategoria,
        ImagenBloqueCategoriaTextoAlternativo: textoAlternativoBloqueCategoria
    };
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    $.ajax({
        url: `${baseURL}Subcategoria/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar la Subcategoria", "WARNING");
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar la Subcategoria", "ERROR");
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

function ListarCategorias() {
    $.ajax({
        url: `${baseURL}categoria/ListarParaSelect`,
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                response.data.forEach(function (value) {
                    $("#ddlCategoria").append(`<option value="${value.Id}">${value.Nombre}</option>`);
                });
            }
        }
    });
}

function ListarSubCategorias(vCategoriaId, vIdPadre) {

    const bloqueSeleccionado = {
        categoriaId: vCategoriaId
    };

    $.ajax({
        url: `${baseURL}subcategoria/listarporcategoria`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#ddlSeccionPadre").html(`<option value="0" selected="selected" class="option-disabled">--Seleccione Sección Padre--</option>`);
                response.data.forEach(function (value) {
                    $("#ddlSeccionPadre").append(`<option value="${value.Id}">${value.Nombre}</option>`);
                });
                if (vIdPadre > 0) {
                    $("#ddlSeccionPadre").val(vIdPadre);
                }
            }
        }
    });
}

function SeleccionarBloque(SubcategoriaTraduccionId) {
    ShowLoader();

    $("#hdnSubcategoriaTraduccionId").val(SubcategoriaTraduccionId);

    const bloqueSeleccionado = {
        id: SubcategoriaTraduccionId,
        idiomaId: 0
    };

    $.ajax({
        url: `${baseURL}Subcategoria/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnSubcategoriaId").val(response.Id);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlCategoria").val(response.CategoriaId);
                $("#ddlSeccionPadre").val(response.IdPadre);
                $("#txtNombre").val(response.Nombre);
                $("#txtSubtitulo").val(response.Subtitulo);
                $("#txtTituloBloqueCategoria").val(response.TituloBloqueCategoria);
                $("#txtDescripcionBloqueCategoria").val(response.DescripcionBloqueCategoria);
                $("#txtNombreBotonBloqueCategoria").val(response.NombreBotonBloqueCategoria);
                $("#txtLinkExterno").val(response.LinkExterno);
                $("#txtOrdenBloqueCategoria").val(response.OrdenBloqueCategoria);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtKeyWords").val(response.Keywords);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");
                response.VisibleBuscador ? $("#chkVisibleBuscador").prop("checked", "checked") : $("#chkVisibleBuscador").prop("checked", "");
                
                ListarGaleriaSubcategoria(response.IdiomaId, response.Id);
                ListarSubCategorias($("#ddlCategoria").val(), response.IdPadre);
            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function ListarGaleriaSubcategoria(idiomaId, subcategoriaId) {
    $.ajax({
        url: `${baseURL}subcategoria/galeria`,
        data: JSON.stringify({ idioma: idiomaId, tipo: 'Subcategoria', id: subcategoriaId}),
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
                            $("#imgImagenDesktop").attr("src", `${baseURL}files/subcategoria/${item.RutaDekstop}`);
                            $("#txtImagenMovil").val(item.RutaMovil);
                            $("#imgImagenMovil").attr("src", `${baseURL}files/subcategoria/${item.RutaMovil}`);
                            $("#txtImagenTextoAlternativo").val(item.TextoAlternativo);
                        } else if (item.Uso === "bloque") {
                            $("#hdnImagenBloqueCategoriaId").val(item.Id);
                            $("#hdnImagenBloqueCategoriaTraduccionId").val(item.TraduccionId);
                            $("#txtImagenBloqueCategoria").val(item.RutaDekstop);
                            $("#imgImagenBloqueCategoria").attr("src", `${baseURL}files/subcategoria/${item.RutaDekstop}`);
                            $("#txtImagenBloqueCategoriaTextoAlternativo").val(item.TextoAlternativo);
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

function SeleccionarBloquePorIdioma(SubcategoriaId, idioma) {
    ShowLoader();

    $("#hdnSubcategoriaId").val(SubcategoriaId);

    const bloqueSeleccionado = {
        id: SubcategoriaId,
        idiomaId: idioma
    };

    $.ajax({
        url: `${baseURL}Subcategoria/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnSubcategoriaTraduccionId").val(response.TraduccionId);
                $("#ddlIdioma").val(response.IdiomaId);
                $("#ddlCategoria").val(response.CategoriaId);
                $("#ddlSeccionPadre").val(response.IdPadre);
                $("#txtNombre").val(response.Nombre);
                $("#txtSubtitulo").val(response.Subtitulo);
                $("#txtTituloBloqueCategoria").val(response.TituloBloqueCategoria);
                $("#txtDescripcionBloqueCategoria").val(response.DescripcionBloqueCategoria);
                $("#txtNombreBotonBloqueCategoria").val(response.NombreBotonBloqueCategoria);
                $("#txtLinkExterno").val(response.LinkExterno);
                $("#txtOrdenBloqueCategoria").val(response.OrdenBloqueCategoria);
                $("#txtTituloSEO").val(response.TituloSeo);
                $("#txtDescripcionSEO").val(response.DescripcionSeo);
                $("#txtKeyWords").val(response.Keywords);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");
                response.VisibleBuscador ? $("#chkVisibleBuscador").prop("checked", "checked") : $("#chkVisibleBuscador").prop("checked", "");

                ListarSubCategorias($("#ddlCategoria").val(), response.IdPadre);
            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function EditarBloque(videoId) {
    LimpiarPopupBloque();
    
    if (videoId !== null && videoId !== "") {

        SeleccionarBloque(videoId);
        $("#btnGuardar").val("Actualizar");
        $("#lblRegistro").html("Actualizar Subcategoría");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
        
    }
}

function ActualizarBloque(SubcategoriaTraduccionId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = $("#hdnSubcategoriaId").val();
    actualizarBloque.TraduccionId = SubcategoriaTraduccionId;
    actualizarBloque.ImagenId = $("#hdnImagenId").val();
    actualizarBloque.ImagenTraduccionId = $("#hdnImagenTraduccionId").val();
    actualizarBloque.ImagenBloqueCategoriaId = $("#hdnImagenBloqueCategoriaId").val();
    actualizarBloque.ImagenBloqueCategoriaTraduccionId = $("#hdnImagenBloqueCategoriaTraduccionId").val();

    $.ajax({
        url: `${baseURL}Subcategoria/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                ListarBloques();
            } else {
                alert("No se pudo actualizar la Subcategoria", "WARNING");
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar la Subcategoria", "ERROR");
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
                    url: `${baseURL}Subcategoria/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó el Subcategoria", "CHECK");
                        } else {
                            alert("No se pudo eliminar la Subcategoria", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarBloques();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar la Subcategoria", "ERROR");
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