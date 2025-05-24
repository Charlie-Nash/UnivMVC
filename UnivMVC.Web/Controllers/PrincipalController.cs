using Microsoft.AspNetCore.Mvc;

using UnivMVC.Application.Interfaces;
using UnivMVC.Application.DTOs;
using System.Net;

namespace UnivMVC.Web.Controllers
{
    public class PrincipalController : Controller
    {
        private readonly IAcademicService _academicService;

        public PrincipalController(IAcademicService academicService)
        {
            _academicService = academicService;
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usr")))
            {
                return RedirectToAction("Index", "Auth");
            }

            int personaId = HttpContext.Session.GetInt32("persona_id") ?? 0;
            string nombre_usuario = HttpContext.Session.GetString("nombre_usuario") ?? "";
            string doc_id = HttpContext.Session.GetString("doc_id") ?? "";

            var estudiante = await _academicService.ObtenerEstudianteInfoAsync(personaId);

            if (estudiante.status == HttpStatusCode.InternalServerError)
            {
                ViewBag.Error = "Error de conexión con el servicio académico";

                return View();
            }

            if (estudiante.id <= 0)
            {
                ViewBag.Error = "El usuario no figura como un estudiante registrado";

                return View();
            }

            HttpContext.Session.SetInt32("estudiante_id", estudiante.id);
            HttpContext.Session.SetInt32("semestre_id", estudiante.semestre_id);
            HttpContext.Session.SetInt32("categoria_id", estudiante.categoria_id);
            HttpContext.Session.SetString("sede", estudiante.sede);
            HttpContext.Session.SetString("facultad", estudiante.facultad);
            HttpContext.Session.SetString("programa", estudiante.programa);
            HttpContext.Session.SetString("semestre", estudiante.semestre);
            HttpContext.Session.SetInt32("matricula_id", estudiante.matricula_id);

            ViewBag.Error = "";

            return View();
        }
    }
}
