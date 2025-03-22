using Agenda.Application.DTOS.ViewModels;
using Agenda.Application.DTOS.InputModels;
using Agenda.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Application.Mappings
{
    public class ContatoMappingProfile : Profile
    {
        public ContatoMappingProfile()
        {
            // Mapeamento de CreateContatoInputModel -> Contato
            CreateMap<CreateContatoInputModel, Contato>();

            // Mapeamento de UpdateContatoInput -> Contato
            CreateMap<UpdateContatoInput, Contato>();

            // Mapeamento de Contato -> ContatoViewModel
            CreateMap<Contato, ContatoViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            // Mapeamento de Contato -> ContatoDetailsViewModel
            CreateMap<Contato, ContatoDetailsViewModel>();
        }
    }
}
