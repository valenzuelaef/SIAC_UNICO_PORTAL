using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetContact
{
    [DataContract(Name = "ContactResponsePostPaid")]  
    public  class ContactResponse
    {
         [DataMember]
         public List<ContactType> lstContactType { get; set; }
         [DataMember] 
         public List<ContactTypeField> lstContactFields { get; set; } 
         [DataMember]
         public List<DocumentType> lstDocumentType { get; set; }
         [DataMember]
         public List<DataType> lstDataType { get; set; }
         [DataMember]
         public List<Contact> lstContact { get; set; }
         [DataMember]  
         public string  AvailableEmails { get; set; }
         [DataMember] 
         public string DocumentTypeDNI { get; set; }
         [DataMember] 
         public string DocumentTypeRUC { get; set; }
         [DataMember] 
         public string DocumentTypePAS { get; set; }
         [DataMember] 
         public string DocumentTypeCEX { get; set; }
         [DataMember] 
         public string DocumentTypeCIP { get; set; }
         [DataMember]
         public string DocumentTypeCFA { get; set; }
         [DataMember] 
         public string NewState { get; set; }
         [DataMember] 
         public string ModifyState { get; set; }
         [DataMember] 
         public string ConsultState { get; set; }

         [DataMember]
         public string ConsultConst { get; set; }
         [DataMember]
         public string AddConst { get; set; }
         [DataMember]
         public string EditConst { get; set; }
         [DataMember]
         public string AddEditConst { get; set; }   

           
    }
}
