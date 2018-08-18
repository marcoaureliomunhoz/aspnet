using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using WebApp.Domain;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;

        public HomeController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new UsuarioViewModel());
        }
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var usuariodb = mapper.Map<Usuario>(usuario);
                if (usuariodb != null)
                {
                    Debug.WriteLine($"Logando... {usuariodb.Nome}");
                }
                
                if (usuario.Nome == "marco" || usuario.Nome == "admin")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Role, usuario.Permissao)
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(userIdentity);

                    HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário/permissão inválidos!");
                }
            }
            return View(usuario);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
