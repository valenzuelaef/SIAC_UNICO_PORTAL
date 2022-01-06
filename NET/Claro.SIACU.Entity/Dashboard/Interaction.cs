using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard
{
    [DataContract(Name = "Interaction")]
    public class Interaction
    {
        [DataMember]
        public string CLASE { get; set; }
        [DataMember]
        public string CODIGOEMPLEADO { get; set; }
        [DataMember]
        public string CODIGOSISTEMA { get; set; }
        [DataMember]
        public string FLAGCASO { get; set; }
        [DataMember]
        public string HECHOENUNO { get; set; }
        [DataMember]
        public string METODOCONTACTO { get; set; }
        [DataMember]
        public string NOTAS { get; set; }
        [DataMember]
        public string SITEOBJID { get; set; }
        [DataMember]
        public string SUBCLASE { get; set; }
        [DataMember]
        public string TELEFONO { get; set; }
        [DataMember]
        public string TEXTRESULTADO { get; set; }
        [DataMember]
        public string TIPO { get; set; }
        [DataMember]
        public string TIPOINTERACCION { get; set; }

    }
}
