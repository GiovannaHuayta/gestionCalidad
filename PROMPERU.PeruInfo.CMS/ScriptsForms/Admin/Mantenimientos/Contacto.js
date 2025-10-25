$(document).ready(function () {

    $(document).ajaxError(function (event, jqXHR) {
        if (jqXHR.status === 401) {
            window.location.href = baseURL + 'usuario/login';
        }
    });

    ListarUsuarios();
});

var baseURL = $("base").attr("href");


function ListarUsuarios() {
    ShowLoader();

    var table = $("#gridBloques").DataTable({
        "destroy": true,
        "paging": false,
        "searching": true,
        // orderCellsTop: true,
        "pagingType": "full_numbers",
        // fixedHeader: true,
        "order": [[4, "desc"]], // Sort by first column descending
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        "ajax": `${baseURL}Contacto/Listar`,
        "deferRender": true,
        "columns": [
            { "data": "Id" },
            { "data": "Nombre" },
            { "data": "Apellidos" },
            { "data": "Correo" },
            { "data": "FechaRegistro" }
        ],
        "createdRow": function (row, data, index) {
            $("td", row).eq(4).html(data.FechaRegistro.split("T")[0]);
        }
    });

    //$('#gridContacto thead tr').clone(true).appendTo('#gridContacto thead');
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