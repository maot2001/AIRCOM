namespace AIRCOM.Models.DTO
{
    public class ServiceInstallationDTO
    {
        public int? Code { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float? Stars { get; set; }
        public int InstallationID { get; set; }
        public List<On_siteDTO> On_sites { get; set; }
    }
}
