namespace AIRCOM.Models.DTO
{
    public class ShipsDTO
    {
        public string Plate { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public int Crew { get; set; }
        public int Pass { get; set; }
        public int? ClientID { get; set; }
        public string? State { get; set; }
    }
}
