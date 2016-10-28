(function () {
    'use strict';

    $(document).ready(function () {

        listarContatos();

        $('#botaoAbrirInserirContato').on('click', function () {
            abrirInserirContato();
        });
        
        $('#botaoInserirContato').on('click', function () {
            inserirContato();
        });

    });

    function listarContatos() {
        $.ajax({
            type: 'GET',
            url: '/Contato/ListarContatos',
            success: listarContatosRetorno,
            error: function(jqXHR,error, errorThrown) {  
                if(jqXHR.status&&jqXHR.status==400){
                    alert(jqXHR.responseText); 
                }else{
                    alert("Something went wrong");
                }
            }
        });
    };

    function listarContatosRetorno(lista) {
        
        var _tabela = $('#tabelaContatos tbody');

        _tabela.empty();

        for (var i = 0; i < lista.length; i++) {

            var _tr = $('<tr></tr>');

            var _td = $('<td></td>');
            _td.text(lista[i].nome);
            
            var _td_id_grupo = $('<td></td>');
            _td_id_grupo.text(lista[i].id_grupo);

            var _tdOpcoes = $('<td></td>');
            
            var _linkEditar = $('<a></a>');
            _linkEditar.text('Editar');
            _linkEditar.attr('data-id', lista[i].id);
            _linkEditar.on('click', function () {
                abrirEditarContato(this);
            });

            var _linkExcluir = $('<a></a>');
            _linkExcluir.text('Excluir');
            _linkExcluir.attr('data-id', lista[i].id);
            _linkExcluir.on('click', function () {
                abrirExcluirContato(this);
            });

            _tdOpcoes.append(_linkEditar);
            _tdOpcoes.append('&nbsp;');
            _tdOpcoes.append(_linkExcluir);

            _tr.append(_td);
            _tr.append(_td_id_grupo);
            _tr.append(_tdOpcoes);

            _tabela.append(_tr);
        }

    }

    function abrirInserirContato() {
        $('#divInserirContato').show();
    };

    function inserirContato() {
        var _parametros = {
            nome: $('#inserirContatoNome').val()
        };

        $.ajax({
            type: 'GET',
            url: '/Contato/InserirContato',
            data: _parametros,
            success: inserirContatoRetorno
        });
    };

    function inserirContatoRetorno() {
        $('#divInserirContato').hide();
        listarContatos();
    };

    function abrirEditarContato(_this) {
        obterContato($(_this).attr('data-id'));
        alert("Abrir editar contato!");
    };

    function obterContato(_id) {
        alert("Abrir obter contato!");
    };

    function abrirExcluirContato(_this) {
        alert("Abrir excluir contato!");
    };

}());