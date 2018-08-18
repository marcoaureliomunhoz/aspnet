using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Livro
    {
        public int LivroId { get; private set; } = 0;
        public string Titulo { get; private set; } = "";

        public int? EditoraId { get; private set; }
        public virtual Editora Editora { get; set; }

        public virtual List<Autor> Autores { get; set; }

        private Livro()
        {
        }

        public Livro(string titulo)
        {
            Alterar(titulo);
        }

        public void Alterar(string titulo)
        {
            Titulo = titulo;
        }
    }
}