using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetService
{
    [DataContract(Name = "ServiceRequestPostPaid")]
    public class ServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strphonespeed { get; set; }
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public string ContractID { get; set; }
        [DataMember]
        public string Application { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string IpApplication { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string UserApplication { get; set; }
        [DataMember]
        public string CustomerType { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public string Modality { get; set; }
        [DataMember]
        public string plataformaAT { get; set; }
        [DataMember]
        public string flagConvivencia { get; set; }
        [DataMember]
        public string coIdPub { get; set; }
        [DataMember]
        public string csIdPub { get; set; }
        [DataMember]
        public string coId { get; set; }
        
    }
}
