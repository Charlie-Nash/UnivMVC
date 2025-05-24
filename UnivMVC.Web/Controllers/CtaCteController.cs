using Microsoft.AspNetCore.Mvc;
using UnivMVC.Application.Interfaces;

namespace UnivMVC.Web.Controllers
{
    public class CtaCteController : Controller
    {
        private readonly ICtaCteService _ctaCteService;

        public CtaCteController(ICtaCteService ctaCteService)
        {            
            _ctaCteService = ctaCteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            int estudiante_id = HttpContext.Session.GetInt32("estudiante_id") ?? 0;
            int semestre_id = HttpContext.Session.GetInt32("semestre_id") ?? 0;

            if (estudiante_id <= 0 || semestre_id <= 0)
            {
                return RedirectToAction("Index", "Auth");
            }            

            var listaOfertaAcad = await _ctaCteService.ConsultarEstadoCtaCteAsync(estudiante_id, semestre_id);

            return View(listaOfertaAcad);
        }
    }
}
