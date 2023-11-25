namespace AIRCOM.Models.DTO
{
    public class InstallationDTO
    {
        public int? ID { get; set; }
        public int InstallationID { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public string Ubication { get; set; }
        public int? AirportID { get; set; }
    }
}