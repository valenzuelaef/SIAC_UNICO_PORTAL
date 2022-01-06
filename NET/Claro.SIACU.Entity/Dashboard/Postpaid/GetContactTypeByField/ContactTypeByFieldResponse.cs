using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField
{
    [DataContract(Name = "ContactTypeByFieldResponsePostPaid")]
    public class ContactTypeByFieldResponse  
    { 
         [DataMember]
         public List<ContactTypeByField> lstContactTypeByField { get; set; }
    }
} 
