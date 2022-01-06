using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDueDocumentNumber
{
    [DataContract(Name = "DueDocumentNumberResponsePostPaid")]
    public  class DueDocumentNumberResponse
    {
       [DataMember]
        public Entity.Dashboard.Postpaid.DueDocumentNumber ObjDueDocumentNumber { get; set; } 
    }
    
}
