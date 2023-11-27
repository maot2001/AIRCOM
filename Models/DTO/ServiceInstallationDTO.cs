namespace AIRCOM.Models.DTO
{
    public class ServiceInstallationDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float? Stars { get; set; }
        public int InstallationID { get; set; }
        public int? AirportID { get; set; }
        public int Code { get; set; }
    }
}
