using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.Contact;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer
{

    public class ContactModel
    {
        public string AvailableEmails { get; set; }
        public string DocumentTypeDNI { get; set; }
        public string DocumentTypeRUC { get; set; }
        public string DocumentTypePAS { get; set; }
        public string DocumentTypeCEX { get; set; }
        public string DocumentTypeCIP { get; set; }
        public string DocumentTypeCFA { get; set; }
        public string AccountNumber { get; set; }
        public string Profile { get; set; }
        public string Telephone { get; set; }
        public string NewState { get; set; }
        public string ModifyState { get; set; }
        public string ConsultState { get; set; }
        public string ConsultConst { get; set; }
        public string AddConst { get; set; }
        public string EditConst { get; set; }
        public string AddEditConst { get; set; }
        public string BusinessName { get; set; }
        public List<ContactTypeFieldModel> lstContactTypeField { get; set; }
        public List<Contact> lstContact { get; set; }
        public List<DataTypeModel> lstDataType { get; set; }
        public List<ContactTypeModel> lstContactType { get; set; }
        public List<DocumentTypeModel> lstDocumentType { get; set; }

        public ContactModel()
        {
            lstContactTypeField = new List<ContactTypeFieldModel>();
            lstContact = new List<Contact>();
            lstDataType = new List<DataTypeModel>();
            lstContactType = new List<ContactTypeModel>();
            lstDocumentType = new List<DocumentTypeModel>();
        }
    }

    
}