using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetDataCustomer
{
    [DataContract(Name = "DataCustomerRequestDashboard")]
    public class DataCustomerRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TypeSearch { get; set; }
        [DataMember]
        public string AccountCustomer { get; set; }
        [DataMember]
        public string NroCellphone { get; set; }
        [DataMember]
        public string Application { get; set; }
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string IpApplication { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string UserApplication { get; set; }


    }
}
