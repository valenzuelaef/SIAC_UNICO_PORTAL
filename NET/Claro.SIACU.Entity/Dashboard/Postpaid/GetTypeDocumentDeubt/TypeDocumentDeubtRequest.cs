using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeDocumentDeubt
{
     [DataContract(Name = "TypeDocumentDeubtRequestPostPaid")]
    public class TypeDocumentDeubtRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strDocLinea { get; set; }
        [DataMember]
        public string strCod_consulta { get; set; }
    }
}
