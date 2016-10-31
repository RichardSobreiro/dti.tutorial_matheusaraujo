using dti.crud.dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dti.crud.repositorio
{
    public class ContatoRepositorio
    {
        #region Constantes

        private const string LISTAR_CONTATOS = @"
            SELECT 
                c.id,
                c.id_grupo,
                c.nome
            FROM
                contato c
            ORDER BY
                c.nome
            ";

        private const string INSERIR_CONTATO = @"
            INSERT INTO contato (nome,
                                id_grupo)
            VALUES (@nome,
                    @id_grupo)
        ";

        private const string INSERIR_CONTATO_REGISTRO = @"
            INSERT INTO contato_registro ( id_contato, 
                                           tipo, 
                                           telefone )
            VALUES ( @id_contato,
                     @tipo,
                     @telefone )
        ";

        private const string OBTER_GRUPO = @"
            SELECT
                g.id,
                g.nome
            FROM
                grupo g
            WHERE
                g.nome = @nome
        ";

        private const string OBTER_GRUPO_POR_ID = @"
            SELECT
                g.id,
                g.nome
            FROM
                grupo g
            WHERE
                g.id = @id
        ";

        private const string OBTER_CONTATO = @"
            SELECT
                c.id,
                c.id_grupo,
                c.nome
            FROM
                contato c
            WHERE
                c.nome = @nome
        ";

        public const string LISTAR_CONTATOS_POR_NOME = @"
            SELECT
                c.id,
                c.id_grupo,
                c.nome
            FROM
                contato c
            WHERE
                c.nome like '%' + @nome + '%'
            ORDER BY
                c.nome
        ";

        private const string EDITAR_NOME_CONTATO = @"
            UPDATE contato
            SET nome = @nome
            WHERE id = @id
        ";

        private const string EXCLUIR_CONTATO = @"
            DELETE contato 
            WHERE id = @id
        ";

        #endregion

        #region Métodos Privados

        private static Contato ConverterContato(SqlDataReader linhaBancoDados)
        {
            Contato contato = new Contato();
            contato.id = int.Parse(linhaBancoDados["id"].ToString());

            contato.id_grupo = int.Parse(linhaBancoDados["id_grupo"].ToString());
            if (contato.id_grupo == 0) contato.id_grupo = 0;

            contato.nome = linhaBancoDados["nome"].ToString();
            if (contato.nome.Equals(null) || contato.nome.Equals("")) contato.nome = "ERRO: NULL";

            return contato;
        }

        private static Grupo ConverterGrupo(SqlDataReader linhaBancoDados)
        {
            Grupo grupo = new Grupo();
            grupo.id = int.Parse(linhaBancoDados["id"].ToString());
            grupo.nome = linhaBancoDados["nome"].ToString();
            return grupo;
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

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Contato contato = ConverterContato(leitor);
                        listaContatos.Add(contato);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                return null;
            }

            return listaContatos;
        }

        public static Grupo ObterGrupoPorId(int id)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            try
            {
                conexao.Open();

                SqlCommand comando = new SqlCommand(OBTER_GRUPO_POR_ID, conexao);
                comando.Parameters.AddWithValue("id", id);

                SqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    return null;
                }
                else
                {
                    leitor.Read();
                    Grupo grupo = ConverterGrupo(leitor);
                    return grupo;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                return null;
            }
        }

        public static Grupo ObterGrupo(string nome)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            try
            {
                conexao.Open();

                SqlCommand comando = new SqlCommand(OBTER_GRUPO, conexao);
                comando.Parameters.AddWithValue("nome", nome);

                SqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    return null;
                }
                else
                {
                    leitor.Read();
                    Grupo grupo = ConverterGrupo(leitor);
                    return grupo;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                return null;
            }
        }

        public static Contato ObterContato(string nome)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            try
            {
                conexao.Open();

                SqlCommand comando = new SqlCommand(OBTER_CONTATO, conexao);
                comando.Parameters.AddWithValue("nome", nome);

                SqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    return null;
                }
                else
                {
                    leitor.Read();
                    Contato contato = ConverterContato(leitor);
                    return contato;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                return null;
            }
        }

        public static int InserirContato(string nome, int id_grupo)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();
            int num_linhas_afetadas;

            try
            {
                conexao.Open();

                SqlCommand comando = new SqlCommand(INSERIR_CONTATO, conexao);
                comando.Parameters.AddWithValue("nome", nome);
                comando.Parameters.AddWithValue("id_grupo", id_grupo);

                num_linhas_afetadas = comando.ExecuteNonQuery();

                return num_linhas_afetadas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                return -1;
            }
        }

        public static Erro InserirContatoRegistro(int id_contato, string tipo, string telefone)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();
            Erro erro = new Erro();

            try
            {
                conexao.Open();

                SqlCommand comando = new SqlCommand(INSERIR_CONTATO_REGISTRO, conexao);
                comando.Parameters.AddWithValue("id_contato", id_contato);
                comando.Parameters.AddWithValue("tipo", tipo);
                comando.Parameters.AddWithValue("telefone", telefone);
                comando.ExecuteNonQuery();

                erro.mensagem = "OK";
                return erro;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                return null;
            }
        }

        public static List<Contato> ListarContatosPorNome(string nome)
        {
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            List<Contato> listaContato = new List<Contato>();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(LISTAR_CONTATOS_POR_NOME, conexao);
                comando.Parameters.AddWithValue("nome", nome);
                SqlDataReader leitor = comando.ExecuteReader();

                if (leitor.HasRows)
                { 
                    while (leitor.Read())
                    {
                        Contato contato = ConverterContato(leitor);
                        listaContato.Add(contato);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                return null;
            }

            return listaContato;
        }

        public static int EditarContatoNome(int id, string nome)
        {
            int num_linhas_afetadas = 0;
            SqlConnection conexao = ConexaoBanco.CriarConexao();

            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand(EDITAR_NOME_CONTATO, conexao);
                comando.Parameters.AddWithValue("nome", nome);
                comando.Parameters.AddWithValue("id", id);
                num_linhas_afetadas = comando.ExecuteNonQuery();

                return num_linhas_afetadas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
                return -1;
            }
        }

        #endregion
    }
}
