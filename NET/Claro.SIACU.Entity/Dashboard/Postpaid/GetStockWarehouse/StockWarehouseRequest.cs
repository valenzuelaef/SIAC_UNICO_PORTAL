using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse
{
    [DataContract(Name = "StockWarehouseRequestDashboard")]
    public class StockWarehouseRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strSerie { get; set; }
        [DataMember]
        public string strDescripcion { get; set; }
        [DataMember]
        public string strRegion { get; set; }
    }
}
