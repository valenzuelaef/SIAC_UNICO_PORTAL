using Claro.Data;
using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DbTable("TEMPO")]
    [DataContract(Name = "TypeTriationPrePaid")]
    public class TypeTriation
    {
        [DbColumn("CODIGO_TRIACION")]
        [DataMember]
        public int Codigo { get; set; }

        [DbColumn("DESCRIPCION")]
        [DataMember]
        public string Descripcion { get; set; }

        [DbColumn("ID_TRIACION")]
        [DataMember]
        public int IdTriacion { get; set; }

        [DbColumn("DESCRIPCION_ALTERNA")]
        [DataMember]
        public string DescripcionAlterna { get; set; }

        [DbColumn("POSICION_SUBSCRIBER")]
        [DataMember]
        public int PosicionSubscriber { get; set; }

        [DbColumn("FLAG_SUBSCRIBER")]
        [DataMember]
        public string FlagSubscriber { get; set; }

        [DbColumn("VIGENCIA_INICIO")]
        [DataMember]
        public DateTime VigenciaInicio { get; set; }

        [DbColumn("VIGENCIA_FIN")]
        [DataMember]
        public DateTime VigenciaFin { get; set; }

        [DbColumn("GRATUIDAD_INICIO")]
        [DataMember]
        public DateTime GratuidadInicio { get; set; }

        [DbColumn("GRATUIDAD_FIN")]
        [DataMember]
        public DateTime GratuidadFin { get; set; }

        [DbColumn("COBRO_INSCRIPCION")]
        [DataMember]
        public double CobroInscripcion { get; set; }

        [DbColumn("COBRO_CAMBIO")]
        [DataMember]
        public double CobroCambio { get; set; }

        [DbColumn("MENSAJE")]
        [DataMember]
        public string mensaje { get; set; }
    }
}