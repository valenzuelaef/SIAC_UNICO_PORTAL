using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving
{
    [DataContract(Name = "ObtenerDatosClienteResponseColiving")]
    public class ObtenerDatosClienteResponse
    {
        [DataMember]
        public String Status { get; set; }
        [DataMember]
        public String CodeResponse { get; set; }
        [DataMember]
        public String DescriptionResponse { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public String CustomerName { get; set; }
        [DataMember]
        public String CustomerAddress { get; set; }
        [DataMember]
        public String DocumentType { get; set; }
        [DataMember]
        public String DescriptionDocumentType { get; set; }
        [DataMember]
        public String DocumentNumber { get; set; }
        [DataMember]
        public String ProductType { get; set; }
        [DataMember]
        public List<SubscriptionPostPaidResponse> SubscriptionsPostPaid { get; set; }
        [DataMember]
        public List<SubscriptionPrepaidResponse> SubscriptionsPrepaid { get; set; }

    }
}
