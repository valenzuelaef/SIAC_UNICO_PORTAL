using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid
{
    public class DataAccountModel
    {
        public string CustomerType { get; set; }
        public string Modality { get; set; }
        public string Segment { get; set; }
        public string BillingCycle { get; set; }
        public string Niche { get; set; }
        public string AccountStatus { get; set; }
        public string ResponsiblePayment { get; set; }
        public string CreditLimit { get; set; }
        public string SaldoCreditLimit { get; set; }

        public string TotalLines { get; set; }
        public string TotalAccounts { get; set; }
        public string ActivationDate { get; set; }

        public string Consultant { get; set; }
        public string Consultant_Account { get; set; }

        public string Name { get; set; }
        public string Balance { get; set; }
        public string ExpirationDate { get; set; }
        public string LastName { get; set; }
        public string AccountParent { get; set; }
        public string AccountId { get; set; }
        public string Level { get; set; }
        public string CustomerId { get; set; }
        public string plataformaAT { get; set; }
        public string billingAccountId { get; set; }
        public string bmIdPub { get; set; }
        public string contactSeqno { get; set; }
        public string IsSendEmail { get; set; }
        public List<DataAccountModel> lstPostDataAccount { get; set; }
    }
}