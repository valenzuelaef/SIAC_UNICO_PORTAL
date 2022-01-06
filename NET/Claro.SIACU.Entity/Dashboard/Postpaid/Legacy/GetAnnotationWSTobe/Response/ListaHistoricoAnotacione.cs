using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Response
{
    public class ListaHistoricoAnotacione
    {
        public string cuenta { get; set; }
        public string codigoTickler { get; set; }
        public string numeroTickler { get; set; }
        public string estado { get; set; }
        public string estadoAnotacion { get; set; }
        public string prioridad { get; set; }
        public string descripcionCorta { get; set; }
        public string descripcionLarga { get; set; }
        public string accionSeg { get; set; }
        public string usuarioSeg { get; set; }
        public string usuarioCierre { get; set; }
        public string usuarioApertura { get; set; }
        public string usuarioModificacion { get; set; }
        public string fechaSeg { get; set; }
        public string fechaApertura { get; set; }
        public string fechaCierre { get; set; }
        public string fechaModif { get; set; }
    }
}
