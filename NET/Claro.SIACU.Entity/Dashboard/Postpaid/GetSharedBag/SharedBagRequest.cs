using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSharedBag
{
    [DataContract(Name = "SharedBagRequestPostPaid")]
    public class SharedBagRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string Application { get; set; }
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string IPAplication { get; set; }
        [DataMember]
        public string UsrAplication { get; set; }
        [DataMember]
        public string Customerid { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string FlagQuery { get; set; }
        [DataMember]
        public string MesageText { get; set; }
        [DataMember]
        public string TypeCustomer { get; set; }
        

    }
}
