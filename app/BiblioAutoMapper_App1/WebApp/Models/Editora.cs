using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Editora
    {
        public int EditoraId { get; private set; } = 0;
        public string Nome { get; private set; } = "";

        private Editora()
        {
        }

        public Editora(string nome)
        {
            Alterar(nome);
        }

        public void Alterar(string nome)
        {
            Nome = nome;
        }
    }
}