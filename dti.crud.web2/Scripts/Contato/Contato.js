(function () {
    'use strict';

    $(document).ready(function () {
        listarContatos();

    });

    function listarContatos() {
        $.ajax({
            type: 'GET',
            url: '/Contato/ListarContatos',
            success: listarContatosRetorno
        });
    };

    function listaContatosRetorno(lista) {
        
        var _tabela = $('.tabela');

        _tabela.empty();

        for (var i = 0; i < lista.length; i++) {
            var _tr = $('<tr></tr>');

            var _td = $('<td></td>');
            _td.text(lista.)
        }

    }

}());