$(function () {
    $(window).hover(function (e) {
        $('#logo-hover').addClass('rollIn animated');
        $('#menu-hover').addClass('lightSpeedIn animated');
        $('#cart-hover').addClass('bounceInDown animated');
        $('.top-header').addClass('rotateInDownLeft animated');
    });

    $(window).click(function (e) {
        $('#logo-hover').removeClass('rollIn animated');
        $('#menu-hover').removeClass('lightSpeedIn animated');
        $('#cart-hover').removeClass('bounceInDown animated');
    });

    $(window).on('load', function (e) {
        $('body').removeClass('preloading');
        $('.load').delay(1000).fadeOut('200');
    });
    $(window).scroll(function (event) {
        var page1 = pageYOffset;
        var i;
          console.log(page1);
        if (page1 >= 275) {
            $('#logo-hover').removeClass('rollIn animated');
            $('#menu-hover').removeClass('menu-custom lightSpeedIn');
            $('#menu-hover').addClass('menu-small zoomIn animated');
            $("#smallsearch").css({ "padding-right": "0px" });
            $("#smallsearch").addClass('flip animated');
            $('.top-header').removeClass('rotateInDownLeft animated');
        }
        if (page1 < 270) {
          
            $('#menu-hover').removeClass('menu-small zoomIn');
            $('#menu-hover').addClass('menu-custom lightSpeedIn animated')
            $("#smallsearch").css({ "padding-right": "350px" });
            $("#smallsearch").removeClass('flip animated');
            $("#smallsearch").addClass('rollIn animated');
        }
        if (page1 == 0) {
            $('#logo-hover').addClass('rollIn animated');
            $('.top-header').addClass('rotateInDownLeft animated');
        }
        if (page1 >= 700) {
            $('#myCarousel').removeClass('zoomIn animated');
        }
        if (page1 < 400) {
            $('#myCarousel').addClass('zoomIn animated');
        }    
        if (page1 >= 390) {
            $('#youtube').addClass('slideInLeft animated');
            $('#image').addClass('slideInRight animated');
        }
        if (page1 == 0) {
            $('#youtube').removeClass('slideInLeft animated');
            $('#image').removeClass('slideInRight animated');
        }
        if (page1 >= 1350) {
            for (i = 1; i < 5; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('bottotop');
            }
        }
        if (page1 >= 1600) {
            for (i = 5; i < 9; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('bottotop');
            }
        }
        if (page1 >= 1850) {
            for (i = 9; i < 13; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('bottotop');
            }
        }
        if (page1 >= 2100) {
            for (i = 13; i < 17; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('bottotop');
            }
        }
        if (page1 >= 2350) {
            for (i = 17; i < 21; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('bottotop');
            }
        }
        if (page1 >= 3950) {
            for (i = 21; i < 25; i++) {
                var animation = "#animation" + i;
                if (i < 23)
                    $(animation).addClass('rotateInDownLeft animated');
                if (i >= 23) $(animation).addClass('rotateInDownRight animated');
            }
        }
        if (page1 >= 4250) {
            for (i = 25; i < 29; i++) {
                var animation = "#animation" + i;
                if (i < 27)
                    $(animation).addClass('rotateInDownLeft animated');
                if (i >= 27) $(animation).addClass('rotateInDownRight animated');
            }
        }

        if (page1 >= 4550) {
            for (i = 29; i < 33; i++) {
                var animation = "#animation" + i;
                if (i < 31)
                    $(animation).addClass('rotateInDownLeft animated');
                else $(animation).addClass('rotateInDownRight animated');
            }
        }

        if (page1 >= 4850) {
            for (i = 33; i < 37; i++) {
                var animation = "#animation" + i;
                if (i < 35)
                    $(animation).addClass('rotateInDownLeft animated');
                else $(animation).addClass('rotateInDownRight animated');
            }
        }
        if (page1 >= 6450) {
            for (i = 37; i < 41; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('zoomInLeft animated');
            }
        }
        if (page1 >= 6750) {
            for (i = 41; i < 45; i++) {
                var animation = "#animation" + i;
                if (i < 41)
                    $(animation).addClass('zoomInRight animated');
                else $(animation).addClass('zoomInRight animated');
            }
        }
        if (page1 >= 7050) {
            for (i = 45; i < 49; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('zoomInLeft animated');
            }
        }
        if (page1 >= 7350) {
            for (i = 49; i < 53; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('zoomInRight animated');
            }
        }
        if (page1 >= 7650) {
            for (i = 53; i < 57; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('slideInDown animated');
            }
        }
        if (page1 >= 7950) {
            for (i = 57; i < 61; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('slideInDown animated');
            }
        }
        if (page1 >= 8250) {
            for (i = 61; i < 65; i++) {
                var animation = "#animation" + i;
                $(animation).addClass('slideInDown animated');
            }
        }
    });
});