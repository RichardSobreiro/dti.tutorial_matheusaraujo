using dti.crud.dto;
using dti.crud.repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dti.crud.executor
{
    public class GrupoExecutor
    {
        public static List<Grupo> ListarGrupos()
        {
            return GrupoRepositorio.ListarGrupos();
        }

        public static List<Grupo> ListarGruposPorNome(string nome)
        {
            return GrupoRepositorio.ListarGruposPorNome(nome);
        }

        public static Grupo ObterGrupo(int id)
        {
            return GrupoRepositorio.ObterGrupo(id);
        }

        public static void EditarGrupo(int id, string nome)
        {
            GrupoRepositorio.EditarGrupo(id, nome);
        }

        public static void InserirGrupo(string nome)
        {
            GrupoRepositorio.InserirGrupo(nome);
        }

        public static void ExcluirGrupo(int id)
        {
            GrupoRepositorio.ExcluirGrupo(id);
        }
    }
}
