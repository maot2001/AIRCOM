using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;

namespace AIRCOM.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<RepairInstallation, RepairInstallationDTO>();
            CreateMap<RepairInstallation, RepairInstallationDTO>().ReverseMap();
        }
    }
}
