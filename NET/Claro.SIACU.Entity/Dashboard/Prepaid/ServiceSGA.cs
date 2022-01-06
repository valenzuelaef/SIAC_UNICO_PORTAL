using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract (Name = "ServiceSGAPrepaid")]
    public class ServiceSGA
    {
    [DataMember]
    public string CODIGO_RECARGA { get; set; }
    [DataMember]
    public string SERVICIO { get; set; }
    [DataMember]
    public string NUMREGISTRO { get; set; }
    [DataMember]
    public string ESTADO_REGISTRO { get; set; }
    [DataMember]
    public string PAQUETE_VENTA { get; set; }
    [DataMember]
    public string PRODUCTO { get; set; }
    [DataMember]
    public string FECHA_ACTIVACION { get; set; }
    [DataMember]
    public string FECINIVIG { get; set; }
    [DataMember]
    public string FECFINVIG { get; set; }
    [DataMember]
    public string FECCORTE { get; set; }
    [DataMember]
    public string CODIGO_SGA { get; set; }
    }
}
