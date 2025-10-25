$(document).ready(function () {

    ListarBloques();

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    const $btnNuevoBloque = $("#btnNuevo");
    const $btnCancelar = $("#btnCancelar");
    const $btnGuardarBloque = $("#btnGuardar");

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

    $btnGuardarBloque.click(function () {
        if ($("#hdnGaleriaPeruParisId").val() === "") {
            RegistrarBloque();
        } else {
            ActualizarBloque($("#hdnGaleriaPeruParisId").val());
        }
    });

    $btnCancelar.click(function () {
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
        LimpiarPopupBloque();
        $("#btnGuardar").val("");
        $("#lblRegistro").html("Crear Registro Deportistas");
    });


    $("#fileArchivo").change(function (e) {
        e.preventDefault();
        var baseURL = $("base").attr("href");
        var file = $(this).prop("files")[0];
        var tipoArchivo = $("#ddlTipoArchivo").val(); // Obtener el tipo de archivo seleccionado
        var formData = new FormData();
        

        // Validar que se haya seleccionado un tipo de archivo
        if (tipoArchivo === "0" || tipoArchivo === null || tipoArchivo === undefined) {
            // Mostrar mensaje de error si no se ha seleccionado un tipo de archivo
            alert("Por favor, seleccione un tipo de archivo antes de subir un archivo.");
            $("#fileArchivo").val('');
            return; // Salir de la función para evitar el envío del archivo
        }

        // Verificar el tamaño del archivo (5 MB = 5 * 1024 * 1024 bytes)
        var maxFileSize = 5 * 1024 * 1024; // 5 MB en bytes

        if (file.size > maxFileSize) {
            // Mostrar mensaje de error si el archivo es demasiado grande
            alert("El archivo es demasiado grande. El tamaño máximo permitido es de 5 MB.");
            $("#fileArchivo").val('');
            // Limpiar imagen
            $("#imgArchivo").attr("src", "").hide();

            // Limpiar y ocultar video
            const videoPreview = document.querySelector('#videoPreviewContainer');
            videoPreview.style.setProperty('display', 'none', 'important');
            $("#videoSource").attr("src", ""); // Limpia la fuente del video
            $("#videoArchivo")[0].pause(); // Pausa el video
            $("#videoArchivo")[0].currentTime = 0; // Reinicia el tiempo del video
            return; // Salir de la función para evitar el envío del archivo
        }

        // Validar el tipo de archivo basado en la selección
        var allowedExtensions = [];
        if (tipoArchivo === "imagen") {
            allowedExtensions = ["jpg", "jpeg", "png", "svg", "webp"];
        } else if (tipoArchivo === "video") {
            allowedExtensions = ["mp4"];
        }

        var fileExtension = file.name.split('.').pop().toLowerCase();
        if (!allowedExtensions.includes(fileExtension)) {
            // Mostrar mensaje de error si el archivo no tiene una extensión permitida
            alert(`El tipo de archivo no es permitido para el tipo seleccionado. Extensiones permitidas: ${allowedExtensions.join(", ")}`);
            $("#fileArchivo").val('');
            // Limpiar imagen
            $("#imgArchivo").attr("src", "").hide();

            // Limpiar y ocultar video
            const videoPreview = document.querySelector('#videoPreviewContainer');
            videoPreview.style.setProperty('display', 'none', 'important');
            $("#videoSource").attr("src", ""); // Limpia la fuente del video
            $("#videoArchivo")[0].pause(); // Pausa el video
            $("#videoArchivo")[0].currentTime = 0; // Reinicia el tiempo del video
            return; // Salir de la función para evitar el envío del archivo
        }

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}GaleriaPeruParis/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {               

                if (response.file !== null && response.file !== "") {
                    var fileName = response.file;
                    var fileExtension = fileName.split('.').pop().toLowerCase();
                    const videoPreview = document.querySelector('#videoPreviewContainer');

                    if (fileExtension === 'mp4') {
                        // Mostrar video
                        $("#imgArchivo").hide();
                        videoPreview.style.setProperty('display', 'flex', 'important'); // Use flex or block as needed
                        $("#videoSource").attr("src", `${baseURL}files/GaleriaPeruParis/${fileName}`);
                        $("#videoArchivo")[0].load(); // Reload video to ensure it is displayed
                        $("#txtArchivo").val(fileName);
                    } else {
                        // Mostrar imagen
                        videoPreview.style.setProperty('display', 'none', 'important');
                        $("#imgArchivo").attr("src", `${baseURL}files/GaleriaPeruParis/${fileName}`);
                        $("#imgArchivo").show();
                        $("#txtArchivo").val(fileName);
                    }
                }
            },
            error: function (response) {
                console.error('Error en la solicitud:', response); // Mensaje de depuración
            }
        });
    });


});

var baseURL = $("base").attr("href");

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
        "ajax": `${baseURL}GaleriaPeruParis/listar`,
        "deferRender": true,
        "pageLength": 20,
        "columns": [
            { "data": "Id" },
            { "data": "Titulo" },
            { "data": "Descripcion" },
            { "data": "TipoArchivo" },
            { "data": "NombreArchivo" },
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
            let fileType = data.TipoArchivo;
            let fileName = data.NombreArchivo;
            let fileUrl = `${baseURL}files/GaleriaPeruParis/${fileName}`;
            let htmlContent;

            if (fileType === 'imagen') {
                // Si el archivo es una imagen
                htmlContent = `<img src="${fileUrl}" style="width: 100px; margin: auto; display: block;" alt="">`;
            } else if (fileType === 'video') {
                // Si el archivo es un video
                htmlContent = `<video controls style="width: 100px; margin: auto; object-fit: cover;display: block;">
                                   <source src="${fileUrl}" type="video/mp4">
                                   Su navegador no soporta el elemento <code>video</code>.
                               </video>`;
            } else {
                // En caso de que el tipo de archivo no sea imagen ni video
                htmlContent = `<span>Archivo no compatible</span>`;
            }

            $("td", row).eq(4).html(htmlContent);
            $("td", row).eq(5).html(data.Activo ? "Activo" : "Inactivo");
        },
        "info": true

    });

    // Editar 
    $("#gridBloques tbody").on("click",
        "#EditarBloque",
        function () {
            const data = table.row($(this).parents("tr")).data();
            EditarBloque(data.Id);
        });

    // Eliminar
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

function EditarBloque(id) {
    if (id !== null && id !== "") {
        SeleccionarBloque(id);
        $("#btnGuardar").val("Actualizar");
        $("#lblRegistro").html("Actualizar Registro");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });

    }
}

function SeleccionarBloque(Id) {
    ShowLoader();

    const bloqueSeleccionado = {
        id: Id
    };

    $.ajax({
        url: `${baseURL}GaleriaPeruParis/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnGaleriaPeruParisId").val(response.Id);
                $("#txtTitulo").val(response.Titulo);
                $("#txtDescripcion").val(response.Descripcion);
                $("#ddlTipoArchivo").val(response.TipoArchivo);
                $("#txtArchivo").val(response.NombreArchivo);

                var fileExtension = response.NombreArchivo.split('.').pop().toLowerCase();
                const videoPreview = $("#videoPreviewContainer");
                const imagePreview = $("#imgArchivo");

                if (fileExtension === 'mp4') {
                    // Mostrar video
                    imagePreview.hide();
                    videoPreview.show();
                    $("#videoSource").attr("src", `${baseURL}files/GaleriaPeruParis/${response.NombreArchivo}`);
                    $("#videoArchivo")[0].load(); // Reload video to ensure it is displayed
                } else {
                    // Mostrar imagen
                    videoPreview.hide();
                    imagePreview.show();
                    imagePreview.attr("src", `${baseURL}files/GaleriaPeruParis/${response.NombreArchivo}`);
                }

                $("#chkActivo").prop("checked", response.Activo ? "checked" : "");
            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function LlenarDatosBloque() {

    var activo;

    const titulo = $("#txtTitulo").val().trim();
    const descripcion = $("#txtDescripcion").val().trim();
    const tipoArchivo = $("#ddlTipoArchivo").val();
    const nombreArchivo = $("#txtArchivo").val().trim();
    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    const bloqueObj = {
        Titulo: titulo,
        Descripcion: descripcion,
        TipoArchivo: tipoArchivo,
        NombreArchivo: nombreArchivo,
        Activo: activo
    };

    return bloqueObj;
}

function LimpiarPopupBloque() {
    $("#hdnGaleriaPeruParisId, #txtTitulo, #txtDescripcion, #txtArchivo, #lblNota").val("");
    $("#fileArchivo").val("");
    $("#chkActivo").prop("checked", true); // Asegúrate de que esté marcado por defecto

    // Limpiar imagen
    $("#imgArchivo").attr("src", "").hide();

    // Limpiar y ocultar video
    const videoPreview = document.querySelector('#videoPreviewContainer');
    videoPreview.style.setProperty('display', 'none', 'important');
    $("#videoSource").attr("src", ""); // Limpia la fuente del video
    $("#videoArchivo")[0].pause(); // Pausa el video
    $("#videoArchivo")[0].currentTime = 0; // Reinicia el tiempo del video

    $("#ddlTipoArchivo").val("0");
}



function ActualizarBloque(GaleriaPeruParisId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = GaleriaPeruParisId;

    $.ajax({
        url: `${baseURL}GaleriaPeruParis/actualizar`,
        data: JSON.stringify(actualizarBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo actualizar el Registro", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar el Registro", "ERROR");
            HideLoader();
        }
    });
}

function RegistrarBloque() {
    ShowLoader();

    const nuevaBloque = LlenarDatosBloque();

    $.ajax({
        url: `${baseURL}GaleriaPeruParis/insertar`,
        data: JSON.stringify(nuevaBloque),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupBloque();
                ListarBloques();
            } else {
                alert("No se pudo registrar", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar", "ERROR");
            HideLoader();
        }
    });
}

function EliminarBloque(id) {

    confirm("¿Está seguro de eliminar el Bloque?",
        function (result) {
            if (result) {
                const eliminarBloque = LlenarDatosBloque();
                eliminarBloque.id = id;

                $.ajax({
                    url: `${baseURL}GaleriaPeruParis/eliminar`,
                    data: JSON.stringify(eliminarBloque),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó el registro", "CHECK");
                        } else {
                            alert("No se pudo eliminar el registro", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarBloques();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar el registro", "ERROR");
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