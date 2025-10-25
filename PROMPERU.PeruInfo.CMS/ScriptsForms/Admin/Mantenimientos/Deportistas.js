$(document).ready(function () {

    ListarBloques();

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    const $btnCancelar = $("#btnCancelar");
    const $btnGuardarBloque = $("#btnGuardar");

    $btnGuardarBloque.click(function () {
        if ($("#hdnDeportistaId").val() === "") {
            //RegistrarBloque();
        } else {
            ActualizarBloque($("#hdnDeportistaId").val());
        }
    });

    $btnCancelar.click(function () {
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
        LimpiarPopupBloque();
        $("#btnGuardar").val("");
        $("#lblRegistro").html("Crear Registro Deportistas");
    });


    $("#fileImagenDesktop").change(function (e) {
        e.preventDefault();
        var baseURL = $("base").attr("href");

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}Deportistas/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenDesktop").attr("src", `${baseURL}files/deportistas/${response.file}`);
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
        var baseURL = $("base").attr("href");

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}Deportistas/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagenMovil").attr("src", `${baseURL}files/deportistas/${response.file}`);
                    $("#txtImagenMovil").val(response.file);
                }
            },
            error: function (response) {
                console.error(response.message);
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
        "ajax": `${baseURL}Deportistas/listar`,
        "deferRender": true,
        "pageLength": 20,
        "columns": [
            { "data": "Id" },
            { "data": "NombreDeportista" },
            { "data": "ImagenDesktop" },
            { "data": "ImagenMobile" },
            { "data": "Disciplina" },
            { "data": "LinkNota" },
            { "data": "Activo" },
            {
                "defaultContent": `<div class="accion-box">
                            <a href="javascript:" id="EditarBloque" title="Editar">
                                <span class="fas fa-edit"></span>
                            </a>                            
                        </div>`
            }
        ],
        "createdRow": function (row, data, index) {
            $("td", row).eq(2)
                .html(`<img src="${baseURL}files/deportistas/${data.ImagenDesktop}" 
                style="width: 100px; margin: auto; display: block;" alt="">`);
            $("td", row).eq(3)
                .html(`<img src="${baseURL}files/deportistas/${data.ImagenMobile}" 
                style="width: 100px; margin: auto; display: block;" alt="">`);
            data.Activo === true ? $("td", row).eq(6).html("Activo") : $("td", row).eq(6).html("Inactivo");
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
        //$("#lblRegistro").html("Actualizar Registro");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });

    }
}

function SeleccionarBloque(Id) {
    ShowLoader();

    const bloqueSeleccionado = {
        id: Id
    };

    $.ajax({
        url: `${baseURL}Deportistas/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnDeportistaId").val(response.Id);
                $("#txtNombreDeportista").val(response.NombreDeportista);
                $("#txtImagenDesktop").val(response.ImagenDesktop);
                $("#imgImagenDesktop").attr("src", `${baseURL}files/deportistas/${response.ImagenDesktop}`);
                $("#txtImagenMovil").val(response.ImagenMobile);
                $("#imgImagenMovil").attr("src", `${baseURL}files/deportistas/${response.ImagenMobile}`);
                $("#txtDisciplina").val(response.Disciplina);
                $("#txtLinkNota").val(response.LinkNota);
                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");

            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function LlenarDatosBloque() {

    var activo;

    const nombreDeportista = $("#txtNombreDeportista").val().trim();
    const imagenDesktop = $("#txtImagenDesktop").val().trim();
    const imagenMobile = $("#txtImagenMovil").val().trim();
    const disciplina = $("#txtDisciplina").val().trim();
    const linkNota = $("#txtLinkNota").val().trim();
    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    const bloqueObj = {
        NombreDeportista: nombreDeportista,
        ImagenDesktop: imagenDesktop,
        ImagenMobile: imagenMobile,
        Disciplina: disciplina,
        LinkNota: linkNota,
        Activo: activo
    };

    return bloqueObj;
}

function LimpiarPopupBloque() {
    $("#txtNombreDeportista, #txtImagenDesktop, #txtImagenMovil, #txtDisciplina, #txtLinkNota").val("");
    $("#fileImagenDesktop, #fileImagenMovil").val("");
    $("#chkDestacado, #chkActivo").prop("checked", "checked");
    $("#imgImagenDesktop, #imgImagenMovil").attr("src", "");

}



function ActualizarBloque(deportistaId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = deportistaId;

    $.ajax({
        url: `${baseURL}deportistas/actualizar`,
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