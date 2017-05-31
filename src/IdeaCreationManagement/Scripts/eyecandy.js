$(".disappearing").delay(3000).fadeOut(1000);

$("#EmailConfirmed").change(function () {
    if (this.checked) {
        $("#emailstatus").text("Potwierdzony");
    } else {
        $("#emailstatus").text("Niepotwierdzony");
    }
});