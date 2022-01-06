using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "FeeEquipmentPostPaid")]
    public class FeeEquipment
    {
        [DataMember]
        public int CorrelativoId { get; set; }


        [Data.DbColumn("TIPO_DOCUMENTO")]
        [DataMember]
        public string Tipo_documento { get; set; }


        [Data.DbColumn("NUMERO_DOCUMENTO")]
        [DataMember]
        public string numero_documento { get; set; }


        [Data.DbColumn("NUMERO_SISACT")]
        [DataMember]
        public string numero_sisact { get; set; }


        [Data.DbColumn("NUMERO_CVE_FINANCIAMIENTO")]
        [DataMember]
        public string numero_cve_financiamiento { get; set; }


        [Data.DbColumn("NUMBER_IMEI")]
        [DataMember]
        public string numero_imei { get; set; }


        [Data.DbColumn("FECHA_EMISION_CUOTA")]
        [DataMember]
        public string fecha_emision_cuota { get; set; }


        [Data.DbColumn("FECHA_VENCIMIENTO_CUOTA")]
        [DataMember]
        public string fecha_vencimiento_cuota { get; set; }


        [Data.DbColumn("IMPORTE_ORIGINAL_CUOTA")]
        [DataMember]
        public string importe_original_cuota { get; set; }


        [Data.DbColumn("SALDO_PENDIENTE_CUOTA")]
        [DataMember]
        public string saldo_pendiente_cuota { get; set; }

        [Data.DbColumn("ESTADO_CUOTA")]
        [DataMember]
        public string estado_cuota { get; set; }

    }
}
