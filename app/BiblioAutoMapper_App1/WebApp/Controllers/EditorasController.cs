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
    [LogActionFilter]
    public class EditorasController : Controller
    {
        DbContextBiblio db;

        public EditorasController()
        {
            db = new DbContextBiblio();
        }

        // GET: Editoras
        public ActionResult Index()
        {
            var rset = from e in db.Editoras
                       orderby e.Nome
                       select e;

            //var md = db.Editoras.ToList();
            var md = rset.ToList();
            var vm = Mapper.Map<List<Editora>, List<EditoraViewModel>>(md);
            return View(vm);
        }

        [MyAuthorizeFilter("admin")]
        public ActionResult Alterar(int id)
        {
            var rset = from e in db.Editoras
                       where e.EditoraId == id
                       select e;
            //var md = db.Editoras.FirstOrDefault(x => x.EditoraId == id);
            var md = rset.FirstOrDefault();
            var vm = Mapper.Map<Editora, EditoraViewModel>(md);
            return View("Cadastro", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(EditoraViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var md = Mapper.Map<EditoraViewModel, Editora>(vm);
                try
                {
                    db.Entry<Editora>(md).State = System.Data.Entity.EntityState.Modified;
                    if (db.SaveChanges() > 0)
                        return RedirectToAction("Index");
                }
                catch { }
            }
            return View("Cadastro", vm);
        }

        [MyAuthorizeFilter("admin")]
        public ActionResult Novo()
        {
            return View("Cadastro", new EditoraViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Novo(EditoraViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var md = Mapper.Map<EditoraViewModel, Editora>(vm);
                try
                {
                    db.Editoras.Add(md);
                    if (db.SaveChanges() > 0)
                        return RedirectToAction("Index");
                }
                catch { }
            }
            ModelState.AddModelError(string.Empty, "Verifique os dados fornecidos e tente novamente.");
            return View("Cadastro", vm);
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                var rset = from e in db.Editoras
                           where e.EditoraId == id
                           select e;
                var md = rset.FirstOrDefault();
                //var md = db.Editoras.FirstOrDefault(x => x.EditoraId == id);
                if (md != null)
                {
                    db.Editoras.Remove(md);
                    db.SaveChanges();
                }
            }
            catch { }
            return RedirectToAction("Index");
        }
    }
}