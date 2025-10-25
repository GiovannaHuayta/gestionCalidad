$(document).ready(function () {

    /****************************************************/
    /******************** MI **********************/
    /****************************************************/


    // ********** ********** MOSTRAR MENU-CATEGORIAS ********** **********
    $( ".title-select").click(function() {
        $(this).parent('.select-tipos-embajadores').find(".content-items-select").toggleClass('mostrar-menu-categorias');
    });
    // ********** ********** /MOSTRAR MENU-CATEGORIAS ********** **********



    // ********** ********** ACTIVE-ITEM-CATEGORIA ********** **********
    $( ".box-item-categoria").click(function() {
        $('.box-item-categoria').removeClass('active-item-categoria');
        $(this).addClass('active-item-categoria');
    });
    // ********** ********** /MOSTRAR MENU-CATEGORIAS ********** **********


    
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