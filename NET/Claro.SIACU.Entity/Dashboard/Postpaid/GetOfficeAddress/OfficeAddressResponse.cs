using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress
{
    [DataContract(Name = "OfficeAddressResponsePostPaid")]
    public class OfficeAddressResponse
    {
         [DataMember]
         public Entity.Dashboard.Postpaid.OfficeAddress ObjOfficeAddress { get; set; }
    }
}

