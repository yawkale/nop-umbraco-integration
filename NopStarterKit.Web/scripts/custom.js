
$(document).ready(function () {
    $('.header').on('mouseenter', '#topcartlink', function () {
        $('#flyout-cart').addClass('active');
    });
    $('.header').on('mouseleave', '#topcartlink', function () {
        $('#flyout-cart').removeClass('active');
    });
    $('.header').on('mouseenter', '#flyout-cart', function () {
        $('#flyout-cart').addClass('active');
    });
    $('.header').on('mouseleave', '#flyout-cart', function () {
        $('#flyout-cart').removeClass('active');
    });


    $('#small-searchterms').autocomplete({
        delay: 500,
        minLength: 3,
        source: '/catalog/searchtermautocomplete',
        appendTo: '.search-box',
        select: function (event, ui) {
            $("#small-searchterms").val(ui.item.label);
            setLocation(ui.item.producturl);
            return false;
        }
    })
                       .data("ui-autocomplete")._renderItem = function (ul, item) {
                           var t = item.label;
                           //html encode
                           t = htmlEncode(t);
                           return $("<li></li>")
                               .data("item.autocomplete", item)
                               .append("<a><span>" + t + "</span></a>")
                               .appendTo(ul);
                       };

    $('.menu-toggle').click(function () {
        $(this).siblings('.top-menu.mobile').slideToggle('slow');
    });
    $('.top-menu.mobile .sublist-toggle').click(function () {
        $(this).siblings('.sublist').slideToggle('slow');
    });

    $('.thumb-popup-link').magnificPopup(
    {
        type: 'image',
        removalDelay: 300,
        gallery: {
            enabled: true,
            tPrev: 'Previous (Left arrow key)',
            tNext: 'Next (Right arrow key)',
            tCounter: '%curr% of %total%'
        },
        tClose: 'Close (Esc)',
        tLoading: 'Loading...'
    });

    $('.attributes #color-squares-10').delegate('input', 'click', function (event) {
        $('.attributes #color-squares-10').find('li').removeClass('selected-value');
        $(this).closest('li').addClass('selected-value');
    });

    //attribute_change_handler_25();
    $('#product_attribute_9').change(function () { attribute_change_handler_25(); });
    $('#product_attribute_10_25').click(function () { attribute_change_handler_25(); });
    $('#product_attribute_10_26').click(function () { attribute_change_handler_25(); });
    $('#product_attribute_10_27').click(function () { attribute_change_handler_25(); });

    $('#newsletter-subscribe-button').click(function () {
        newsletter_subscribe('true');
    });
    $("#newsletter-email").keydown(function (event) {
        if (event.keyCode == 13) {
            $("#newsletter-subscribe-button").click();
            return false;
        }
    });

    $('.footer-block .title').click(function () {
        var e = window, a = 'inner';
        if (!('innerWidth' in window)) {
            a = 'client';
            e = document.documentElement || document.body;
        }
        var result = { width: e[a + 'Width'], height: e[a + 'Height'] };
        if (result.width < 769) {
            $(this).siblings('.list').slideToggle('slow');
        }
    });

    $('.block .title').click(function () {
        var e = window, a = 'inner';
        if (!('innerWidth' in window)) {
            a = 'client';
            e = document.documentElement || document.body;
        }
        var result = { width: e[a + 'Width'], height: e[a + 'Height'] };
        if (result.width < 1001) {
            $(this).siblings('.listbox').slideToggle('slow');
        }
    });

    $('#vote-poll-1').click(function () {
        var pollAnswerId = $("input:radio[name=pollanswers-1]:checked").val();
        if (typeof (pollAnswerId) == 'undefined') {
            alert('Please select an answer');
        }
        else {
            var voteProgress = $("#poll-voting-progress-1");
            voteProgress.show();
            $.ajax({
                cache: false,
                type: "POST",
                url: "/poll/vote",
                data: { "pollAnswerId": pollAnswerId },
                success: function (data) {
                    voteProgress.hide();

                    if (data.error) {
                        $("#block-poll-vote-error-1").html(data.error);
                        $('#block-poll-vote-error-1').fadeIn("slow").delay(2000).fadeOut("slow");
                    }

                    if (data.html) {
                        $("#poll-block-1").replaceWith(data.html);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('Failed to vote.');
                    voteProgress.hide();
                }
            });
        }
        return false;
    });

});

$(window).load(function () {
    $('#nivo-slider').nivoSlider();
});

$("#small-search-box-form").submit(function (event) {
    if ($("#small-searchterms").val() == "") {
        alert('Please enter some search keyword');
        $("#small-searchterms").focus();
        event.preventDefault();
    }

    $("#addtocart_25_EnteredQuantity").keydown(function (event) {
        if (event.keyCode == 13) {
            $("#add-to-cart-button-25").click();
            return false;
        }
    });


});

//function attribute_change_handler_25() {
//    $.ajax({
//        cache: false,
//        url: '/shoppingcart/productdetails_attributechange?productId=25&validateAttributeConditions=False&loadPicture=True',
//        data: $('#product-details-form').serialize(),
//        type: 'post',
//        success: function (data) {
//            if (data.price) {
//                $('.price-value-25').text(data.price);
//            }
//            if (data.sku) {
//                $('#sku-25').text(data.sku);
//            }
//            if (data.mpn) {
//                $('#mpn-25').text(data.mpn);
//            }
//            if (data.gtin) {
//                $('#gtin-25').text(data.gtin);
//            }
//            if (data.stockAvailability) {
//                $('#stock-availability-value-25').text(data.stockAvailability);
//            }
//            if (data.enabledattributemappingids) {
//                for (var i = 0; i < data.enabledattributemappingids.length; i++) {
//                    $('#product_attribute_label_' + data.enabledattributemappingids[i]).show();
//                    $('#product_attribute_input_' + data.enabledattributemappingids[i]).show();
//                }
//            }
//            if (data.disabledattributemappingids) {
//                for (var i = 0; i < data.disabledattributemappingids.length; i++) {
//                    $('#product_attribute_label_' + data.disabledattributemappingids[i]).hide();
//                    $('#product_attribute_input_' + data.disabledattributemappingids[i]).hide();
//                }
//            }
//            if (data.pictureDefaultSizeUrl) {
//                $('#main-product-img-25').attr("src", data.pictureDefaultSizeUrl);
//            }
//            if (data.pictureFullSizeUrl) {
//                $('#main-product-img-lightbox-anchor-25').attr("href", data.pictureFullSizeUrl);
//            }

//            $.event.trigger({ type: "product_attributes_changed", changedData: data });
//        }
//    });
//}

function newsletter_subscribe(subscribe) {
    var subscribeProgress = $("#subscribe-loading-progress");
    subscribeProgress.show();
    var postData = {
        subscribe: subscribe,
        email: $("#newsletter-email").val()
    };
    $.ajax({
        cache: false,
        type: "POST",
        url: "/subscribenewsletter",
        data: postData,
        success: function (data) {
            subscribeProgress.hide();
            $("#newsletter-result-block").html(data.Result);
            if (data.Success) {
                $('#newsletter-subscribe-block').hide();
                $('#newsletter-result-block').show();
            } else {
                $('#newsletter-result-block').fadeIn("slow").delay(2000).fadeOut("slow");
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to subscribe.');
            subscribeProgress.hide();
        }
    });
}