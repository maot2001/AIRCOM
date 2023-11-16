namespace AIRCOM.Models.DTO
{
    public class RepairInstallationDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public int Stars { get; set; }
        public int? InstallationID { get; set; }
        public int? AirportID { get; set; }
        public int? RepairID { get; set; }
        public string State { get; set; }
    }
}
