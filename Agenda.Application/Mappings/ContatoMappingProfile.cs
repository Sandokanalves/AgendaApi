using Agenda.Application.DTOS.ViewModels;
using Agenda.Application.DTOS.InputModels;
using Agenda.Domain.Entities;
using AutoMapper;


namespace Agenda.Application.Mappings
{
    public class ContatoMappingProfile : Profile
    {
        public ContatoMappingProfile()
        {
            
            CreateMap<CreateContatoInputModel, Contato>();

 
            CreateMap<UpdateContatoInput, Contato>();

            
            CreateMap<Contato, ContatoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.Telefone));


            CreateMap<Contato, ContatoDetailsViewModel>();
        }
    }
}
