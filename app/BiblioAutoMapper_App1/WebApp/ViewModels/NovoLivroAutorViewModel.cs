using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.ViewModels
{
    public class AutorCheckBox
    {
        public int AutorId { get; set; } = 0;
        public string Nome { get; set; } = "";
        public bool Selected { get; set; } = false;

        public AutorCheckBox(int id, string nome)
        {
            AutorId = id;
            Nome = nome;
        }

        public AutorCheckBox()
        {
                    }
    }

    public class NovoLivroAutorViewModel
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public List<AutorCheckBox> Autores { get; set; }

        public NovoLivroAutorViewModel()
        {
            Autores = new List<AutorCheckBox>();
        }

        public NovoLivroAutorViewModel(int id, string titulo)
        {
            Autores = new List<AutorCheckBox>();
            LivroId = id;
            Titulo = titulo;
        }
    }
}