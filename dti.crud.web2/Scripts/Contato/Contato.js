(function () {
    'use strict';

    $(document).ready(function () {
        
        listarContatos();

        $('#botaoAbrirInserirContato').on('click', function () {
            abrirInserirContato();
        });

        $('#botaoEditarContatoNomeCancelar').on('click', function () {
            $('#divEditarContatoNome').hide();
        });
        
        $('#botaoInserirContato').on('click', function () {
            inserirContato();
        });

        $('#botaoFiltroContatos').on('click', function () {
            listarContatoPorNome();
        });

        $('#botaoEditarContatoNome').on('click', function () {
            editarContatoNome();
        });

    });

    function listarContatos() { 

        $.ajax({
            type: 'GET',
            url: '/Contato/ListarContatos',
            success: listarContatosRetorno,
            error: function(jqXHR,error, errorThrown) {  
                if (jqXHR.status&&jqXHR.status == 400) {
                    alert(jqXHR.responseText); 
                } else {
                    alert("Erro de comunicação com o servidor!");
                }
            }
        });

    };

    function listarContatosRetorno(lista) {

        if (lista == null) {
            alert("Lista retornada vazia!");
            return;
        }

        var _tabela = $('#tabelaContatos tbody');
        _tabela.empty();

        for (var i = 0; i < lista.length; i++) {

            var _tr = $('<tr></tr>');

            var _td = $('<td></td>');
            _td.text(lista[i].nome);

            var _tdOpcoes = $('<td></td>');

            var _linkEditar = $('<a></a>');
            _linkEditar.text('Editar');
            _linkEditar.attr('data-id', lista[i].id);
            _linkEditar.on('click', function () {
                abrirEditarContatoNome(this);
            });

            var _linkExcluir = $('<a></a>');
            _linkExcluir.text('Excluir');
            _linkExcluir.attr('data-id', lista[i].id);
            _linkExcluir.on('click', function () {
                abrirExcluirContato(this);
            });

            var _linkDetalhes = $('<a></a>');
            _linkDetalhes.text('Detalhes');
            _linkDetalhes.attr('data-id', lista[i].id);
            _linkDetalhes.on('click', function () {
                abrirDetalhesContato(this);
            });

            _tdOpcoes.append(_linkEditar);
            _tdOpcoes.append('&nbsp;');
            _tdOpcoes.append(_linkDetalhes);
            _tdOpcoes.append('&nbsp;');
            _tdOpcoes.append(_linkExcluir);

            _tr.append(_td);
            _tr.append(_tdOpcoes);

            _tabela.append(_tr);
        }

    };

    function abrirInserirContato() {
        $('#divInserirContato').show();
    };

    function abrirEditarContatoNome(_this) {
        $('#editarContatoId').val($(_this).attr('data-id'));
        $('#divEditarContatoNome').show();
    };

    function abrirExcluirContato(_this) {
        alert("Abrir excluir contato!");
    };

    function abrirDetalhesContato(_this) {
        alert("Abrir detalhes contato!");
    };

    function obterContato(_id) {
        alert("Abrir obter contato!");
    };

    function inserirContato() {
        var _parametros = {
            nome: $('#inserirContatoNome').val(),
            nome_grupo: $('#inserirContatoNomeGrupo').val(),
            tipo: $('#inserirContatoTipo').val(),
            telefone: $('#inserirContatoTelefone').val()
        };

        $.ajax({
            type: 'GET',
            url: '/Contato/InserirContato',
            data: _parametros,
            success: inserirContatoRetorno,
            error: function(jqXHR,error, errorThrown) {  
            if (jqXHR.status&&jqXHR.status == 400) {
                alert(jqXHR.responseText); 
            } else {
                alert("Erro de comunicação com o servidor!");
            }
        }
        });
    };

    function inserirContatoRetorno(erro) {

        var mensagem_retorno = new String(erro.mensagem);
        
        if (mensagem_retorno.localeCompare("OK") != 0) alert(erro.mensagem);

        $('#inserirContatoNome').val('');
        $('#inserirContatoNomeGrupo').val('');
        $('#inserirContatoTipo').val('');
        $('#inserirContatoTelefone').val('');

        $('#divInserirContato').hide();

        listarContatos();
    };

    function listarContatoPorNome() {
        var _parametros = {
            nome: $('#inputFiltroContatos').val()
        };

        $.ajax({
            type: 'GET',
            url: '/Contato/ListarContatosPorNome',
            data: _parametros,
            success: listarContatosRetorno,
            error: function (jqXHR, error, errorThrown) {
                if (jqXHR.status && jqXHR.status == 400) {
                    alert(jqXHR.responseText);
                } else {
                    alert("Erro de comunicação com o servidor!");
                }
            }
        });
    };

    function editarContatoNome() {
        var _parametros = {
            id: $('#editarContatoId').val(),
            nome: $('#editarContatoNome').val()
        };

        $.ajax({
            type: 'GET',
            url: '/Contato/EditarContatoNome',
            data: _parametros,
            success: editarContatoNomeRetorno
        });
    };

    function editarContatoNomeRetorno(erro) {
        $('#divEditarContatoNome').hide();

        var mensagem_retorno = new String(erro.mensagem);

        if (mensagem_retorno.localeCompare("OK") != 0) {
            alert(erro.mensagem);
        } else {
            listarContatos();
        }
    };

    function editarContatoTelefoneRetorno(erro) {
        $('#divEditarContatoTelefone').hide();

        var mensagem_retorno = new String(erro.mensagem);

        if (mensagem_retorno.localeCompare("OK") != 0) {
            alert(erro.mensagem);
        } 
    };
}());