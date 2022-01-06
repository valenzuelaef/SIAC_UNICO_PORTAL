using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental;
using System.Collections.Generic;
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class StockWarehouseModal
    {
        public StockWarehouseModal()
        {
            listStockWarehouse = new List<StockWarehouse>();
        }
        public List<StockWarehouse> listStockWarehouse { get; set; }
    }
}