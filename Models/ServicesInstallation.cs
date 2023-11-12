using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRCOM.Models
{
    public class ServicesInstallation
    {
        public int Price { get; set; }
        public int Valoration { get; set; }
        public int InstallationID { get; set; }
        [ForeignKey(nameof(InstallationID))]
        public virtual Airoport? Airoport { get; set; }
        public int AiroportID { get; set; }
        [ForeignKey(nameof(AiroportID))]
        public virtual Installation? Installation { get; set; }
        public int Code { get; set; }
        [ForeignKey(nameof(Code))]
        public virtual CustomerService? CustomerService { get; set; }

    }

}
