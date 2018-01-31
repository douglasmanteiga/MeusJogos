// Início jQuery
$(document).ready(function (e) {
    // evento click no botão login (quando eu clicar no botão login ele vai executar esses comandos)
    $('form').submit(function (e) {
        //$("#btnLogin").click(function (e) { // Ao clicar no botão login do formulário, irá executar os comandos abaixo.

        var vLogin = $("#txtLogin").val(); // Estou recebendo o valor do campo Login
        var vSenha = $("#txtSenha").val(); // Estou recebendo o valor do campo Senha

        e.preventDefault();

        if (vLogin.length <= 0) {

            $("#falhaLogin").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                $('#mensagemErro').html("O campo Login é obrigatório.");
                $("#falhaLogin").delay(1500).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.
            });

            //Foco no campo
            $("#txtLogin").focus();

            //Limpa mensagem
            $('#mensagemErro').html('');

        } else if (vSenha.length <= 0) {

            $("#falhaLogin").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                $('#mensagemErro').html("O campo Senha é obrigatório.");
                $("#falhaLogin").delay(1500).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.
            });

            //Foco
            $("#txtSenha").focus();

            //Limpa mensagem
            $('#mensagemErro').html('');

        } else {

            var data =
            {
                "login": vLogin,
                "senha": vSenha
            };
            $.ajax({
                url: "/Home/ValidarUsuario/",
                async: false,
                type: "POST",
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json",
                success: function (status) {
                    if (status.Mensagem == "Sucess") {
                        window.location.href = "/Home/Index/";
                    }
                    else {

                        $("#falhaLogin").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                            $('#mensagemErro').html(status.Mensagem);
                            $("#falhaLogin").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 2 segundos e escondo.
                        });
                    }
                },
                error: function () {

                    $("#falhaLogin").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                        $('#mensagemErro').html('Erro ao logar no sistema');
                        $("#falhaLogin").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 2 segundos e escondo.
                    });
                }
            });

            //POST>>
            //$.post("/Home/ValidarUsuario/", { login: vLogin, senha: vSenha },
            //    function (retorno) {
            //        if (retorno.retorno == "Sucess") {
            //            alert('Olá');
            //        }
            //        else {
            //            alert('erro' + retorno.retorno);
            //        }
            //    });

            //OK>>
            //    success: function (status) {
            //        if (status.Mensagem == "Sucess") {
            //            window.location.href = "/Home/Index/";
            //        }
            //        //Se não for sucesso a mensagem será setada via razor
            //        //else {
            //        //    //alert('ERRO 1' + status.Mensagem)
            //        //    $('#falhaLogin').html(status.Mensagem);
            //        //}
            //    },
            //    error: function () {
            //        //$('#falhaLogin').html(status.Mensagem);
            //        alert("Erro ao logar no sistema.");
            //    }
            //});



        }
    })

});