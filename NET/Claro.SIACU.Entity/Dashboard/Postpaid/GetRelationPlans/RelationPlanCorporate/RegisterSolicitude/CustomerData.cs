using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude
{
    [DataContract]
    public class CustomerData
    {
        [DataMember(Name = "razonSocial")]
        public string BusinessName { get; set; }
        [DataMember(Name = "direFacturacion")]
        public string BillingAddress { get; set; }
        [DataMember(Name = "contactoFacturas")]
        public string ContactInvoices { get; set; }
        [DataMember(Name = "contactoCliente")]
        public string CustomerContact { get; set; }
        [DataMember(Name = "limiteCredito")]
        public string CreditLimit { get; set; }
        [DataMember(Name = "accountManager")]
        public string AccountManager { get; set; }
        [DataMember(Name = "nroAcuerdoPCS")]
        public string PCSAgreementNumber { get; set; }
        [DataMember(Name = "distrito")]
        public string District { get; set; }
        [DataMember(Name = "repLegal")]
        public string LegalRepresentative { get; set; }
        [DataMember(Name = "tlfReferencia")]
        public string TelephoneReference { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "fechaActivacion")]
        public string ActivationDate { get; set; }
        [DataMember(Name = "codCliente")]
        public string ClientCode { get; set; }
        [DataMember(Name = "provincia")]
        public string Province { get; set; }
        [DataMember(Name = "ruc")]
        public string Ruc { get; set; }
        [DataMember(Name = "fax")]
        public string Fax { get; set; }
        [DataMember(Name = "totalLineasA")]
        public string TotalLinesA { get; set; }
        [DataMember(Name = "envioReciboCorreo")]
        public string ShippingReceiptMail { get; set; }
    }
}
