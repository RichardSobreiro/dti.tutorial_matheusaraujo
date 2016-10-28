﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using dti.crud.dto;

namespace dti.crud.repositorio
{
    public class ContatoRepositorio
    {
        #region Constantes

        private const string LISTAR_CONTATOS = @"
            SELECT 
                c.id
                c.id_grupo
                c.nome
            FROM
                contato c
            ORDER BY
                c.nome
            ";

        #endregion

        #region Métodos Privados

        private static Contato ConverterContato(SqlDataReader linhaBancoDados)
        {
            Contato contato = new Contato();
            contato.id = int.Parse(linhaBancoDados["id"].ToString());
            contato.id_grupo = int.Parse(linhaBancoDados["id_grupo"].ToString());
            contato.nome = linhaBancoDados["nome"].ToString();
            return contato;
        }

        #endregion

        #region Métodos Públicos

        public static List<Contato> ListarContatos()
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            List<Contato> listaContatos = new List<Contato>();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(LISTAR_CONTATOS, conexao);

                SqlDataReader leitor = comando.ExecuteReader();

                while(leitor.Read())
                {
                    Contato contato = ConverterContato(leitor);
                    listaContatos.Add(contato);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return listaContatos;
        }

        #endregion
    }
}