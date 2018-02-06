// Início jQuery
$(document).ready(function (e) {
    // evento click no botão login (quando eu clicar no botão login ele vai executar esses comandos)
    $('form').submit(function (e) {
        //$("#btnLogin").click(function (e) { // Ao clicar no botão login do formulário, irá executar os comandos abaixo.

        var vLogin = $("#txtLogin").val(); // Estou recebendo o valor do campo Login
        var vSenha = $("#txtSenha").val(); // Estou recebendo o valor do campo Senha
        var vSenhaConfirmar = $("#txtSenhaConfirmar").val(); 

        e.preventDefault();

        if (vLogin.length <= 0) {

            $("#falhaCadastrar").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                $('#mensagemErro').html("O campo Usuário é obrigatório.");
                $("#falhaCadastrar").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.
            });

            //Foco no campo
            $("#txtLogin").focus();

            //Limpa mensagem
            $('#mensagemErro').html('');

        } else if (vSenha.length <= 0) {

            $("#falhaCadastrar").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                $('#mensagemErro').html("O campo Senha é obrigatório.");
                $("#falhaCadastrar").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.
            });

            //Foco
            $("#txtSenha").focus();

            //Limpa mensagem
            $('#mensagemErro').html('');

        }
        else if (vSenhaConfirmar.length <= 0) {

            $("#falhaCadastrar").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                $('#mensagemErro').html("O campo Confirmar Senha é obrigatório.");
                $("#falhaCadastrar").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.
            });

            //Foco
            $("#txtSenhaConfirmar").focus();

            //Limpa mensagem
            $('#mensagemErro').html('');

        }
        else if (vSenha != vSenhaConfirmar) {

            $("#falhaCadastrar").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                $('#mensagemErro').html("As senhas informadas não conferem.");
                $("#falhaCadastrar").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.
            });

            //Foco
            $("#txtSenhaConfirmar").focus();

            //Limpa mensagem
            $('#mensagemErro').html('');

        } else {

            var posting = $.post('/Usuario/CreateUsuarioNaoLogado/', { usuario: vLogin, senha: vSenha, confirmarSenha: vSenhaConfirmar });

            // Put the results in a div
            posting.done(function (data) {

                if (data.Mensagem == "Sucess") {

                    $("#sucessoCadastrar").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                        $('#mensagemSucesso').html("Usuário e Senha cadastrado com sucesso, faça o login para acessar o sistema.");
                        $("#sucessoCadastrar").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 1,5 segundos e escondo.                        
                    });

                    setTimeout(function () {
                        window.location.href = "/Home/Login/";
                    }, 2000);
                }
                else {

                    $("#falhaCadastrar").fadeIn("fast", function (e) { // Exibir o bloco de mensagem de erro
                        $('#mensagemErro').html(data.Mensagem);
                        $("#falhaCadastrar").delay(2000).fadeOut("fast"); // Depois de exibido, eu dou um tempo de 2 segundos e escondo.
                    });
                }

            });

            posting.fail(function () {

                $("#falhaCadastrar").fadeIn("fast", function (e) {
                    $('#mensagemErro').html('Erro ao cadastrar o usuário.');
                    $("#falhaCadastrar").delay(2000).fadeOut("fast");
                });

            });
        }
    })

});