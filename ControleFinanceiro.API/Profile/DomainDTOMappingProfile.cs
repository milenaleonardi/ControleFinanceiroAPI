using AutoMapper;
using ControleFinanceiro.API.Data.Dtos;
using ControleFinanceiro.API.Models;

namespace ControleFinanceiro.API.Profile
{
    public class DomainDTOMappingProfile : Profile
    {
        public DomainDTOMappingProfile()
        {
            CreateMap<Despesa, DespesaDto>().ReverseMap();
            CreateMap<Receita, ReceitaDto>().ReverseMap();
            CreateMap<ResumoDto, Resumo>().ReverseMap()
                .ForMember(dest => dest.DespesaDto, opt => opt.MapFrom(x => x.Despesa))
                .ForMember(dest => dest.ReceitaDto, opt => opt.MapFrom(x => x.Receita));
            CreateMap<ResumoDto, Resumo>().ReverseMap();
        }
    }
}
