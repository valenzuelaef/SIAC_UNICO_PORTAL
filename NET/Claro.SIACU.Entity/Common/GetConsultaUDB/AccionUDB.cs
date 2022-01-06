using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetConsultaUDB
{
    [DataContract(Name = "AccionUDBCommon")]
    public class AccionUDB
    {
        [DataMember]
        public string idAccion { get; set; }
        [DataMember]
        public List<Entity.Common.GetConsultaUDB.ListaParametro> lstParametro { get; set; }
        
    }
}
