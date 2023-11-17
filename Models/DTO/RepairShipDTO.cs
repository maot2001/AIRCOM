namespace AIRCOM.Models.DTO
{
    public class RepairShipDTO
    {
        public int Id { get; set; }
        public int? InstallationID { get; set; }
        public int? AirportID { get; set; }
        public int? RepairID { get; set; }
        public string? Plate { get; set; }
        public DateTime Init { get; set; }
        public DateTime Finish { get; set; }
        public DateTime newTime { get; set; }
        public string? State { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}
