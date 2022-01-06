﻿using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice
{
    public class CancelInvoiceBodyResponse
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }

        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }
    }
}
