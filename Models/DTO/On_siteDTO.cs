namespace AIRCOM.Models.DTO
{
    public class On_siteDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Fecha { get; set; }
        public float? Price { get; set; }
        public int Stars { get; set; } = 0;
        public string Comment { get; set; }
        public int? InstallationID { get; set; }
        public int? AirportID { get; set; }
        public int? Code { get; set; }
        public int? ClientID { get; set; }
    }
}
