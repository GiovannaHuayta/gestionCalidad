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
        speed: 1500,
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
        autoplay: true,
        autoplaySpeed: 4000,
        speed: 1000,
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
            scrollTop: $('.container-categorias-home').offset().top - 60
        }, 'slow');

    });
    // ********** ********** /NEXT-SECTION-BANNER ********** **********


    
    // ********** ********** SLIDER-BLOG ********** **********
    $('.content-slider-blog').slick({
        infinite: true,
        slidesToShow: 3,
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
                slidesToShow: 2
            }
        },{
            breakpoint: 992,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 2
            }
        },{
            breakpoint: 1200,
            settings: {
                arrows: true,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 3
            }
        }]
    });
    // ********** ********** /SLIDER-BLOG ********** **********



    // ********** ********** ACTIVE-MOSTRAR-CARDS ********** **********
    $( ".ver-mas" ).click(function() {
        $( ".inferior-cards" ).toggleClass('mostrar-card-inferior');
        $( ".ver-mas" ).find('p').toggleClass('mostrar-ver-menos');
    });
    // ********** ********** /ACTIVE-MOSTRAR-CARDS ********** **********
    

    
    // ********** ********** SLIDER-NOTICIA-HOME ********** **********
    $('.slider-noticia').slick({
        infinite: false,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        arrows: false,
        dots: false,
        // centerMode: true,
        // centerPadding: '60px',
        responsive: [{
            breakpoint: 576,
            settings: {
                arrows: true,
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



    // ********** ********** ACTIVE-MOSTRAR-IFRAME ********** **********
    $( ".ver-mas-iframe" ).click(function() {
        $( ".contenedor-inferior-iframe" ).toggleClass('active-mostrar-iframe-inferior');
        $( ".ver-mas-iframe" ).find('p').toggleClass('mostrar-ver-menos-iframe');
    });
    // ********** ********** /ACTIVE-MOSTRAR-IFRAME ********** **********


       // ********** ********** /SLIDER-IFRAME ********** **********




    /****************************************************/
    /******************** MA **********************/
    /****************************************************/


    // ********** ********** COMENTARIO ********** **********
    
    // ********** ********** /COMENTARIO ********** **********


    // AOS.init();
    
});