using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getListOST
{
    [DataContract(Name = "ListOSTResponseColiving")]
    public class ListOSTResponse
    {
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public String CodeResponse { get; set; }
        [DataMember]
        public String DescriptionResponse { get; set; }
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public List<TechnicalServicesOSTType> TechnicalServicesOSTType { get; set; }



    }
}
