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
                    ContatoRepositorio.InserirContato(nome, grupo.id);

                    Contato contato = ContatoRepositorio.ObterContato(nome);
                    ContatoRepositorio.InserirContatoRegistro(contato.id, tipo, telefone);

                    erro.mensagem = "OK";
                    return erro;
                }
            }
            else
            {
                erro.mensagem = "Contato já existe!";
                return erro;
            }
        }

    }
}
