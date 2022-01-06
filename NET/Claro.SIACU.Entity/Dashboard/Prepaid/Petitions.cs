using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DataContract(Name = "Petitions")]
    public class Petitions
    {
        [DataMember]
        public  DateTime  Fecha_Peticion { get; set; }

        [DataMember]
        public DateTime Fecha_Vencimiento { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Accion { get; set; }

    
    }
}
