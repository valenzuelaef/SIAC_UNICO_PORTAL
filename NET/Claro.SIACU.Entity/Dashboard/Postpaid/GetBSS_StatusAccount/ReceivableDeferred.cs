using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "cobranzaDiferida")]
    public class ReceivableDeferred
    {
        [DataMember(Name = "cuenta")]
        public string account { get; set; }

        [DataMember(Name = "razonSocial")]
        public string businessName { get; set; }

        [DataMember(Name = "nroOcc")]
        public string nroOcc { get; set; }

        [DataMember(Name = "factSeleccionada")]
        public string selectedInvoice { get; set; }

        [DataMember(Name = "periodos")]
        public string periods { get; set; }

        [DataMember(Name = "importe")]
        public string amount{ get; set; }

        [DataMember(Name = "nombreOcc")]
        public string nameOcc { get; set; }

        [DataMember(Name = "comentario")]
        public string commentary { get; set; }

        [DataMember(Name = "fechaIngreso")]
        public string admissionDate { get; set; }

        [DataMember(Name = "cuentaContable")]
        public string accountingAccount { get; set; }

        [DataMember(Name = "fechaValidez")]
        public string dateValidity { get; set; }
    }
}
