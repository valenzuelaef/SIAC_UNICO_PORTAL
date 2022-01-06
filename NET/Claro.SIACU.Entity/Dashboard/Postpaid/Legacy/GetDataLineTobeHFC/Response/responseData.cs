using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Response
{
    public class responseData
    {
        public string telefono { get; set; }
        public string estado { get; set; }
        public string motivo { get; set; }
        public string fechaEstado { get; set; }
        public string plan { get; set; }
        public string plazoContrato { get; set; }
        public string iccid { get; set; }
        public string imsi { get; set; }
        public string campania { get; set; }
        public string venta { get; set; }
        public string nichoId { get; set; }
        public string vendedor { get; set; }
        public string coId { get; set; }
        public string fechaAct { get; set; }
        public string flagPlataforma { get; set; }
        public string pin1 { get; set; }
        public string puk1 { get; set; }
        public string pin2 { get; set; }
        public string puk2 { get; set; }
        public string codplanTarifario { get; set; }
        public string descEstado { get; set; }
        public string descMotivo { get; set; }
        public string topeConsumo { get; set; }
        public string bolsasAdicionales { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
