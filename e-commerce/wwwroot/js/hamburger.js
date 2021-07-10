$("input").on("click", function () {
  if ($(this).is(":checked")) {
    $(".carousel-control-prev").css("z-index", "auto");
    $(".carousel-control-next").css("z-index", "auto");
    $(".pointer-event").css("touch-action", "auto");
  } else {
    $(".carousel-control-prev").css("z-index", "1");
    $(".carousel-control-next").css("z-index", "1");
    $(".pointer-event").css("touch-action", "pan-y");
  }
});
$(".search-box").on("click", function () {
  $(".search-box").css("background", "#6D757A");
});

$(function () {
  var Accordion = function (el, multiple) {
    this.el = el || {};
    this.multiple = multiple || false;

    var links = this.el.find(".article-title");
    links.on(
      "click",
      {
        el: this.el,
        multiple: this.multiple,
      },
      this.dropdown
    );
  };

  Accordion.prototype.dropdown = function (e) {
    var $el = e.data.el;
    ($this = $(this)), ($next = $this.next());

    $next.slideToggle();
    $this.parent().toggleClass("open");

    if (!e.data.multiple) {
      $el
        .find(".accordion-content")
        .not($next)
        .slideUp()
        .parent()
        .removeClass("open");
    }
  };
  var accordion = new Accordion($(".accordion-container"), false);
});
//$(".fa-heart").on("click", function () {
//    $(this).toggle().css("color", "red");
//})