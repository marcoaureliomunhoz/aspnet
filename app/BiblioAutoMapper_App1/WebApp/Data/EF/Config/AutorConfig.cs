using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Data.EF.Config
{
    public class AutorConfig : EntityTypeConfiguration<Autor>
    {
        public AutorConfig()
        {
            ToTable("Autor");
            HasKey(x => x.AutorId);
        }
    }
}