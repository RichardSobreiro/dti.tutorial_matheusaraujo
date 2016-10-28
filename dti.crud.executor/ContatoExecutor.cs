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
    }
}
