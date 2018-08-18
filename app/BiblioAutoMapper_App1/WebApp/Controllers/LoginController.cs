using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private void ResetLogin()
        {
            try
            {
                var hash = "";
                if (HttpContext.Response.Cookies["biblio_user_id"] != null)
                    hash = HttpContext.Response.Cookies["biblio_user_id"].Value;
                if (!string.IsNullOrEmpty(hash))
                    HttpContext.Session.Remove(hash);
                HttpContext.Response.Cookies.Remove("biblio_user_id");
                var c = new HttpCookie("biblio_user_id");
                c.Expires = DateTime.Now.AddDays(-999);
                HttpContext.Response.Cookies.Add(c);
            }
            catch { }
        }

        // GET: Login
        public ActionResult Index()
        {
            ResetLogin();
            ViewBag.Deslogar = true;
            var vm = new UsuarioViewModel();
            return View(vm);
        }

        private string MD5Hash(string itemToHash)
        {
            try
            {
                return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(itemToHash)).Select(s => s.ToString("x2")));
            }
            catch { }
            return DateTime.Now.ToLongDateString().Replace(' ', '_');
        }

        [HttpPost]
        public ActionResult Index(UsuarioViewModel vm)
        {
            if (vm.Nome == "admin" || vm.Nome == "user" || vm.Nome == "teste")
            {
                var hash = MD5Hash(vm.Nome + "|" + DateTime.Now.ToLongDateString());
                var c = new HttpCookie("biblio_user_id", hash);
                c.HttpOnly = true;
                c.Expires = DateTime.Now.AddHours(24);
                HttpContext.Response.Cookies.Add(c);
                HttpContext.Session[hash] = JsonConvert.SerializeObject(vm);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Deslogar = true;
            return View(vm);
        }

        public ActionResult Logout()
        {
            ResetLogin();
            return RedirectToAction("Index");
        }
    }
}