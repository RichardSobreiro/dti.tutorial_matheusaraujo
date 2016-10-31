using dti.crud.dto;
using dti.crud.executor;
using System.Collections.Generic;
using System.Web.Mvc;

namespace dti.crud.web2.Controllers
{
    public class ContatoController : Controller
    {
        // GET: Contato
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarContatos()
        {
            List<Contato> listaContatos = ContatoExecutor.ListarContatos();
            return Json(listaContatos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InserirContato(string nome, string nome_grupo, string tipo, string telefone)
        {
            Erro erro = ContatoExecutor.InserirContato(nome, nome_grupo, tipo, telefone);
            return Json(erro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarContatosPorNome(string nome)
        {
            List<Contato> listaContatos = ContatoExecutor.ListarContatosPorNome(nome);
            return Json(listaContatos, JsonRequestBehavior.AllowGet);
        }
    }
}