using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "BagBalanceCBIOSPostPaid")]
    public class BagBalanceCBIOS
    {
        [DataMember]
        public string CodigoConsumo { get; set; }
        [DataMember]
        public string Bolsa { get; set; }
        [DataMember]
        public decimal Saldo { get; set; }
        [DataMember]
        public decimal Consumo { get; set; }
        [DataMember]
        public decimal UnidadesAsignadas { get; set; }
        [DataMember]
        public decimal lineLimit { get; set; }
        [DataMember]
        public decimal lineConsumption { get; set; }
        [DataMember]
        public decimal LimiteDisponible { get; set; }
        [DataMember]
        public string SharedProduct { get; set; }
        [DataMember]
        public string ChargeTypeCode { get; set; }
        [DataMember]
        public string Type { get; set; }
      


    }
}
