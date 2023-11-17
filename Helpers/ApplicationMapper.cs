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
            CreateMap<RepairShipDTO, RepairShip>();
            CreateMap<RepairShipDTO, RepairShip>().ReverseMap();
            CreateMap<RepairShipDTO, RepairInstallationDTO>();
            CreateMap<RepairShipDTO, RepairInstallationDTO>().ReverseMap();
            CreateMap<RepairInstallationDTO, RepairShip>();
            CreateMap<RepairInstallationDTO, RepairShip>().ReverseMap();
            CreateMap<ClientDTO, Client>();
            CreateMap<ClientDTO, Client>().ReverseMap();
            CreateMap<ClientDTO, Worker>()
                .ForMember(dest => dest.WorkerId, opt => opt.MapFrom(src => src.ClientID));
            CreateMap<ClientDTO, Worker>().ReverseMap();
            CreateMap<Ships, ShipsDTO>();
            CreateMap<Ships, ShipsDTO>().ReverseMap();
            CreateMap<Airport, AirportDTO>();
            CreateMap<Airport, AirportDTO>().ReverseMap();
            CreateMap<Repair, RepairDTO>();
            CreateMap<Repair, RepairDTO>().ReverseMap();
            CreateMap<CustomerService, ServiceDTO>();
            CreateMap<CustomerService, ServiceDTO>().ReverseMap();
            CreateMap<History, HistoryDTO>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Ships.State));
            CreateMap<History, HistoryDTO>().ReverseMap();
            CreateMap<Installation, InstallationDTO>();
            CreateMap<Installation, InstallationDTO>().ReverseMap();
            CreateMap<ServiceInstallationDTO, On_siteDTO>();
            CreateMap<ServiceInstallationDTO, On_siteDTO>().ReverseMap();
            CreateMap<on_site, On_siteDTO>();
            CreateMap<on_site, On_siteDTO>().ReverseMap();
            CreateMap<ServicesInstallation, ServiceInstallationDTO>();
            CreateMap<ServicesInstallation, ServiceInstallationDTO>().ReverseMap();
            CreateMap<ServiceInstallationDTO, on_site>();
            CreateMap<ServiceInstallationDTO, on_site>().ReverseMap();
            CreateMap<ServicesInstallation, InstallationDTO>();
            CreateMap<ServicesInstallation, InstallationDTO>().ReverseMap();
        }
    }
}
