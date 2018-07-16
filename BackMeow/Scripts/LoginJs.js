//$(window).load(function () {
$(window).on("load", function () {

    $("input[type=text]").each(function () {  // for all input text
        this.classList.add("used");
        var inputText = $('[name="' + this.name + '"]');
        SetClass($(this));
    });
    //$(window).ready(function () {
    //$(window, document, undefined).ready(function () {
    $("input[type=password]").each(function () {  // for all input password
        var pwd = $('[name="' + this.name + '"]');
        SetClass($(this));
    });

    $('input').keyup(function () {
        SetClass($(this));
    });

    //$('input').blur(function() {
    //    var $this = $(this);
    //    if ($this.val()) {
    //        $this.addClass('used');
    //    }
    //    else {
    //        $this.removeClass('used');
    //    }
});

function SetClass(e) {
    //if (e.val()) {
         e.addClass('used');
    //} else {
    //  e.removeClass('used');
    //};
} 