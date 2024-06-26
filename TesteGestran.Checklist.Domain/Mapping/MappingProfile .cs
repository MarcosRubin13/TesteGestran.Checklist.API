using AutoMapper;
using TesteGestran.Checklist.Domain.Checklist;
using TesteGestran.Checklist.Domain.Entities;
using TesteGestran.Checklist.Domain.User;

namespace TesteGestran.Checklist.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChecklistItemCommand, Entities.ChecklistItem>();

            CreateMap<ChecklistCommand, Entities.Checklist>();

            CreateMap<Entities.Checklist, ChecklistDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao))
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataCadastro))
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));

            CreateMap<Entities.ChecklistItem, ChecklistItemDto>()
                       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                       .ForMember(dest => dest.Verificado, opt => opt.MapFrom(src => src.Verificado))
                       .ForMember(dest => dest.Observacao, opt => opt.MapFrom(src => src.Observacao))
                       .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item));

            CreateMap<UserCommand, Entities.User>();

            CreateMap<ChecklistUpdateStatusCommand, Entities.Checklist>()
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao));

            CreateMap<ChecklistCreateCommand, Entities.Checklist>()
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<ChecklistCommand, Entities.Checklist>()
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));
        }
    }
}
