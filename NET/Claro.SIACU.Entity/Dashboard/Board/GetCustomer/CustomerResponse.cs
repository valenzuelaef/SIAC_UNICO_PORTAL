using Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Board.GetCustomer
{
    [DataContract(Name = "CustomerResponseDashboard")]
    public class CustomerResponse
    {
        [DataMember]
        public Entity.Dashboard.ICustomer InterfaceCustomer { get; set; }
        [DataMember]
        public string ApplicationType { get; set; }
        [DataMember]
        public string CodeResponse { get; set; }
        [DataMember]
        public string MsjValidation { get; set; }
        [DataMember]
        public List<Entity.Dashboard.Postpaid.IndicatorIGV> ListIndicatorIGV { get; set; }
        [DataMember]
        public OptionsResponse objOptions { get; set; }
        [DataMember]
        public Itm itm { get; set; }
    }
}
