using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class LivrosController : Controller
    {
        DbContextBiblio db;

        public LivrosController()
        {
            db = new DbContextBiblio();
        }

        // GET: Livros
        public ActionResult Index()
        {
            var md = db.Livros.Include(x => x.Editora).ToList();
            var vm = Mapper.Map<List<Livro>, List<LivroViewModel>>(md);
            return View(vm);
        }

        public ActionResult Alterar(int id)
        {
            var md = db.Livros.Include(x => x.Autores).FirstOrDefault(x => x.LivroId == id);
            var vm = Mapper.Map<Livro, LivroViewModel>(md);
            vm.ListEditoras = Mapper.Map<List<Editora>, List<EditoraViewModel>>(db.Editoras.ToList());
            return View("Cadastro", vm);
        }

        [HttpPost]
        public ActionResult Alterar(LivroViewModel vm)
        {
            var md = Mapper.Map<LivroViewModel, Livro>(vm);
            try
            {
                db.Entry<Livro>(md).State = System.Data.Entity.EntityState.Modified;
                if (db.SaveChanges() > 0)
                    return RedirectToAction("Index");
            }
            catch { }
            if (vm.EditoraId > 0)
            {
                vm.Editora = Mapper.Map<Editora, EditoraViewModel>(db.Editoras.FirstOrDefault(x => x.EditoraId == vm.EditoraId));
            }
            vm.ListEditoras = Mapper.Map<List<Editora>, List<EditoraViewModel>>(db.Editoras.ToList());
            return View("Cadastro", vm);
        }

        public ActionResult Novo()
        {
            var vm = new LivroViewModel();
            vm.ListEditoras = Mapper.Map<List<Editora>, List<EditoraViewModel>>(db.Editoras.ToList());
            return View("Cadastro", vm);
        }

        [HttpPost]
        public ActionResult Novo(LivroViewModel vm)
        {
            var md = Mapper.Map<LivroViewModel, Livro>(vm);
            try
            {
                db.Livros.Add(md);
                if (db.SaveChanges() > 0)
                    return RedirectToAction("Index");
            }
            catch { }
            if (vm.EditoraId > 0)
            {
                vm.Editora = Mapper.Map<Editora, EditoraViewModel>(db.Editoras.FirstOrDefault(x => x.EditoraId == vm.EditoraId));
            }
            vm.ListEditoras = Mapper.Map<List<Editora>, List<EditoraViewModel>>(db.Editoras.ToList());
            return View("Cadastro", vm);
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                var md = db.Livros.FirstOrDefault(x => x.LivroId == id);
                if (md != null)
                {
                    db.Livros.Remove(md);
                    db.SaveChanges();
                }
            }
            catch { }
            return RedirectToAction("Index");
        }

        public ActionResult ExcluirAutor(int livroId, int autorId)
        {
            try
            {
                var md = db.Livros.Include(x => x.Editora).Include(x => x.Autores).FirstOrDefault(x => x.LivroId == livroId);
                if (md != null)
                {
                    md.Autores.Remove(md.Autores.FirstOrDefault(x => x.AutorId == autorId));
                    db.SaveChanges();
                    return RedirectToAction("Alterar", new { id = livroId });
                }
            }
            catch { }
            return RedirectToAction("Index");
        }

        public ActionResult NovoAutor(int livroId)
        {
            try
            {
                var livro = db.Livros.Include(x => x.Autores).FirstOrDefault(x => x.LivroId == livroId);
                if (livro != null)
                {
                    List<Autor> autores = null;
                    if (livro.Autores != null)
                    {
                        var ids = livro.Autores.Select(x => x.AutorId).ToArray();
                        autores = db.Autores.Where(x => !ids.Contains(x.AutorId)).ToList();
                    }
                    else
                    {
                        autores = db.Autores.ToList();
                    }

                    var vm = new NovoLivroAutorViewModel(livroId, livro.Titulo);

                    if (autores != null)
                        autores.ForEach(x => vm.Autores.Add(new AutorCheckBox(x.AutorId, x.Nome)));

                    return View(vm);
                }
            }
            catch { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult NovoAutor(NovoLivroAutorViewModel vm)
        {
            try
            {
                var md = db.Livros.Include(x => x.Editora).Include(x => x.Autores).FirstOrDefault(x => x.LivroId == vm.LivroId);
                var novos = vm.Autores.Where(x => x.Selected);
                var qtde = 0;
                foreach (var novo in novos)
                {
                    if (md.Autores.Count(x => x.AutorId == novo.AutorId) == 0)
                    {
                        var novoAutor = db.Autores.FirstOrDefault(x => x.AutorId == novo.AutorId);
                        if (novoAutor != null)
                        {
                            md.Autores.Add(novoAutor);
                            qtde++;
                        }
                    }
                }
                if (qtde > 0)
                    db.SaveChanges();
                return RedirectToAction("Alterar", new { id = vm.LivroId });
            }
            catch { }
            return RedirectToAction("Index");
        }
    }
}