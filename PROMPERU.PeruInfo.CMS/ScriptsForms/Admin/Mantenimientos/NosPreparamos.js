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
        if ($("#hdnNosPreparamosId").val() === "") {
            //RegistrarBloque();
        } else {
            ActualizarBloque($("#hdnNosPreparamosId").val());
        }
    });

    $btnCancelar.click(function () {
        $(".reveal-modal-bg").css({ transition: "all 0.4s linear", height: "0", opacity: "0" });
        LimpiarPopupBloque();
        $("#btnGuardar").val("");
        $("#lblRegistro").html("Crear Registro");
    });


    $("#fileImagen").change(function (e) {
        e.preventDefault();
        var baseURL = $("base").attr("href");

        var file = $(this).prop("files")[0];
        if (!validarImagen(file)) return;
        var formData = new FormData();

        formData.append("file", file);

        $.ajax({
            url: `${baseURL}nosPreparamos/upload`,
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.file !== null && response.file !== "") {
                    $("#imgImagen").attr("src", `${baseURL}files/nosPreparamos/${response.file}`);
                    $("#txtImagen").val(response.file);
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
        "ajax": `${baseURL}NosPreparamos/listar`,
        "deferRender": true,
        "pageLength": 20,
        "columns": [
            { "data": "Id" },
            { "data": "Titulo" },
            { "data": "SubTitulo" },
            { "data": "Imagen" },
            { "data": "LinkRedSocial" },
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
            $("td", row).eq(3)
                .html(`<img src="${baseURL}files/nosPreparamos/${data.Imagen}" 
                style="width: 100px; margin: auto; display: block;" alt="">`);
            data.Activo === true ? $("td", row).eq(5).html("Activo") : $("td", row).eq(5).html("Inactivo");
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
        url: `${baseURL}NosPreparamos/Seleccionar`,
        data: JSON.stringify(bloqueSeleccionado),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response !== null && response !== undefined) {
                $("#hdnNosPreparamosId").val(response.Id);
                $("#txtTitulo").val(response.Titulo);
                $("#txtSubTitulo").val(response.SubTitulo);
                $("#txtImagen").val(response.Imagen);
                $("#imgImagen").attr("src", `${baseURL}files/nosPreparamos/${response.Imagen}`);                
                $("#txtAltImagen").val(response.AltImagen);
                $("#txtLinkRedSocial").val(response.LinkRedSocial);
                $("#txtHtmlRedSocial").val(response.HtmlRedSocial);
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

    const titulo = $("#txtTitulo").val().trim();
    const subTitulo = $("#txtSubTitulo").val().trim();
    const imagen = $("#txtImagen").val().trim();
    const altImagen = $("#txtAltImagen").val().trim();
    const linkRedSocial = $("#txtLinkRedSocial").val().trim();
    const htmlRedSocial = $("#txtHtmlRedSocial").val().trim();
    
    $("#chkActivo").prop("checked") ? activo = true : activo = false;

    const bloqueObj = {
        Titulo: titulo,
        SubTitulo: subTitulo,
        Imagen: imagen,
        AltImagen: altImagen,
        LinkRedSocial: linkRedSocial,
        HtmlRedSocial: htmlRedSocial,
        Activo: activo
    };

    return bloqueObj;
}

function LimpiarPopupBloque() {
    $("#txtTitulo, #txtSubTitulo, #txtImagen, #txtAltImagen, #txtAltImagen, #txtLinkRedSocial, #txtHtmlRedSocial").val("");
    $("#fileImagen, #hdnNosPreparamosId").val("");
    $("#chkDestacado, #chkActivo").prop("checked", "checked");
    $("#imgImagen").attr("src", "");
}



function ActualizarBloque(NosPreparamosId) {
    ShowLoader();

    const actualizarBloque = LlenarDatosBloque();
    actualizarBloque.Id = NosPreparamosId;

    $.ajax({
        url: `${baseURL}NosPreparamos/actualizar`,
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