using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer
{
    [DataContract(Name = "CustomerRequestPostPaid")]
    public class CustomerRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TypeSearch { get; set; }
        [DataMember]
        public string AccountCustomer { get; set; }
        [DataMember]
        public string NumCellphone { get; set; }
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
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string Plataform { get; set; }
        [DataMember]
        public Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat outOptional { get; set; }
        [DataMember]
        public string strTecnologia { get; set; } //INICIATIVA-616

    }
}
