using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService
{
    [DataContract (Name="TypeServiceResponsePostpaid")]
    public class TypeServiceResponse
    {
        [DataMember]
        public string NameTypeService { get; set; }
    }
}
