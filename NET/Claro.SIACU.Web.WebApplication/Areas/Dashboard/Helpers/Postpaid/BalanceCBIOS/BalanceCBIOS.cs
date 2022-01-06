using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS
{
    public class BalanceCBIOS
    {

        public string CodigoConsumo { get; set; }

        public string Bolsa { get; set; }

        public decimal Saldo { get; set; }

        public decimal Consumo { get; set; }

        public decimal UnidadesAsignadas { get; set; }

        public decimal lineLimit { get; set; }

        public decimal lineConsumption { get; set; }

        public decimal LimiteDisponible { get; set; }

        public string SharedProduct { get; set; }

        public string ChargeTypeCode { get; set; }

        public string Type { get; set; }
    }
}