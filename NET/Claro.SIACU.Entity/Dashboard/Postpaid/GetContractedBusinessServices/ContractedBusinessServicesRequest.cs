using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetContractedBusinessServices
{
    [DataContract(Name = "ContractedBusinessServicesRequestPostPaid")]
    public class ContractedBusinessServicesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string System { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string ContractId { get; set; }
        [DataMember]
        public string DesPlan { get; set; }
        [DataMember]
        public string ServiceCode { get; set; }
        [DataMember]
        public string Application { get; set; }
        [DataMember]
        public string plataformaAT { get; set; }
        [DataMember]
        public string coIdPub { get; set; }
        [DataMember]
        public string flagMigrado { get; set; }
        [DataMember]
        public string origen { get; set; }

    }
}
