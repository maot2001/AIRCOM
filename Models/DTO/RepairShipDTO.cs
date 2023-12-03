namespace AIRCOM.Models.DTO
{
    public class RepairShipDTO
    {
        public int? RSID { get; set; }
        public string Name { get; set; }
        public int RepairInstallationID { get; set; }
        public string Plate { get; set; }
        public DateTime? Init { get; set; }
        public DateTime? Finish { get; set; }
        public string State { get; set; }
        public float Price { get; set; }
        public int? Stars { get; set; }
        public string? Comment { get; set; }
    }
}
