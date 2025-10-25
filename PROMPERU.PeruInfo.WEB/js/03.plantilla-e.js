$(document).ready(function () {

    /****************************************************/
    /******************** MI **********************/
    /****************************************************/


    // ********** ********** SLIDER-BANNER-HOME ********** **********
    $('.content-slider-img').slick({
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        speed: 1200,
        arrows: false,
        dots: false,
        fade: true,
        asNavFor: '.content-slider-txt',
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
    $('.content-slider-txt').slick({
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        // autoplaySpeed: 4000,
        speed: 1200,
        // arrows: true,
        dots: false,
        prevArrow: $('.prev-banner'),
        nextArrow: $('.next-banner'),
        asNavFor: '.content-slider-img',
        responsive: [{
            breakpoint: 576,
            settings: {
                // arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }, {
            breakpoint: 768,
            settings: {
                // arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        },{
            breakpoint: 992,
            settings: {
                // arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        },{
            breakpoint: 1200,
            settings: {
                // arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }]
    });
    // ********** ********** /SLIDER-BANNER-HOME ********** **********



    // ********** ********** NEXT-SECTION-BANNER ********** **********
    $('.scroll-boton').on('click', function () {
        $('body, html').animate({
            scrollTop: $('.buscador-filtros').offset().top - 70
        }, 'slow');

    });
    // ********** ********** /NEXT-SECTION-BANNER ********** **********



    // ********** ********** CALENDARIO-FILTRO ********** **********
    $( function() {
        $( ".active-datepicker" ).datepicker();
    } );
    // ********** ********** /CALENDARIO-FILTRO ********** **********



    // ********** ********** SLIDER-MAS-NUEVO ********** **********
    $('.content-slider-mas-nuevo').slick({
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        // speed: 1500,
        arrows: true,
        dots: false,
        responsive: [{
            breakpoint: 576,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }, {
            breakpoint: 768,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        },{
            breakpoint: 992,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        },{
            breakpoint: 1200,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }]
    });
    // ********** ********** /SLIDER-MAS-NUEVO ********** **********



    // ********** ********** SLIDER-NUEVO-MOBILE ********** **********
    $('.content-slider-nuevo-mobile').slick({
        infinite: false,
        slidesToShow: 2,
        slidesToScroll: 1,
        autoplay: false,
        // speed: 1500,
        arrows: true,
        dots: false,
        responsive: [{
            breakpoint: 576,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1,
                centerMode: true,
                centerPadding: '68px'
            }
        }, {
            breakpoint: 768,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 2,
                centerMode: true,
                centerPadding: '60px'
            }
        },{
            breakpoint: 992,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 2,
                centerMode: true,
                centerPadding: '100px'
            }
        }]
    });
    // ********** ********** /SLIDER-NUEVO-MOBILE ********** **********



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

    

    // ********** ********** SLIDER-NOTICIA-HOME ********** **********
    $('.slider-noticia').slick({
        infinite: false,
        slidesToShow: 3,
        slidesToScroll: 1,
        autoplay: false,
        arrows: false,
        dots: false,
        // centerMode: true,
        // centerPadding: '60px',
        responsive: [{
            breakpoint: 576,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
                // centerMode: true,
                // centerPadding: '60px',
            }
        }, {
            breakpoint: 768,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
                // centerMode: true,
                // centerPadding: '60px',
            }
        },{
            breakpoint: 992,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 2,
                centerMode: true,
                centerPadding: '60px'
            }
        },{
            breakpoint: 1200,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 1
                // centerMode: true,
                // centerPadding: '60px',
            }
        }]
    });
    // ********** ********** /SLIDER-NOTICIA-HOME ********** **********



    





    










    /****************************************************/
    /******************** MA **********************/
    /****************************************************/


    // ********** ********** COMENTARIO ********** **********
    
    // ********** ********** /COMENTARIO ********** **********



    
});