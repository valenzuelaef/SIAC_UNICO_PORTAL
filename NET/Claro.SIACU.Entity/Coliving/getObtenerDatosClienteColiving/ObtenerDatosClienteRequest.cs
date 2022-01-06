using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving
{
    [DataContract(Name = "ObtenerDatosClienteRequestColiving")]
    public class ObtenerDatosClienteRequest : Claro.Entity.Request
    {
        [DataMember]
        public String CustomerId { get; set; }
        [DataMember]
        public String LineNumber { get; set; }
        [DataMember]
        public String DocumentType { get; set; }
        [DataMember]
        public String DocumentNumber { get; set; }
    }
}
