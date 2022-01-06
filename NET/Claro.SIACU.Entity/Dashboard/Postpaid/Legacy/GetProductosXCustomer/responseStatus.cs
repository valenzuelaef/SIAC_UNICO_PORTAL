using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer
{
    [DataContract]
    public class responseStatus
    {
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string codigoRespuesta { get; set; }
        [DataMember]
        public string descripcionRespuesta { get; set; }
        [DataMember]
        public string ubicacionError { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string origen { get; set; }
    }
}
