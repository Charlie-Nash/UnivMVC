using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using OtpNet;

using UnivMVC.Application.Interfaces;
using UnivMVC.Domain.Auth;
using UnivMVC.Web.Helpers;

namespace UnivMVC.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid) return View(loginRequest);

            var response = await _loginService.LoginAsync(loginRequest);

            if (response.status == HttpStatusCode.OK)
            {
                TempData["usr"] = response.usr;
                TempData["secreto"] = response.secreto;
                TempData["tfa"] = response.tfa;

                return RedirectToAction("Tfa");
            }

            ViewBag.Error = response.mensaje;

            return View(loginRequest);
        }

        [HttpGet]
        public IActionResult Tfa()
        {
            var usr = TempData["usr"] as string;
            var secreto = TempData["secreto"]?.ToString();
            var tfa = Convert.ToBoolean(TempData["tfa"]);

            if (usr == null) return RedirectToAction("Index");
            if (secreto == null) return RedirectToAction("Index");

            ViewBag.Usr = usr;
            ViewBag.QRCodeImage = "";

            ViewBag.Error = "";
            ViewBag.Critico = 0;

            Guid secretoGuid;

            if (!Guid.TryParse(secreto, out secretoGuid))
            {
                ViewBag.Critico = 1;
                ViewBag.Error = "El valor del secreto no es un GUID válido.";

                return View();                
            }

            byte[] secretoGuidBytes = secretoGuid.ToByteArray();
            string secretoBase32 = Base32Encoding.ToString(secretoGuidBytes);

            TempData["secretoBase32"] = secretoBase32;
            TempData["secreto"] = secreto;
            TempData["tfa"] = tfa;

            if (!tfa)
            {
                ViewBag.QRCodeImage = QR.GenerateCodeUri(usr, secretoBase32);                
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Tfa(TfaRequest tfaRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.QRCodeImage = "";

                return View(tfaRequest);
            }
                

            var secretoBase32 = TempData["secretoBase32"]?.ToString();
            var secreto = TempData["secreto"]?.ToString();            
            var tfa = Convert.ToBoolean(TempData["tfa"]);

            var totp = new Totp(Base32Encoding.ToBytes(secretoBase32));

            ViewBag.Usr = tfaRequest.Usuario;
            ViewBag.QRCodeImage = "";

            TempData["secretoBase32"] = secretoBase32;
            TempData["secreto"] = secreto;
            TempData["tfa"] = tfa;

            if (!(totp.VerifyTotp(tfaRequest.Codigo, out _, new VerificationWindow(2, 2))))
            {
                if (!tfa)
                {
                    ViewBag.QRCodeImage = QR.GenerateCodeUri(tfaRequest.Usuario, secretoBase32);
                }

                return View(tfaRequest);
            }

            tfaRequest.Secreto = secreto ?? "";

            var response = await _loginService.Login2faAsync(tfaRequest);

            if (response.status == HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("usr", response.usr);
                HttpContext.Session.SetInt32("rol_id", response.rol_id);
                HttpContext.Session.SetInt32("persona_id", response.persona_id);
                HttpContext.Session.SetString("nombre_usuario", response.nombre_usuario);
                HttpContext.Session.SetString("doc_id", response.doc_id);

                return RedirectToAction("Index", "Principal");
            }

            ViewBag.Error = response.mensaje;

            if (!tfa)
            { 
                ViewBag.QRCodeImage = QR.GenerateCodeUri(tfaRequest.Usuario, secretoBase32);
            }

            return View(tfaRequest);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Auth");
        }
    }
}
