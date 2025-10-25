$(document).ready(function () {

    /****************************************************/
    /******************** MI **********************/
    /****************************************************/


    // ********** ********** SLIDER-VIDEOS ********** **********
    $('.content-slider-video').slick({
        infinite: false,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        // speed: 1500,
        arrows: false,
        dots: false,
        fade: true,
        asNavFor: '.content-slider-miniaturas',
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
    $('.content-slider-miniaturas').slick({
        infinite: false,
        slidesToShow: 4,
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
                slidesToShow: 3
            }
        }, {
            breakpoint: 768,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 4
            }
        },{
            breakpoint: 992,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 4
            }
        },{
            breakpoint: 1200,
            settings: {
                arrows: false,
                dots: false,
                slidesToScroll: 1,
                slidesToShow: 4
            }
        }]
    });
    // ********** ********** /SLIDER-VIDEOS ********** **********



    // ********** ********** GALERIA-SLIDER ********** **********
    $('.content-slider-galeria').slick({
        infinite: true,
        slidesToShow: 2,
        slidesToScroll: 1,
        autoplay: false,
        // speed: 1500,
        arrows: false,
        dots: false,
        prevArrow: '<div class="slick-prev"> <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 256 512"><path fill="currentColor" d="M166.5 424.5l-143.1-152c-4.375-4.625-6.562-10.56-6.562-16.5c0-5.938 2.188-11.88 6.562-16.5l143.1-152c9.125-9.625 24.31-10.03 33.93-.9375c9.688 9.125 10.03 24.38 .9375 33.94l-128.4 135.5l128.4 135.5c9.094 9.562 8.75 24.75-.9375 33.94C190.9 434.5 175.7 434.1 166.5 424.5z" /></svg> </div>',
        nextArrow: '<div class="slick-next"> <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 256 512"><path fill="currentColor" d="M89.45 87.5l143.1 152c4.375 4.625 6.562 10.56 6.562 16.5c0 5.937-2.188 11.87-6.562 16.5l-143.1 152C80.33 434.1 65.14 434.5 55.52 425.4c-9.688-9.125-10.03-24.38-.9375-33.94l128.4-135.5l-128.4-135.5C45.49 110.9 45.83 95.75 55.52 86.56C65.14 77.47 80.33 77.87 89.45 87.5z"></path></svg> </div>',
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
                slidesToShow: 3
            }
        }]
    });
    // ********** ********** /GALERIA-SLIDER ********** **********





    

    /****************************************************/
    /******************** MA **********************/
    /****************************************************/


    // ********** ********** COMENTARIO ********** **********
    
    // ********** ********** /COMENTARIO ********** **********



    
});