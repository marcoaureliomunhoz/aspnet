using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data.EF;
using WebApp.Filters;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [MyAuthenticationFilter]
    public class AutoresController : Controller
    {
        DbContextBiblio db;

        public AutoresController()
        {
            db = new DbContextBiblio();
        }

        // GET: Autores
        public ActionResult Index()
        {
            var md = db.Autores.ToList();
            var vm = Mapper.Map<List<Autor>, List<AutorViewModel>>(md);
            return View(vm);
        }

        public ActionResult Alterar(int id)
        {
            var md = db.Autores.FirstOrDefault(x => x.AutorId == id);
            var vm = Mapper.Map<Autor, AutorViewModel>(md);
            return View("Cadastro", vm);
        }

        [HttpPost]
        public ActionResult Alterar(AutorViewModel vm)
        {
            var md = Mapper.Map<AutorViewModel, Autor>(vm);
            try
            {
                db.Entry<Autor>(md).State = System.Data.Entity.EntityState.Modified;
                if (db.SaveChanges() > 0)
                    return RedirectToAction("Index");
            }
            catch { }
            return View("Cadastro", vm);
        }

        public ActionResult Novo()
        {
            return View("Cadastro", new AutorViewModel());
        }

        [HttpPost]
        public ActionResult Novo(AutorViewModel vm)
        {
            var md = Mapper.Map<AutorViewModel, Autor>(vm);
            try
            {
                db.Autores.Add(md);
                if (db.SaveChanges() > 0)
                    return RedirectToAction("Index");
            }
            catch { }
            return View("Cadastro", vm);
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                var md = db.Autores.FirstOrDefault(x => x.AutorId == id);
                if (md != null)
                {
                    db.Autores.Remove(md);
                    db.SaveChanges();
                }
            }
            catch { }
            return RedirectToAction("Index");
        }
    }
}