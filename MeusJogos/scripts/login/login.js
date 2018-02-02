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
                $("#falhaLogin").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.
            });

            //Foco no campo
            $("#txtLogin").focus();

            //Limpa mensagem
            $('#mensagemErro').html('');

        } else if (vSenha.length <= 0) {

            $("#falhaLogin").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                $('#mensagemErro').html("O campo Senha é obrigatório.");
                $("#falhaLogin").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.
            });

            //Foco
            $("#txtSenha").focus();

            //Limpa mensagem
            $('#mensagemErro').html('');

        } else {

            var posting = $.post('/Home/ValidarUsuario/', { login: vLogin, senha: vSenha});

            // Put the results in a div
            posting.done(function (data) {

                if (data.Mensagem == "Sucess") {
                    window.location.href = "/Home/Index/";
                }
                else {

                    $("#falhaLogin").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                        $('#mensagemErro').html(data.Mensagem);
                        $("#falhaLogin").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 2 segundos e escondo.
                    });
                }

            });

            posting.fail(function () {

                $("#falhaLogin").fadeIn("fast", function (e) {
                    $('#mensagemErro').html('Erro ao logar no sistema');
                    $("#falhaLogin").delay(2000).fadeOut("fast"); 
                });

            });

            //Sempre executa
            //posting.always(function (data) {
            //    if (data.Mensagem == "Sucess") {
            //        window.location.href = "/Home/Index/";
            //    }
            //});


            //Funcionando >> porém async false está obsoleto
            //var data =
            //{
            //    "login": vLogin,
            //    "senha": vSenha
            //};
            //$.ajax({
            //    url: "/Home/ValidarUsuario/",
            //    async: false,
            //    type: "POST",
            //    data: JSON.stringify(data),
            //    dataType: "json",
            //    contentType: "application/json",
            //    success: function (status) {
            //        if (status.Mensagem == "Sucess") {
            //            window.location.href = "/Home/Index/";
            //        }
            //        else {

            //            $("#falhaLogin").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
            //                $('#mensagemErro').html(status.Mensagem);
            //                $("#falhaLogin").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 2 segundos e escondo.
            //            });
            //        }
            //    },
            //    error: function () {

            //        $("#falhaLogin").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
            //            $('#mensagemErro').html('Erro ao logar no sistema');
            //            $("#falhaLogin").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 2 segundos e escondo.
            //        });
            //    }
            //});
        }
    })

});