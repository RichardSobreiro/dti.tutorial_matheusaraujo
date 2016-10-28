using dti.crud.dto;
using dti.crud.executor;
using System.Collections.Generic;
using System.Web.Mvc;

namespace dti.crud.web2.Controllers
{
    public class GrupoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarGrupos()
        {
            List<Grupo> listaGrupos = GrupoExecutor.ListarGrupos();
            return Json(listaGrupos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarGruposPorNome(string nome)
        {
            List<Grupo> listaGrupos = GrupoExecutor.ListarGruposPorNome(nome);
            return Json(listaGrupos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterGrupo(int id)
        {
            Grupo grupo = GrupoExecutor.ObterGrupo(id);
            return Json(grupo, JsonRequestBehavior.AllowGet);
        }

        public void EditarGrupo(int id, string nome)
        {
            GrupoExecutor.EditarGrupo(id, nome);
        }

        public void InserirGrupo(string nome)
        {
            GrupoExecutor.InserirGrupo(nome);
        }

        public void ExcluirGrupo(int id)
        {
            GrupoExecutor.ExcluirGrupo(id);
        }

    }
}