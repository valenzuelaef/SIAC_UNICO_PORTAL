using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetDataPower
{
    [DataContract]
	public class HeaderRequest
	{
        [DataMember(Name = "consumer")]
		public string consumer {get; set;}
        [DataMember(Name = "country")]
        public string country { get; set; }
        [DataMember(Name = "dispositivo")]
        public string dispositivo { get; set; }
        [DataMember(Name = "language")]
        public string language { get; set; }
        [DataMember(Name = "modulo")]
        public string modulo { get; set; }
        [DataMember(Name = "msgType")]
        public string msgType { get; set; }
        [DataMember(Name = "operation")]
        public string operation { get; set; }
        [DataMember(Name = "pid")]
        public string pid { get; set; }
        [DataMember(Name = "system")]
        public string system { get; set; }
        [DataMember(Name = "timestamp")]
        public string timestamp { get; set; }
        [DataMember(Name = "userId")]
        public string userId { get; set; }
        [DataMember(Name = "wsIp")]
        public string wsIp { get; set; }
        [DataMember(Name = "VarArg")]
        public string VarArg { get; set; }
	}
}
