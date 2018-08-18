using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Domain;
using WebApp.Models;

namespace WebApp.Mappings
{
    public class GenericProfileMapping : Profile
    {
        public GenericProfileMapping()
        {
            CreateMap<UsuarioViewModel, Usuario>().ReverseMap();
        }
    }
}
