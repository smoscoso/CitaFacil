async function pagar() {
    //obtener valor de texto input
    console.log("Carga del script");
    var body = {
        precio: $("#precio").val(),
        producto: "Suscripcion estandar"
    }

    const response = await $.ajax({
        // metodo que recibe del controlador
        url: '@Url.Action("Paypal", "ServicioPagoController")',
        type: "POST",
        data: JSON.stringify(body),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: async function (data) {
            console.log(data);
            $("div.jumbotron").LoadingOverlay("hide");
            if (data.status) {
                var jsonresult = await JSON.parse(data.respuesta);
                console.log(jsonresult);
                var links = jsonresult.links;
                var resultado = links.find(item => item.rel === "approve");
                window.location.href = resultado.href
                /*console.log(links)*/
                /*console.log(resultado)*/
            } else {
                alert("Vuelva a intentarlo más tarde")
            }
        },
        beforeSend: function () {
            $("div.jumbotron").LoadingOverlay("show");
        }
    });
}