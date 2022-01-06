using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData
{
    [DataContract(Name = "SalesDataResponse")]
    public class SalesDataResponse
    {
        [DataMember]
        public Entity.Dashboard.Prepaid.DataSalePEL objDataSalePEL { get; set; }
    }
}
