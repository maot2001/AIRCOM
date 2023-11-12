namespace AIRCOM.Models.DTO
{
    public class RepairInstallationDTO
    {
        public int InstallationID { get; set; }
        public int AirportID { get; set; }
        public int RepairID { get; set; }
        public string? State { get; set; }
    }
}
