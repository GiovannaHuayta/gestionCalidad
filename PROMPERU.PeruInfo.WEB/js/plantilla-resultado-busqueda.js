$(document).ready(function () {

    /****************************************************/
    /******************** MI **********************/
    /****************************************************/


    // ********** ********** ACTIVE-TODOS-SUBMENU ********** **********
    $( ".title-todos-submenu").click(function() {
        $('.title-submenu-filtro').removeClass('active-item-categoria');
        $('.contenedor-sublista-filtros').removeClass('mostrar-items-submenu');
        $('.item-subcategoria').removeClass('active-item-subcategoria');
        $(this).addClass('active-item-categoria');
    });
    // ********** ********** /ACTIVE-TODOS-SUBMENU ********** **********



    // ********** ********** MOSTRAR MENU-SUBCATEGORIAS ********** **********
    $( ".title-submenu-filtro").click(function() {
        $('.title-todos-submenu').removeClass('active-item-categoria');
        $('.title-submenu-filtro').removeClass('active-item-categoria');
        $('.contenedor-sublista-filtros').removeClass('mostrar-items-submenu');
        // $('.item-subcategoria').removeClass('active-item-subcategoria');
        $(this).addClass('active-item-categoria');
        $(this).parent('.box-item-filtro').find(".contenedor-sublista-filtros").toggleClass('mostrar-items-submenu');
    });
    // ********** ********** /MOSTRAR MENU-SUBCATEGORIAS ********** **********



     // ********** ********** ACTIVE-ITEM-SUBCATEGORIA ********** **********
    $( ".item-subcategoria").click(function() {
        $('.item-subcategoria').removeClass('active-item-subcategoria');
        $(this).addClass('active-item-subcategoria');
    });
    // ********** ********** /ACTIVE-TODOS-SUBCATEGORIA ********** **********



    // ********** ********** CALENDAR-FILTRO ********** **********
    $( function() {
        $( ".active-datepicker" ).datepicker();
    } );
    // ********** ********** /CALENDARIO-FILTRO ********** **********



    // ********** ********** MOSTRAR FILTROS-LATERAL ********** **********
    $( ".title-select").click(function() {
        $('.contenedor-mobile-filtros-lateral').addClass('mostrar-filtros-lateral');
    });
    // ********** ********** MOSTRAR FILTROS-LATERAL ********** **********

    // ********** ********** OCULTAR FILTROS-LATERAL ********** **********
    $( ".icon-close-lateral, .box-fondo-oscuro").click(function() {
        $('.title-submenu-filtro-mobile').removeClass('active-item-categoria');
        $('.contenedor-sublista-filtros-mobile').removeClass('mostrar-items-submenu');
        $('.item-subcategoria-mobile').removeClass('active-item-subcategoria');
        $('.contenedor-mobile-filtros-lateral').removeClass('mostrar-filtros-lateral');
        $('.title-todos-submenu-mobile').removeClass('active-item-categoria');
    });

    // $( ".box-fondo-oscuro").click(function() {
    //     $('.contenedor-mobile-filtros-lateral').removeClass('mostrar-filtros-lateral');
    // });
    // ********** ********** MOSTRAR FILTROS-LATERAL ********** **********



    // ********** ********** ACTIVE-TODOS-SUBMENU-MOBILE ********** **********
    $( ".title-todos-submenu-mobile").click(function() {
        $('.contenedor-sublista-filtros-mobile').removeClass('mostrar-items-submenu');
        $('.title-submenu-filtro-mobile').removeClass('active-item-categoria');
        $('.item-subcategoria-mobile').removeClass('active-item-subcategoria');
        $(this).addClass('active-item-categoria');
    });
    // ********** ********** /ACTIVE-TODOS-SUBMENU-MOBILE ********** **********



    // ********** ********** MOSTRAR MENU-SUBCATEGORIAS-MOBILE ********** **********
    $( ".title-submenu-filtro-mobile").click(function() {
        $('.title-todos-submenu-mobile').removeClass('active-item-categoria');
        $('.title-submenu-filtro-mobile').removeClass('active-item-categoria');
        $('.contenedor-sublista-filtros-mobile').removeClass('mostrar-items-submenu');
        $(this).addClass('active-item-categoria');
        $(this).parent('.box-item-filtro-mobile').find(".contenedor-sublista-filtros-mobile").toggleClass('mostrar-items-submenu');
    });
    // ********** ********** /MOSTRAR MENU-SUBCATEGORIAS-MOBILE ********** **********


    
    // ********** ********** ACTIVE-ITEM-SUBCATEGORIA-MOBILE ********** **********
    $( ".item-subcategoria-mobile").click(function() {
        $('.item-subcategoria-mobile').removeClass('active-item-subcategoria');
        $(this).addClass('active-item-subcategoria');
    });
    // ********** ********** /ACTIVE-TODOS-SUBCATEGORIA-MOBILE ********** **********



    // ********** ********** SLIDER-MAS-VISTO ********** **********
    $('.content-slider-video').slick({
        infinite: false,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        // speed: 1500,
        arrows: false,
        dots: false,
        fade: true,
        asNavFor: '.content-slider-bottom',
        responsive: [{
            breakpoint: 576,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }, {
            breakpoint: 768,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        },{
            breakpoint: 992,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        },{
            breakpoint: 1200,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }]
    });
    $('.content-slider-bottom').slick({
        infinite: false,
        slidesToShow: 2,
        slidesToScroll: 1,
        asNavFor: '.content-slider-video',
        dots: false,
        arrows: false,
        centerMode: false,
        centerPadding: '0px',
        focusOnSelect: true,
        responsive: [{
            breakpoint: 576,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 2
            }
        }, {
            breakpoint: 768,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 2
            }
        },{
            breakpoint: 992,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 2
            }
        },{
            breakpoint: 1200,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 2
            }
        }]
    });
    // ********** ********** /SLIDER-MAS-VISTO ********** **********






    

    /****************************************************/
    /******************** MA **********************/
    /****************************************************/


    // ********** ********** COMENTARIO ********** **********
    
    // ********** ********** /COMENTARIO ********** **********



    
});