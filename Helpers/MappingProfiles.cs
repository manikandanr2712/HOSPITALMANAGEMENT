using AutoMapper;
using HOSPITALMANAGEMENT.Model.DbModels;
using HOSPITALMANAGEMENT.Model;

namespace HOSPITALMANAGEMENT.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<LoginInputModel, User>().ReverseMap();
            CreateMap<RegisterInputModel, User>().ReverseMap();
            CreateMap<EventInputModel, Event>().ReverseMap();
            CreateMap<UserEventInputModel, UserEvent>().ReverseMap();
        }
    }
}
