using AutoMapper;
using TesteGestran.Checklist.Domain.Checklist;
using TesteGestran.Checklist.Domain.User;

namespace TesteGestran.Checklist.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChecklistItemCommand, Entities.ChecklistItem>();
            CreateMap<ChecklistCommand, Entities.Checklist>();
            CreateMap<UserCommand, Entities.User>();
        }
    }
}
