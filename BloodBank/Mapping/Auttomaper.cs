using AutoMapper;
using BloodBank.Models;
namespace BloodBank.Mapping
{
    public class Auttomaper : Profile
    {
        public Auttomaper() {

            CreateMap<RegisterDto,ApplicationUser>().ForMember(w=>w.UserName,e=>e.MapFrom(src=>src.Name));

            CreateMap<LoginDTO,ApplicationUser>();

            CreateMap<RegisterDto, Donor>();
        }
    }
}
