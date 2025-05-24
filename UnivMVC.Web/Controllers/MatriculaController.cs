using Microsoft.AspNetCore.Mvc;
using UnivMVC.Application.Interfaces;
using UnivMVC.Application.DTOs;

namespace UnivMVC.Web.Controllers
{
    public class MatriculaController : Controller
    {
        private readonly IAcademicService _academicService;
        private readonly ICtaCteService _ctaCteService;

        public MatriculaController(IAcademicService academicService, ICtaCteService ctaCteService)
        {
            _academicService = academicService;
            _ctaCteService = ctaCteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<int> horariosMat = new();

            int estudiante_id = HttpContext.Session.GetInt32("estudiante_id") ?? 0;
            int semestre_id = HttpContext.Session.GetInt32("semestre_id") ?? 0;
            int categoria_id = HttpContext.Session.GetInt32("categoria_id") ?? 0;

            int matriculaId = HttpContext.Session.GetInt32("matricula_id") ?? 0;

            string error = TempData["matricularme_error"]?.ToString() ?? "";

            if (estudiante_id <= 0)
            {
                return RedirectToAction("Index", "Auth");
            }

            ViewBag.Error = error;

            var pagoMatricula = await _ctaCteService.ConsultarPagoMatriculaAsync(estudiante_id, semestre_id, categoria_id);

            if (pagoMatricula == null) {
                ViewBag.Error = "No se pudo confirmar el pago por el derecho de matrícula.";
                return View();
            }

            if (pagoMatricula.respuesta <= 0)
            {
                ViewBag.Error = pagoMatricula.mensaje;
                return View();
            }

            var listaOfertaAcad = await _academicService.ObtenerOfertaAcademicaAsync(estudiante_id);
            var listaCursosMat = await _academicService.ObtenerCursosMatriculadosAsync(matriculaId);

            foreach (var cursoMat in listaCursosMat)
            {
                horariosMat.Add(cursoMat.id);
            }

            ViewBag.HorariosMat = horariosMat;

            return View(listaOfertaAcad);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<int, int> Horarios)
        {
            MatriculaDto matriculaDto = new();

            //Nada seleccionado
            if (Horarios.Count <= 0)
            {
                TempData["matricularme_error"] = "Debe seleccionar al menos un curso.";                

                return RedirectToAction("Index");
            }

            //Objeto para consumo de endpoint de matrícula
            matriculaDto.estudiante_id = HttpContext.Session.GetInt32("estudiante_id") ?? 0;
            matriculaDto.semestre_id = HttpContext.Session.GetInt32("semestre_id") ?? 0;
            matriculaDto.categoria_id = HttpContext.Session.GetInt32("categoria_id") ?? 0;            

            foreach (var item in Horarios)
            {
                Horario horario = new Horario();

                horario.curso = item.Key;
                horario.horario = item.Value;

                matriculaDto.horarios.Add(horario);                
            }

            //Consumir endpoint de matrícula            
            var listaCursosMat = await _academicService.RegistrarMatriculaAsync(matriculaDto);
            
            if (listaCursosMat == null || listaCursosMat.Count <= 0)
            {
                //Regresar a la vista porque algo salió mal
                var listaOfertaAcad = await _academicService.ObtenerOfertaAcademicaAsync(matriculaDto.estudiante_id);

                ViewBag.HorariosSeleccionados = Horarios;                
                ViewBag.Error = "No se pudo registrar la matrícula. Inténtalo nuevamente.";

                return View("Index", listaOfertaAcad);                
            }

            HttpContext.Session.SetInt32("matricula_id", listaCursosMat[0].matricula_id);

            return RedirectToAction("Horario");
        }

        [HttpGet]
        public async Task<IActionResult> Horario()
        {
            int matriculaId = HttpContext.Session.GetInt32("matricula_id") ?? 0;
            int estudiante_id = HttpContext.Session.GetInt32("estudiante_id") ?? 0;

            if (estudiante_id <= 0)
            {
                return RedirectToAction("Index", "Auth");
            }

            var listaCursosMat = await _academicService.ObtenerCursosMatriculadosAsync(matriculaId);

            return View(listaCursosMat);
        }
    }
}