using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dti.crud.dto;
using dti.crud.repositorio;

namespace dti.crud.executor
{
    public class ContatoExecutor
    {
        public static List<Contato> ListarContatos()
        {
            return ContatoRepositorio.ListarContatos();
        }

        public static Erro InserirContato(string nome, string nome_grupo, string tipo, string telefone)
        {
            Erro erro = new Erro();
            int num_linhas_afetadas;

            Contato contato_duplo = ContatoRepositorio.ObterContato(nome);
            if(contato_duplo == null)
            {
                Grupo grupo = ContatoRepositorio.ObterGrupo(nome_grupo);
                if (grupo == null)
                {
                    erro.mensagem = "Grupo não existe!";
                    return erro;
                }
                else
                {
                    num_linhas_afetadas = ContatoRepositorio.InserirContato(nome, grupo.id);
                    if (num_linhas_afetadas == 1)
                    {
                        Contato contato = ContatoRepositorio.ObterContato(nome);
                        ContatoRepositorio.InserirContatoRegistro(contato.id, tipo, telefone);

                        erro.mensagem = "OK";
                        return erro;
                    } else if (num_linhas_afetadas == 0)
                    {
                        erro.mensagem = "Erro na inserção do contato!";
                        return erro;
                    } else
                    {
                        erro.mensagem = "Erro no servidor (catch exception)!";
                        return erro;
                    }
                }
            }
            else
            {
                erro.mensagem = "Contato já existe!";
                return erro;
            }
        }

        public static List<Contato> ListarContatosPorNome(string nome)
        {
            return ContatoRepositorio.ListarContatosPorNome(nome);
        }

        public static Erro EditarContatoNome(int id, string nome)
        {
            Erro erro = new Erro();
            int num_linhas_afetadas = ContatoRepositorio.EditarContatoNome(id, nome);

            if(num_linhas_afetadas == 0)
            {
                erro.mensagem = "Erro na alteração do nome! (banco)";
            }
            else if (num_linhas_afetadas == 1)
            {

            }

            return erro;
        }
    }
}
