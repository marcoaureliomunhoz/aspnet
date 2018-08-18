using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Data.EF
{
    public class DbContextBiblio : DbContext
    {
        public DbContextBiblio() : base("DbContextBiblio")
        {
            Database.Log = Console.Write;
            Database.SetInitializer<DbContextBiblio>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));
            modelBuilder.Properties<string>().Configure(p => p.IsUnicode(false));

            modelBuilder.Configurations.Add(new Config.EditoraConfig());
            modelBuilder.Configurations.Add(new Config.AutorConfig());
            modelBuilder.Configurations.Add(new Config.LivroConfig());
        }
    }
}