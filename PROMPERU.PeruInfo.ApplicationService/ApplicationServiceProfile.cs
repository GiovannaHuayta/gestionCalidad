using AutoMapper;
using PROMPERU.PeruInfo.ApplicationService.DTO;
using PROMPERU.PeruInfo.Domain.Entities;
using System;

namespace PROMPERU.PeruInfo.ApplicationService
{
    public class ApplicationServiceProfile : Profile
    {
        public ApplicationServiceProfile()
        {
            CreateMap<ComunicadoBE, ComunicadoDTO>();
            CreateMap<PublicacionBE, PublicacionDTO>();
            CreateMap<NegocioBE, NegocioDTO>();
            CreateMap<ContactoBE, ContactoDTO>();
            CreateMap<UsuarioBE, UsuarioDTO>();
            CreateMap<CategoriaBE, CategoriaDTO>();
            CreateMap<SubcategoriaBE, SubcategoriaDTO>();
            CreateMap<PalabrasAlientoBE, PalabrasAlientoDTO>();
            CreateMap<DateTime, string>().ConvertUsing(dt => dt.ToString("yyyy-MM-ddTHH:mm:ss"));
        }
    }
}
