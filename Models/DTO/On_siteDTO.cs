namespace AIRCOM.Models.DTO
{
    public class On_siteDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public DateTime? Fecha { get; set; }
        public float Price { get; set; }
        public int? Stars { get; set; }
        public string? Comment { get; set; }
        public int InstallationID { get; set; }
        //public int AirportID { get; set; }
        public int Code { get; set; }
        public int ClientID { get; set; }
    }
}
