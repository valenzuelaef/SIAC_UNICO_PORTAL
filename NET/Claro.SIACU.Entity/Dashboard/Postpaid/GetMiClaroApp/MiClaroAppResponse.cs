using Claro.SIACU.Entity.Common.GetDataPower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMiClaroApp
{
    public class MiClaroAppResponse
    {
        [DataMember(Name = "MessageResponse")]
        public MessageResponseAppMiClaro MessageResponse { get; set; }

        public MiClaroAppResponse()
        {
            MessageResponse = new MessageResponseAppMiClaro();
        }
    }

    public class MessageResponseAppMiClaro
    {
        [DataMember(Name = "Header")]
        public HeadersResponse header { get; set; }
        [DataMember(Name = "Body")]
        public BodyAppMiClaroResponse body { get; set; }
    }

    [DataContract]
    [Serializable]
    public class BodyAppMiClaroResponse
    {

        [DataMember(Name = "mensajeError")]
        public string mensajeError { get; set; }
        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }
        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }
        [DataMember(Name = "datosAppClaro")]
        public DataAppMiClaro datosAppClaro { get; set; }
    }

    public class DataAppMiClaro
    {
        [DataMember(Name = "numeroLinea")]
        public string numeroLinea { get; set; }
        [DataMember(Name = "flagAppClaro")]
        public string flagAppClaro { get; set; }
        [DataMember(Name = "ultimaTransaccion")]
        public string ultimaTransaccion { get; set; }
        [DataMember(Name = "version")]
        public string version { get; set; }
    }
}
