using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getSearchCustomer
{
    [DataContract(Name = "SearchCustomerResponseColiving")]
    public class SearchCustomerResponse
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
        public String CustomerName { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public String AccountNumber { get; set; }
        [DataMember]
        public String DocumentType { get; set; }
        [DataMember]
        public String DescriptionDocumentType { get; set; }
        [DataMember]
        public String DocumentNumber { get; set; }
        [DataMember]
        public String ParentAccount { get; set; }
        [DataMember]
        public String CustomerStatus { get; set; }
    }
}
