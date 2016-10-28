using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using dti.crud.dto;
using dti.crud.executor;

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
    }
}