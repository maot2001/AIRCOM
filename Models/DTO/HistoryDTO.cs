namespace AIRCOM.Models.DTO
{
    public class HistoryDTO
    {
        public int? ID { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public string? OwnerRole { get; set; }
        public string Plate { get; set; }
        public string State { get; set; }
        public int? ArrivalID { get; set; }
        public int? ExitID { get; set; }
    }
}
