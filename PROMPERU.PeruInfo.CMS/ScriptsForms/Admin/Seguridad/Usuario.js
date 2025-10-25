$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = '/usuario/login';
        }
    });

    ListarUsuarios();

    const $btnNuevoUsuario = $("#btnNuevo");
    const $btnCancelar = $("#btnCancelar");
    const $btnGuardarUsuario = $("#btnGuardar");

    $(document).keyup(function (event) {
        if (event.which === 27) {
            $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
        }
    });

    $btnNuevoUsuario.click(function () {
        $.ajax({
            url: '/usuario/verificarSesion',
            type: 'POST',
            success: function (response) {
                if (response.autenticado) {
                    $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
                    LimpiarPopupBloque();
                } else {
                    window.location.href = '/usuario/login';
                }
            },
            error: function () {
                window.location.href = '/usuario/login';
            }
        });
    });

    $btnCancelar.click(function () {
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
        LimpiarPopupUsuario();
        $("#btnGuardar").val("");
        $("#lblRegistro").html("Crear Usuario");
    });

    $btnGuardarUsuario.click(function () {
        if ($("#hdnUsuarioId").val() === "") {
            RegistrarUsuario();
        } else {
            ActualizarUsuario($("#hdnUsuarioId").val());
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
});

var baseURL = $("base").attr("href");

function LimpiarPopupUsuario() {
    $("#txtEmail, #txtNombre, #txtApellido, #txtClave, #hdnUsuarioId").val("");
    $("#ddlPerfil").val();
    $("#chkActivo").prop("checked", "checked");
}

function ListarUsuarios() {
    ShowLoader();

    var table = $("#gridUsuarios").DataTable();

    //table.destroy();

    //$("#gridUsuarios").empty();

    table = $("#gridUsuarios").DataTable({
        "destroy": true,
        "paging": false,
        "searching": true,
        // orderCellsTop: true,
        "pagingType": "full_numbers",
        // fixedHeader: true,
        "order": [[6, "desc"]], // Sort by first column descending
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": `${baseURL}usuario/listar`,
        "deferRender": true,
        "columns": [
            { "data": "Id" },
            { "data": "Nombres" },
            { "data": "Apellidos" },
            { "data": "Email" },
            { "data": "Perfil" },
            { "data": "Activo" },
            { "data": "FechaCreacion" },
            {
                "defaultContent": `<div class="accion-box">
                            <a href="javascript:" id="EditarUsuario" title="Editar">
                                <span class="fas fa-edit"></span>
                            </a>
                            <a href="javascript:" id="EliminarUsuario" title="Eliminar">
                                <span class="fas icon-cross"></span>
                            </a>
                        </div>`
            }
        ],
        "createdRow": function (row, data, index) {
            // Activo
            data.Activo ? $("td", row).eq(5).html("Activo") : $("td", row).eq(5).html("Desactivo");
            $("td", row).eq(6).html(data.FechaCreacion.split("T")[0]);
        }
    });

    // Editar agenda
    $("#gridUsuarios tbody").on("click",
        "#EditarUsuario",
        function () {
            const data = table.row($(this).parents("tr")).data();
            EditarUsuario(data.Id);
        });

    // Eliminar agenda
    $("#gridUsuarios tbody").on("click",
        "#EliminarUsuario",
        function () {
            const data = table.row($(this).parents("tr")).data();
            EliminarUsuario(data.Id);
        });

    //$('#gridUsuarios thead tr').clone(true).appendTo('#gridUsuarios thead');
    $("#gridUsuarios thead tr:eq(0) th").each(function (i) {
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

function LlenarDatosUsuario() {
    var usuarioActivo;

    const nombres = $("#txtNombre").val().trim();
    const apellidos = $("#txtApellido").val().trim();
    const correo = $("#txtEmail").val().trim();
    const contrasenia = $("#txtClave").val().trim();
    const perfilId = $("#ddlPerfil").val();
    const id = $("#hdnUsuarioId").val().trim();
    $("#chkActivo").prop("checked") ? usuarioActivo = true : usuarioActivo = false;

    const usuarioObj = {
        Nombres: nombres,
        Apellidos: apellidos,
        Email: correo,
        Clave: contrasenia,
        Perfil: perfilId,
        Activo: usuarioActivo
    };

    return usuarioObj;
}

function RegistrarUsuario() {
    ShowLoader();

    const nuevaUsuario = LlenarDatosUsuario();

    $.ajax({
        url: `${baseURL}usuario/insertar`,
        data: JSON.stringify(nuevaUsuario),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                LimpiarPopupUsuario();
                ListarUsuarios();
            } else {
                alert("No se pudo registrar al usuario", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo registrar al usuario", "ERROR");
            HideLoader();
        }
    });
}

function SeleccionarUsuario(usuarioId) {
    ShowLoader();

    $("#hdnUsuarioId").val(usuarioId);

    const usuarioSeleccionado = {
        Id: usuarioId
    };

    $.ajax({
        url: `${baseURL}usuario/seleccionar`,
        data: JSON.stringify(usuarioSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnUsuarioId").val(response.Id);
                $("#txtNombre").val(response.Nombres);
                $("#txtApellido").val(response.Apellidos);
                $("#txtEmail").val(response.Email);
                $("#ddlPerfil").val(response.Perfil);

                response.Activo ? $("#chkActivo").prop("checked", "checked") : $("#chkActivo").prop("checked", "");
            }
        },
        complete: function () {
            HideLoader();
        }
    });
}

function EditarUsuario(usuarioId) {
    if (usuarioId !== null && usuarioId !== "") {
        SeleccionarUsuario(usuarioId);
        $("#btnGuardar").val("Actualizar");
        $("#lblRegistro").html("Actualizar Usuario");
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "100%", opacity: "1" });
    }
}

function ActualizarUsuario(usuarioId) {
    ShowLoader();

    const actualizarUsuario = LlenarDatosUsuario();
    actualizarUsuario.Id = usuarioId;

    $.ajax({
        url: `${baseURL}usuario/actualizar`,
        data: JSON.stringify(actualizarUsuario),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response) {
                $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
                ListarUsuarios();
            } else {
                alert("No se pudo actualizar el usuario", "WARNING");
                return;
            }
        },
        complete: function () {
            HideLoader();
        },
        error: function (error) {
            alert("No se pudo actualizar el usuario", "ERROR");
            HideLoader();
        }
    });
}

function EliminarUsuario(usuarioId) {

    confirm("¿Está seguro de eliminar el usuario?",
        function (result) {
            if (result) {
                const eliminarUsuario = LlenarDatosUsuario();
                eliminarUsuario.Id = usuarioId;

                $.ajax({
                    url: `${baseURL}usuario/eliminar`,
                    data: JSON.stringify(eliminarUsuario),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response) {
                            alert("Se eliminó el usuario", "CHECK");
                        } else {
                            alert("No se pudo eliminar el usuario", "WARNING");
                        }
                    },
                    complete: function () {
                        HideLoader();
                        ListarUsuarios();
                    },
                    error: function (error) {
                        alert("No se pudo eliminar el usuario", "ERROR");
                    }
                });
            } else {
                return false;
            }

            return false;
        });

    $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });

    ListarUsuarios();
}