using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "PetitionstPostPaid")]
    public class Petitions
    {


        [Data.DbColumn("FECHA_PETICION")]
        [DataMember]
        public DateTime Fecha_Peticion { get; set; }


        [Data.DbColumn("FECHA_VENCIMIENTO")]
        [DataMember]
        public DateTime Fecha_Vencimiento { get; set; }


        [Data.DbColumn("ESTADO_PETICION")]
        [DataMember]
        public string Estado { get; set; }


        [Data.DbColumn("ACCION_SOLICITADA")]
        [DataMember]
        public string Accion { get; set; }

        [Data.DbColumn("USU_REG")]
        [DataMember]
        public string Usuario { get; set; }

        [Data.DbColumn("ID_ORDER")]
        [DataMember]
        public string OrdenId { get; set; }
    }
}
