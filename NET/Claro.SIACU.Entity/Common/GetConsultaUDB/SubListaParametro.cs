using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetConsultaUDB
{
    [DataContract(Name = "SubListaParametroCommon")]
    public class SubListaParametro
    {
        [DataMember]
        public string nombreLista { get; set; }
        [DataMember]
        public List<Entity.ItemOpcional> lstParametro { get; set; }
    }
}
