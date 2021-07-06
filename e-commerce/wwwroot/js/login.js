const signUpButton = document.getElementById("signUp");
const signInButton = document.getElementById("signIn");
const container = document.getElementById("container");

signUpButton.addEventListener("click", () => {
  container.classList.add("right-panel-active");
});

signInButton.addEventListener("click", () => {
  container.classList.remove("right-panel-active");
});
//=================================================
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
