using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Coliving.getSearchCustomer
{
    [DataContract(Name = "SearchCustomerRequestColiving")]
    public class SearchCustomerRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
    }
}
