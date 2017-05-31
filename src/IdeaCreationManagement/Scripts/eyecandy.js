$(".disappearing").delay(3000).fadeOut(1000);

$("#EmailConfirmed").change(function () {
    if (this.checked) {
        $("#emailstatus").text("Potwierdzony");
    } else {
        $("#emailstatus").text("Niepotwierdzony");
    }
});

$("#savebutton").click(function() {
    $("#savedialog").css("display", "block");
});

$("#nosavebutton").click(function() {
    $("#savedialog").css("display", "none");
});
