using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getCustomerInfo
{
    [DataContract]
    public class CustomerInfoResponse
    {
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string CodeResponse { get; set; }
        [DataMember]
        public string DescriptionResponse { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public string CustomerType { get; set; }
    }
}
