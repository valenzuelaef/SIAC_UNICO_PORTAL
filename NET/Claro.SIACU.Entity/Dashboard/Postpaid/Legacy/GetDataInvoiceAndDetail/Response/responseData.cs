using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Response
{
    public class responseData
    {
        public string numeroRecibo { get; set; }
        public string fechaEmision { get; set; }
        public string fechaVencimiento { get; set; }
        public string totalAccess { get; set; }
        public string totalSubscription { get; set; }
        public string totalOCCs { get; set; }
        public string totalOCCnIGV { get; set; }
        public string traficoLocalAdicional { get; set; }
        public string traficoLocalAConsumo { get; set; }
        public string largaDistanciaInternacional { get; set; }
        public string largaDistanciaNacional { get; set; }
        public string roamingInternacional { get; set; }
        public string totalCargosDelMes { get; set; }
        public List<Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Common.listaOpcional> listaOpcional { get; set; }
    }
}
