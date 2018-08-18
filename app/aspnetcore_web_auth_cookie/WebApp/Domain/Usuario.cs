using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Domain
{
    public class Usuario
    {
        public int UsuarioId { get; private set; } = 0;
        public string Nome { get; private set; } = "";
        public string Permissao { get; private set; } = "";

        private Usuario()
        {
        }

        public Usuario(string nome, string permissao)
        {
            Nome = nome;
            Permissao = permissao;
        }
    }
}
