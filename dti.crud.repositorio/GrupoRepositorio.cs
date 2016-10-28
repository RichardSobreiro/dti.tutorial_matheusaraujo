using dti.crud.dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dti.crud.repositorio
{
    public class GrupoRepositorio
    {

        #region Constantes 

        private const string LISTAR_GRUPOS = @"
            SELECT
                g.id,
                g.nome
            FROM
                grupo g
            ORDER BY
                g.nome
            ";

        private const string LISTAR_GRUPOS_POR_NOME = @"
            SELECT
                g.id,
                g.nome
            FROM
                grupo g
            WHERE
                g.nome like '%' + @nome + '%'
            ORDER BY
                g.nome
            ";

        private const string OBTER_GRUPO = @"
            SELECT
                g.id,
                g.nome
            FROM
                grupo g
            WHERE
                g.id = @id
        ";

        private const string EDITAR_GRUPO = @"
            UPDATE grupo
            SET nome = @nome
            WHERE id = @id
        ";

        private const string INSERIR_GRUPO = @"
            INSERT INTO grupo (nome) 
            VALUES (@nome)
        ";

        private const string EXCLUIR_GRUPO = @"
            DELETE grupo
            WHERE id = @id
        ";

        #endregion

        #region Métodos Privados

        private static Grupo ConverterGrupo(SqlDataReader linhaBancoDados)
        {
            Grupo grupo = new Grupo();
            grupo.id = int.Parse(linhaBancoDados["id"].ToString());
            grupo.nome = linhaBancoDados["nome"].ToString();
            return grupo;
        }

        #endregion

        #region Métodos Públicos

        public static List<Grupo> ListarGrupos()
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            List<Grupo> listaGrupos = new List<Grupo>();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(LISTAR_GRUPOS, conexao);

                SqlDataReader leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    Grupo grupo = ConverterGrupo(leitor);
                    listaGrupos.Add(grupo);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return listaGrupos;
        }

        public static List<Grupo> ListarGruposPorNome(string nome)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            List<Grupo> listaGrupos = new List<Grupo>();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(LISTAR_GRUPOS_POR_NOME, conexao);
                comando.Parameters.AddWithValue("nome", nome);
                SqlDataReader leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    Grupo grupo = ConverterGrupo(leitor);
                    listaGrupos.Add(grupo);
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return listaGrupos;
        }

        public static Grupo ObterGrupo(int id)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(OBTER_GRUPO, conexao);
                comando.Parameters.AddWithValue("id", id);

                SqlDataReader leitor = comando.ExecuteReader();
                leitor.Read();
                Grupo grupo = ConverterGrupo(leitor);

                return grupo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void EditarGrupo(int id, string nome)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(EDITAR_GRUPO, conexao);
                comando.Parameters.AddWithValue("id", id);
                comando.Parameters.AddWithValue("nome", nome);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {   
            }
        }

        public static void InserirGrupo(string nome)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(INSERIR_GRUPO, conexao);
                comando.Parameters.AddWithValue("nome", nome);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
        }

        public static void ExcluirGrupo(int id)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(EXCLUIR_GRUPO, conexao);
                comando.Parameters.AddWithValue("id", id);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        
    }
}
