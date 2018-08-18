using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; } = 0;

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo permissão é obrigatório.")]
        public string Permissao { get; set; }
    }
}
