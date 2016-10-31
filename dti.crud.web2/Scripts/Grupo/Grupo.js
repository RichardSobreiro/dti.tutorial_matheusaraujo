(function () {
    'use strict';

    $(document).ready(function () {
        listarGrupos();

        $('#botaoFiltroGrupo').on('click', function () {
            listarGruposPorNome();
        });

        $('#botaoEditarGrupo').on('click', function () {
            editarGrupo()
        });

        $('#botaoInserirGrupo').on('click', function () {
            inserirGrupo();
        });

        $('#botaoAbrirInserirGrupo').on('click', function () {
            abrirInserirGrupo();
        });

    });

    function listarGrupos() {
        $.ajax({
            type: 'GET',
            url: '/Grupo/ListarGrupos',
            success: listarGruposRetorno
        });
    };

    function listarGruposRetorno(lista) {

        var _tabela = $('#tabelaGrupos tbody');

        _tabela.empty();
        alert("Comprimento da lista: " + lista.length);
        for (var i = 0; i < lista.length; i++) {

            var _tr = $('<tr></tr>');

            var _td = $('<td></td>');
            _td.text(lista[i].nome);

            var _tdOpcoes = $('<td></td>');

            var _linkEditar = $('<a></a>');
            _linkEditar.text('Editar');
            _linkEditar.attr('data-id', lista[i].id);
            _linkEditar.on('click', function () {
                abrirEditarGrupo(this);
            });

            var _linkExcluir = $('<a></a>');
            _linkExcluir.text('Excluir');
            _linkExcluir.attr('data-id', lista[i].id);
            _linkExcluir.on('click', function () {
                excluirGrupo(this);
            });

            _tdOpcoes.append(_linkEditar);
            _tdOpcoes.append('&nbsp;');
            _tdOpcoes.append(_linkExcluir);

            _tr.append(_td);
            _tr.append(_tdOpcoes);

            _tabela.append(_tr);
        }

    };

    function listarGruposPorNome() {
        
        var _parametros = {
            nome: $('#inputFiltroGrupo').val()
        };
        
        $.ajax({
            type: 'GET',
            url: '/Grupo/ListarGruposPorNome',
            data: _parametros,
            success: listarGruposRetorno
        });
    };

    function obterGrupo(_id) {
        var _parametros = {
            id: _id
        };

        $.ajax({
            type: 'GET',
            url: '/Grupo/ObterGrupo',
            data: _parametros,
            success: obterGrupoRetorno
        });
    };

    function obterGrupoRetorno(data) {
        $('#editarGrupoId').val(data.id);
        $('#editarGrupoNome').val(data.nome);
        $('#divEditarGrupo').show();
    };

    function editarGrupo() {
        var _parametros = {
            id: $('#editarGrupoId').val(),
            nome: $('#editarGrupoNome').val()
        };

        $.ajax({
            type: 'GET',
            url: '/Grupo/EditarGrupo',
            data: _parametros,
            success: editarGrupoRetorno
        });
    };

    function editarGrupoRetorno() {
        $('#divEditarGrupo').hide();
        listarGrupos();
    }

    function inserirGrupo() {
        var _parametros = {
            nome: $('#inserirGrupoNome').val()
        };

        $.ajax({
            type: 'GET',
            url: '/Grupo/InserirGrupo',
            data: _parametros,
            success: inserirGrupoRetorno
        });
    }

    function inserirGrupoRetorno() {
        $('#divInserirGrupo').hide();
        listarGrupos();
    }

    function excluirGrupo(_this) {
        var _parametros = {
            id: $(_this).attr('data-id')
        };

        $.ajax({
            type: 'GET',
            url: '/Grupo/ExcluirGrupo',
            data: _parametros,
            success: excluirGrupoRetorno
        });
    }

    function excluirGrupoRetorno() {
        listarGrupos();
    }

    function abrirEditarGrupo(_this) {
        obterGrupo($(_this).attr('data-id'));
    };

    function abrirInserirGrupo() {
        $('#divInserirGrupo').show();
    };

}());