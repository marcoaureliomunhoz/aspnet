using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.ViewModels
{
    public class EditoraViewModel
    {
        public int EditoraId { get; set; } = 0;

        [Required(ErrorMessage = "O nome é um campo necessário!")]
        [StringLength(100, ErrorMessage = "O campo nome deve ter no mínimo 3 caracteres e no máximo 100.", MinimumLength = 3)]
        public string Nome { get; set; } = "";
    }
}