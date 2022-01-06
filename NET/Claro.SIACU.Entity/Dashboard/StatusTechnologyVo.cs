using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DataContract(Name = "StatusTechnologyVo")]
    public class StatusTechnologyVo
    {
        [DataMember]
        public string TechnologyVoLte { get; set; }

        [DataMember]
        public string TechnologyVoWifi { get; set; }

        [DataMember]
        public string MessageError { get; set; }

    }
}
