using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dti.crud.repositorio
{
    internal class ConexaoBanco
    {
        private static string ConnectionString =
            System.Configuration.ConfigurationManager.
                ConnectionStrings["conn"].ConnectionString;

        internal static SqlConnection CriarConexao()
        {
            SqlConnection conexao = new SqlConnection(ConnectionString);
            return conexao;
        }
    }
}
