using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse
{
    [DataContract(Name = "StockWarehouseResponseDashboard")]
    public class StockWarehouseResponse
    {
        private List<Entity.Dashboard.Postpaid.StockWarehouse> m_listStockWarehouse;

        [DataMember]
        public List<Entity.Dashboard.Postpaid.StockWarehouse> listStockWarehouse
        {
            get
            {
                if (this.m_listStockWarehouse == null)
                {
                    this.m_listStockWarehouse = new List<StockWarehouse>();
                }

                return this.m_listStockWarehouse;
            }
            set
            {
                this.m_listStockWarehouse = value;
            }
        }
    }
}
