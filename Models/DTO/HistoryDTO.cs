namespace AIRCOM.Models.DTO
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string OwnerRole { get; set; }
        public string? Plate { get; set; }
        public string? State { get; set; }
        public int AirportID { get; set; }
    }
}
