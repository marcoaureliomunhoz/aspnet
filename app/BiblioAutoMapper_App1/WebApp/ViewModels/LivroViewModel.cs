using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ViewModels
{
    public class LivroViewModel
    {
        public int LivroId { get; set; } = 0;
        public string Titulo { get; set; } = "";
        public int EditoraId { get; set; } = 0;
        public EditoraViewModel Editora { get; set; }
        public List<AutorViewModel> Autores { get; set; }

        public List<EditoraViewModel> ListEditoras { get; set; }

        public LivroViewModel()
        {
            ListEditoras = new List<EditoraViewModel>();
        }
    }
}