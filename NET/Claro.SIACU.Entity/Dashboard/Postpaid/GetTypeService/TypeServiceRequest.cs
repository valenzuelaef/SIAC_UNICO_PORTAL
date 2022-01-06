using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService
{
    [DataContract (Name="TypeServiceRequestPostpaid")]
    public class TypeServiceRequest :Claro.Entity.Request
    {
        [DataMember]
        public string CodePlanTariff { get; set; }
    }
}
