$(document).ready(function () {
    $("#buscado").on("keyup change",
        () => {
            const buscado = $("#buscado").val();

            if (buscado.length > 2) {
                callDataFromApiSearch($("#buscado").val());
                $(".filtro-texto").html($("#buscado").val());
            }
        });
});

function callDataFromApiSearch(buscado) {
    const url = `${baseDomain}/${culture}/api/publicacion?idioma=${idioma}&buscado=${buscado}`;
    const linkNoticia = $("#hdnLinkNoticia").val();

    $.ajax({
        url: url,
        success: result => {
            $("#contenedor-resultado-blog, #contenedor-resultado-noticias").html('');

            if (result.data.length > 0) {
                $(".contenedor-blog-noticias").css("display", "");
            } else {
                $(".contenedor-blog-noticias").css("display", "none");
            }

            result.data.filter(item => item.TipoId === 1).slice(0, 3).forEach(item => {
                $("#contenedor-resultado-blog").append(`
                            <a href="/${culture}/${item.CategoriaSlug}/${linkNoticia}/${item.CategoriaId}/${item.SubcategoriaId}/${item.Slug}" class="content-card-resultado">
                                <div class="left-logo-resultado">
                                    <img src="${baseDomain}/archivos/publicacion/${item.Imagen}" alt="${item.AltImagen}">
                                </div>
                                <div class="right-info-resultado">
                                    <h5>${item.Titulo}</h5>
                                    <p>${item.Resumen}</p>
                                </div>
                            </a>
                        `);
            });

            result.data.filter(item => item.TipoId === 2).slice(0, 3).forEach(item => {
                $("#contenedor-resultado-noticias").append(`
                            <a href="/${culture}/${item.CategoriaSlug}/${linkNoticia}/${item.CategoriaId}/${item.SubcategoriaId}/${item.Slug}" class="content-card-resultado">
                                <div class="left-logo-resultado">
                                    ${item.Imagen ? `<img src="${baseDomain}/archivos/publicacion/${item.Imagen}" alt="${item.AltImagen}">` : ''}
                                </div>
                                <div class="right-info-resultado">
                                    <h5>${item.Titulo}</h5>
                                    <p>${item.Resumen}</p>
                                </div>
                            </a>
                        `);
            });
        }
    });
}