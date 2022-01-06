using System;

namespace Claro.SIACU.Entity.Cases
{
    [Data.DbTable("TEMPO")]
    public class Case
    {
        [Data.DbColumn("ID_CASE")]
        public string IdCase { get; set; }

        [Data.DbColumn("FECHA_CREACION")]
        public DateTime CreationDate { get; set; }

        [Data.DbColumn("TELEFONO")]
        public string Telephone { get; set; }

        [Data.DbColumn("TIPIFICACION")]
        public string Typification { get; set; }

        [Data.DbColumn("ORIGEN")]
        public string Origin { get; set; }

        [Data.DbColumn("ESTADO")]
        public string State { get; set; }

        [Data.DbColumn("PRIORIDAD")]
        public string Priority { get; set; }

        [Data.DbColumn("INCIDENCIA_SD")]
        public string IncidenceSD { get; set; }

        [Data.DbColumn("FASE")]
        public string Phase { get; set; }

        [Data.DbColumn("REPORTE")]
        public string Report { get; set; }

        [Data.DbColumn("RECLAMO")]
        public string Complaint { get; set; }

        [Data.DbColumn("CONDICION")]
        public string Condition { get; set; }

        [Data.DbColumn("COLA")]
        public string Queue { get; set; }

        [Data.DbColumn("PROPIETARIO")]
        public string Owner { get; set; }

        [Data.DbColumn("NOMBRE_AGENTE")]
        public string AgentName { get; set; }

        [Data.DbColumn("APELLIDO_AGENTE")]
        public string AgentLastName { get; set; }

        [Data.DbColumn("RESULTADO")]
        public string Result { get; set; }

        [Data.DbColumn("RESOLUCION")]
        public string Resolution { get; set; }

        [Data.DbColumn("COORDENADA1")]
        public string Coordinate1 { get; set; }

        [Data.DbColumn("COORDENADA2")]
        public string Coordinate2 { get; set; }

        [Data.DbColumn("COORDENADA3")]
        public string Coordinate3 { get; set; }

        [Data.DbColumn("FECHA_CAMBIO_FASE")]
        public string PhaseChangeDate { get; set; }

        [Data.DbColumn("INCOVENIENTE")]
        public string InconvenientDetail { get; set; }
    }
}