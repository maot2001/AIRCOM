namespace AIRCOM.Models.DTO
{
    public class RepairShipDTO
    {
        public int InstallationID { get; set; }
        public int AirportID { get; set; }
        public int RepairID { get; set; }
        public string? Plate { get; set; }
        public DateTime? Init { get; set; }
        public DateTime? newTime { get; set; }
    }
}
