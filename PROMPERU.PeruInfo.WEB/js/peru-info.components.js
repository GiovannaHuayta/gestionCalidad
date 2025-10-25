$(document).ready(function () {

    /****************************************************/
    /******************** MI **********************/
    /****************************************************/


    // ********** ********** SCROLL-HEADER ********** **********
    $(document).scroll(function (event) {
        var scrollTop = $(document).scrollTop();
        if (scrollTop <= 20) {
            $('.header-peru-info').removeClass('active-scroll-header');
        } else {
            $('.header-peru-info').addClass('active-scroll-header');
        }
    });
    // ********** ********** /SCROLL-HEADER ********** **********



    // ********** ********** ACTIVE-CAMBIO-IDIOMA ********** **********
    $( ".icon-lista-idioma" ).click(function() {
        $('.container-selector-idioma').toggleClass('active-cambio-idioma');
    });
    $( ".icon-lista-idioma" ).blur(function() {
        $('.container-selector-idioma').removeClass('active-cambio-idioma');
    });
    // ********** ********** /ACTIVE-CAMBIO-IDIOMA ********** **********



    // ********** ********** MOSTRAR-MENU-LATERAL ********** **********
    $( ".icon-lista-barras" ).click(function() {
        $('.item-lista-menu').addClass('ocultar-lista-menu-header');
        $('.menu-lateral').addClass('mostrar-menu-lateral');
    });
    // ********** ********** /MOSTRAR-MENU-LATERAL ********** **********

    // ********** ********** OCULTAR-MENU-LATERAL ********** **********
    $( ".cerrar-menu-lateral" ).click(function() {
        $('.item-lista-menu').removeClass('ocultar-lista-menu-header');
        $('.menu-lateral').removeClass('mostrar-menu-lateral');
    });
    // ********** ********** /OCULTAR-MENU-LATERAL ********** **********



    // ********** ********** ACTIVE-SUBLISTA-LATERAL ********** **********
    $( '.title-sublista-menu-lateral' ).click(function() {
        var element = $( this).parents('.contenedor-title-submenu-lateral').find(".sublista-menu-lateral");
        var existClass = element.hasClass('active-sublista-lateral');
        console.log(existClass);
        $('.sublista-menu-lateral').removeClass('active-sublista-lateral');
        if (existClass) {
            element.removeClass('active-sublista-lateral');
        } else {
            element.addClass('active-sublista-lateral');
        }
        // $( this).parents('.contenedor-title-submenu-lateral').find(".sublista-menu-lateral").toggleClass('active-sublista-lateral');
    });
    // ********** ********** /ACTIVE-SUBLISTA-LATERAL ********** **********



    // ********** ********** ACTIVE-MODAL-BUSCADOR ********** **********
    $( ".icon-lista-buscar" ).click(function() {
        $('.modal-buscador-filtro').addClass('active-modal-buscador');
    });
    // ********** ********** /ACTIVE-MODAL-BUSCADOR ********** **********

    // ********** ********** OCULTAR-MODAL-BUSCADOR ********** **********
    $( ".icon-cerrar-modal" ).click(function() {
        $('.modal-buscador-filtro').removeClass('active-modal-buscador');
    });
    // ********** ********** /OCULTAR-MODAL-BUSCADOR ********** **********



    // ********** ********** ACTIVE-MODAL-INFORMATIVO ********** **********
        $(".content-icon-lista.alert").hover(
            function () {
                $('.modal-informativo-archivo2').addClass('active-modal-informativo');
                $('.bg-alert').fadeIn();
            }, function () {
                $('.modal-informativo-archivo2').removeClass('active-modal-informativo');
                $('.bg-alert').fadeOut();
            }
        );

    // ********** ********** /ACTIVE-MODAL-INFORMATIVO ********** **********

    // ********** ********** OCULTAR-MODAL-INFORMATIVO ********** **********
    $( ".box-cerrar-modal" ).click(function() {
        $('.modal-informativo-archivo').removeClass('active-modal-informativo');
    });
    // ********** ********** -/OCULTAR-MODAL-INFORMATIVO ********** **********



    // ********** ********** MOSTRAR-LISTA ********** **********
    $(".title-menu-lista").click(function() {
        var elementDespliegue = $(this).parent('.contenedor-menu-lista-mobile').find( ".menu-lista-mobile")
        var existClassDespliegue = elementDespliegue.hasClass('mostrar-menu-lista-mobile');
        var existClassFlechita = $(this).hasClass('icon-flecha-lista');

        $('.title-menu-lista').removeClass('icon-flecha-lista');
        $('.menu-lista-mobile').removeClass('mostrar-menu-lista-mobile');  
        
    
        // console.log(existClassDespliegue,existClassFlechita);
        if (existClassDespliegue) {
            elementDespliegue.removeClass('mostrar-menu-lista-mobile');
        } else {
            elementDespliegue.addClass('mostrar-menu-lista-mobile');
        }
        if (existClassFlechita) {
            $(this).removeClass('icon-flecha-lista');
        } else {
            $(this).addClass('icon-flecha-lista');
        }
    });
    // ********** ********** MOSTRAR-LISTA ********** **********


    // ********** ********** SLIDER-NOTICIA-HOME ********** **********
    /*$('.content-slider-archivo').slick({
        infinite: false,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        arrows: false,
        dots: true,
        responsive: [{
            breakpoint: 576,
            settings: {
                arrows: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }, {
            breakpoint: 768,
            settings: {
                arrows: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }, {
            breakpoint: 992,
            settings: {
                arrows: false,
                slidesToScroll: 1,
                slidesToShow: 1,
                centerMode: true,
                centerPadding: '60px'
            }
        }, {
            breakpoint: 1200,
            settings: {
                arrows: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }]
    });*/
    // ********** ********** /SLIDER-NOTICIA-HOME ********** **********




    

    /****************************************************/
    /******************** MA **********************/
    /****************************************************/


    // ********** ********** COMENTARIO ********** **********
    
    // ********** ********** /COMENTARIO ********** **********


    AOS.init();
    
});


