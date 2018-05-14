//$(window).load(function () {
$(window).on("load", function () {
    alert("Window Loaded");
//$(window, document, undefined).ready(function () {
    $("input[type=text]").each(function () {  // for all input text
        var inputText = $('[name="' + this.name + '"]'); 
            SetClass(inputText);
    });

    $("input[type=password]").each(function () {  // for all input password
        var pwd = $('[name="' + this.name + '"]');
        SetClass(pwd);
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

    function SetClass(e) {
        alert(e.val());
        if (e.val()) {
            e.addClass('used'); 
        } else {
            e.removeClass('used'); 
        }; 
    }
  });

  //var $ripples = $('.ripples');

  //$ripples.on('click.Ripples', function(e) {

  //  var $this = $(this);
  //  var $offset = $this.parent().offset();
  //  var $circle = $this.find('.ripplesCircle');

  //  var x = e.pageX - $offset.left;
  //  var y = e.pageY - $offset.top;

  //  $circle.css({
  //    top: y + 'px',
  //    left: x + 'px'
  //  });

  //  $this.addClass('is-active');

  //});

  //$ripples.on('animationend webkitAnimationEnd mozAnimationEnd oanimationend MSAnimationEnd', function(e) {
  //	$(this).removeClass('is-active');
  //});
