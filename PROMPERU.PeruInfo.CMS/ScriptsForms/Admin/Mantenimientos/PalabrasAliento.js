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
        if ($("#hdnPalabraAlientoId").val() === "") {
            //RegistrarBloque();
        } else {
            ActualizarBloque($("#hdnPalabraAlientoId").val());
        }
    });

    $btnCancelar.click(function () {
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
        LimpiarPopupBloque();
        $("#btnGuardar").val("");
        $("#lblRegistro").html("Palabra de Aliento");
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
        "order": [[1, "desc"]],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": `${baseURL}PalabrasAliento/listar`,
        "deferRender": true,
        "pageLength": 20,
        "columns": [
            { "data": "Id" },
            { "data": "FechaCreacion" },
            { "data": "Mensaje" },
            { "data": "EstadoAprobacion" },
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
            var estadosActivo = {
                true: 'Activo',
                false: 'Inactivo',
                null: 'Pendiente'
            };

            var estadoActivo = estadosActivo[data.Activo];
            $("td", row).eq(4).html(`${estadoActivo}`);

            var fechaHora = data.FechaCreacion.split("T");
            var fecha = fechaHora[0];
            var hora = fechaHora[1].split(":").slice(0, 2).join(":");
            $("td", row).eq(1).html(`${fecha} ${hora}`);

            var estados = {
                0: 'Pendiente',
                1: 'Aprobado',
                2: 'Rechazado'
            };

            var estado = estados[data.EstadoAprobacion] || 'Desconocido';
            $("td", row).eq(3).html(`${estado}`);

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
        $("#lblRegistro").html("Palabra de Aliento");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });

    }
}

function SeleccionarBloque(Id) {
    ShowLoader();

    const bloqueSeleccionado = {
        id: Id
    };

    $.ajax({
        url: `${baseURL}PalabrasAliento/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnPalabraAlientoId").val(response.Id);
                $("#ddlEstadoAprobacion").val(response.EstadoAprobacion);
                $("#txtMotivo").val(response.MotivoRechazo);
                if (response.Activo === null || response.Activo === true) {
                    $("#chkActivo").prop("checked", true);
                } else {
                    $("#chkActivo").prop("checked", false);
                }

            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function LlenarDatosBloque() {

    var activo;

    const estadoAprobacion = $("#ddlEstadoAprobacion").val();
    const motivoRechazo = $("#txtMotivo").val().trim();

    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    const bloqueObj = {
        EstadoAprobacion: estadoAprobacion,
        MotivoRechazo: motivoRechazo,
        Activo: activo
    };

    return bloqueObj;
}

function LimpiarPopupBloque() {
    $("#txtTitulo, #txtSubTitulo, #txtImagen, #txtAltImagen, #txtAltImagen, #txtLinkRedSocial, #txtHtmlRedSocial").val("");
    $("#fileImagen, #hdnNosPreparamosId").val("");
    $("#chkDestacado, #chkActivo").prop("checked", "checked");
    $("#imgImagen").attr("src", "");
    $("#ddlEstadoAprobacion").val("0");
}



function ActualizarBloque(NosPreparamosId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = NosPreparamosId;

    $.ajax({
        url: `${baseURL}PalabrasAliento/actualizar`,
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