using Claro.SIACU.Entity.Common.GetDataPower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMiClaroApp
{
    public class MiClaroAppRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public MessageRequestAppMiclaro MessageRequest { get; set; }

        public MiClaroAppRequest()
        {
            MessageRequest = new MessageRequestAppMiclaro();
        }
    }

    [DataContract]
    [Serializable]
    public class MessageRequestAppMiclaro
    {
        [DataMember(Name = "Header")]
        public HeadersRequest header { get; set; }
        [DataMember(Name = "Body")]
        public BodyAppMiClaroRequest body { get; set; }

        public MessageRequestAppMiclaro()
        {
            header = new HeadersRequest();
            body = new BodyAppMiClaroRequest();
        }
    }

    public class BodyAppMiClaroRequest
    {
        [DataMember(Name = "numeroLinea")]
        public string numeroLinea { get; set; }
    }
}
