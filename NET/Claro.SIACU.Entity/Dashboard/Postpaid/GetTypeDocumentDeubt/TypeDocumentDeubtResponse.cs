using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeDocumentDeubt
{
     [DataContract(Name = "TypeDocumentDeubtResponsePostPaid")]
    public class TypeDocumentDeubtResponse
    {
        [DataMember]
        public List<Customer> lstCustomer { get; set; }
        [DataMember]
        public string strout_err_code { get; set; }
        [DataMember]
        public string out_err_msg { get; set; }
    }
}
