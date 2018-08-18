using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; } = 0;
        public string Nome { get; set; } = "";
        public string Senha { get; set; } = "";
        public string Permissao { get; set; } = "";
    }
}
