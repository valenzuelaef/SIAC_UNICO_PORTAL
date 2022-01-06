using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice
{
    public class CancelInvoiceBodyRequest
    {
        [DataMember(Name = "numeroDocumento")]
        public string numeroDocumento { get; set; }

        [DataMember(Name = "cuenta")]
        public string cuenta { get; set; }

        [DataMember(Name = "motivoAnulacion")]
        public string motivoAnulacion { get; set; }

        [DataMember(Name = "codigoSistemaOrigen")]
        public string codigoSistemaOrigen { get; set; }

        [DataMember(Name = "comentarios")]
        public string comentarios { get; set; }

        [DataMember(Name = "periodo")]
        public string periodo { get; set; }

        [DataMember(Name = "fechaContable")]
        public string fechaContable { get; set; }

        [DataMember(Name = "puntoAtencion")]
        public string puntoAtencion { get; set; }

        [DataMember(Name = "usuarioAtencion")]
        public string usuarioAtencion { get; set; }

        [DataMember(Name = "interaccionConNivelType")]
        public InteractionWithLevelType interaccionConNivelType { get; set; }

        [DataMember(Name = "interaccionPlusType")]
        public InteractionPlusType interaccionPlusType { get; set; }
    }
}
