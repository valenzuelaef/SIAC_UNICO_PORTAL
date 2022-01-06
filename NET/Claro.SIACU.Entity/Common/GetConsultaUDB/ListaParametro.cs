using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Common.GetConsultaUDB
{
    [DataContract(Name = "ListaParametroCommon")]
    public class ListaParametro
    {
        [DataMember]
        public string nombreLista { get; set; }
        [DataMember]
        public List<Entity.ItemOpcional> lstParametro { get; set; }
        [DataMember]
        public List<Entity.Common.GetConsultaUDB.SubListaParametro> lstSubParametro { get; set; }
    }
}
