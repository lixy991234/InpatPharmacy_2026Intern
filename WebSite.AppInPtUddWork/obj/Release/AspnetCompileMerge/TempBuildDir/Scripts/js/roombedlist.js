const search = [];
var search_text = "";
$(".search1,.search1_on").click(function () {
    $(this).toggleClass("search1");
    $(this).toggleClass("search1_on");
    if ($(this).hasClass("search1_on")) {
        search.push("管");
    } else {
        search.splice($.inArray('管', search), 1);
    }
    search_items();
})
$(".search2,.search2_on").click(function () {
    $(this).toggleClass("search2");
    $(this).toggleClass("search2_on");
    if ($(this).hasClass("search2_on")) {
        search.push("警");
    } else {
        search.splice($.inArray('警', search), 1);
    }
    search_items();
})
$(".search3,.search3_on").click(function () {
    $(this).toggleClass("search3");
    $(this).toggleClass("search3_on");
    if ($(this).hasClass("search3_on")) {
        search.push("冰");
    } else {
        search.splice($.inArray('冰', search), 1);
    }
    search_items();
})
$(".search4,.search4_on").click(function () {
    $(this).toggleClass("search4");
    $(this).toggleClass("search4_on");
    if ($(this).hasClass("search4_on")) {
        search.push("DC");
    } else {
        search.splice($.inArray('DC', search), 1);
    }
    search_items();
})
$(".search5,.search5_on").click(function () {
    $(this).toggleClass("search5");
    $(this).toggleClass("search5_on");
    if ($(this).hasClass("search5_on")) {
        search.push("補");
    } else {
        search.splice($.inArray('補', search), 1);
    }
    search_items();
})
$(".search6,.search6_on").click(function () {
    $(this).toggleClass("search6");
    $(this).toggleClass("search6_on");
    if ($(this).hasClass("search6_on")) {
        search.push("貴");
    } else {
        search.splice($.inArray('貴', search), 1);
    }
    search_items();
})
$(".form_submit").click(function () {
    $(this).find("form").submit();
})
$(".back_img").click(function () {
    window.location.href = "StationList";
})
$(".search4").click(function () {
    $(".col-10 .p_mark").filter(function () {
        if ($(this).text() == "DC") {

        } else {
            $(this).parents(".form_submit").toggleClass("form_submit_none");
        }
    });

})

$(document).ready(function () {
    $("#search_input").on("keyup", function () {
        search_text = $(this).val().toLowerCase();
        search_items();

    });
});

function search_items() {
    var block_item;
    var i = 0;
    var form_sub_tags;
    var form_sub_texts;
    $(".form_submit").filter(function () {
        block_item = true;
        i += 1;
        form_sub_tags = $(this).find(".item_tag");
        form_sub_texts = $(this).find(".item_text");
        $.each(search, function (key, value) {
            if (!form_sub_tags.hasClass(value)) {
                block_item = false;
            }
        })
        if (!(form_sub_texts.attr("class").toLowerCase().indexOf(search_text) > -1)) {
            block_item = false;
        }
        if (block_item == false) {
            $(this).css("display", "none");

        } else {
            $(this).css("display", "block");
        }
    });
}
