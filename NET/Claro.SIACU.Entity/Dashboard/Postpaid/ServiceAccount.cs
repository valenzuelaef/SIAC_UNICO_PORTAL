using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ServiceAccountPostPaid")]
    public class ServiceAccount
    {
        public ServiceAccount()
        {
            ListServiceAccountGSM = new List<ServiceGSMAccount>();
            ListServiceAccountHFC = new List<ServiceHFCAccount>();
            ListServiceAccountLTE = new List<ServiceLTEAccount>();
        }
        
        [Data.DbColumn("CUSTOMER_ID")]
        [DataMember]
        public string CUSTOMER_ID { get; set; }      

        [DataMember]
        public List<ServiceGSMAccount> ListServiceAccountGSM { get; set; }
        [DataMember]
        public List<ServiceHFCAccount> ListServiceAccountHFC { get; set; }
        [DataMember]
        public List<ServiceLTEAccount> ListServiceAccountLTE { get; set; }

    }
}
