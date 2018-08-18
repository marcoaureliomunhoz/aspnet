using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Autor
    {
        public int AutorId { get; private set; } = 0;
        public string Nome { get; private set; } = "";

        public virtual List<Livro> Livros { get; set; }

        private Autor()
        {
        }

        public Autor(string nome)
        {
            Alterar(nome);
        }

        public void Alterar(string nome)
        {
            Nome = nome;
        }
    }
}