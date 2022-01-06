using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetWarranty
{
    [DataContract(Name = "WarrantyResponsePostPaid")]
    public class WarrantyResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.Warranty ObjWarranty { get; set; }

    }
}
