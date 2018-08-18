using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Data.EF.Config
{
    public class LivroConfig : EntityTypeConfiguration<Livro>
    {
        public LivroConfig()
        {
            ToTable("Livro");
            HasKey(x => x.LivroId);
            HasOptional(x => x.Editora);
            HasMany(x => x.Autores)
                .WithMany(x => x.Livros)
                .Map(x =>
                {
                    x.MapLeftKey("LivroId");
                    x.MapRightKey("AutorId");
                    x.ToTable("Livro_Autor");
                });
        }
    }
}