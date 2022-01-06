using Claro.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DeferredCollectionsHelper
{
    public class DeferredCollections : IExcel
    {
        [Header(Title = "Nro", Order = Claro.Constants.NumberZero)]
        public int Nro { get; set; }
        [Header(Title = "Documento", Order = Claro.Constants.NumberOne)]
        public string Comentario { get; set; }
        [Header(Title = "Fecha Ingreso", Order = Claro.Constants.NumberTwo)]
        public string FechaIngreso { get; set; }
        [Header(Title = "Importe(con IGV)", Order = Claro.Constants.NumberThree)]
        public decimal Importe { get; set; }
        [Header(Title = "Estado", Order = Claro.Constants.NumberFour)]
        public string Periodo { get; set; }
        public string CuentaContable { get; set; }
        public string FechaValidez { get; set; }
        public string Cuenta { get; set; }
        public string RazonSocial { get; set; }
        public string NroOCC { get; set; }
        public string FacturaSeleccionada { get; set; }
        public string NombreOCC { get; set; }
    }
}