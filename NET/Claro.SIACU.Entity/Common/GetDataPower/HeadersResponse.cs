using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetDataPower
{
    [DataContract]
	public class HeadersResponse
	{
        [DataMember(Name = "HeaderResponse")]
        public HeaderResponse HeaderResponse;
	}
}
