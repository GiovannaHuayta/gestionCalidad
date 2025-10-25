$(document).ready(function () {

    /****************************************************/
    /******************** MI **********************/
    /****************************************************/

    // ********** ********** OCULTAR-DATE-FILTRO ********** **********
    $( ".filtro-country").click(function() {
        $(this).toggleClass('icon-flecha-filtro');
        $('.filtro-country').parent('.filtro-desplegable').find( ".content-date-filtro").toggleClass('ocultar-filtro');
    });
    // ********** ********** OCULTAR-DATE-FILTRO ********** **********

    // ********** ********** OCULTAR-TAGS ********** **********
    $( ".filtro-tag").click(function() {
        $(this).toggleClass('icon-flecha-filtro');
        $(this).parent('.filtro-desplegable').find( '.box-tags').toggleClass('ocultar-tag'); 
    });
    // ********** ********** OCULTAR-TAGS ********** **********



    // ********** ********** ANIMACION-SCROLL-MASONRY ********** **********
    /*new AnimOnScroll( document.getElementById( 'grid' ), {
        minDuration : 0.4,
        maxDuration : 0.7,
        viewportFactor : 0.2
    } );*/
    // ********** ********** /ANIMACION-SCROLL-MASONRY ********** **********








    





    
    /****************************************************/
    /******************** MA **********************/
    /****************************************************/


    // ********** ********** COMENTARIO ********** **********
    
    // ********** ********** /COMENTARIO ********** **********



    
});