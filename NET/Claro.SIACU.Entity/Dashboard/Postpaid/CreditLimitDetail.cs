using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "CreditLimitDetailPostPaid")]
    public class CreditLimitDetail
    {
        [Data.DbColumn("pv_codaplicacion")]
        [DataMember]
        public string PERIODO { get; set; }

        [Data.DbColumn("pv_usuario")]
        [DataMember]
        public string USUARIO { get; set; }

        [Data.DbColumn("pv_tipo_servicio")]
        [DataMember]
        public string TIPO_SERVICIO { get; set; }

        [Data.DbColumn("pv_cod_cuenta")]
        [DataMember]
        public string COD_CUENTA { get; set; }

        [Data.DbColumn("pv_tipo_documento")]
        [DataMember]
        public string TIPO_DOCUMENTO { get; set; }

        [Data.DbColumn("pv_nro_documento")]
        [DataMember]
        public string NRO_DOCUMENTO { get; set; }

        [Data.DbColumn("xv_id_doc_oac")]
        [DataMember]
        public string ID_DOC_OAC { get; set; }

        [Data.DbColumn("xv_estado_documento")]
        [DataMember]
        public string ESTADO_DOCUMENTO { get; set; }

        [Data.DbColumn("xv_fec_pago")]
        [DataMember]
        public string FEC_PAGO { get; set; }

        [Data.DbColumn("xv_tipo_moneda")]
        [DataMember]
        public string TIPO_MONEDA { get; set; }

        [Data.DbColumn("xv_importe_original")]
        [DataMember]
        public string IMPORTE_ORIGINAL { get; set; }

        [Data.DbColumn("xv_importe_pagado")]
        [DataMember]
        public string IMPORTE_PAGADO { get; set; }

        [Data.DbColumn("xv_saldo")]
        [DataMember]
        public decimal SALDO { get; set; }

        [Data.DbColumn("xv_importe_reclamado")]
        [DataMember]
        public decimal IMPORTE_RECLAMADO { get; set; }

        [Data.DbColumn("xv_importe_fco")]
        [DataMember]
        public string IMPORTE_FCO { get; set; }

        [Data.DbColumn("xv_importe_financiamiento")]
        [DataMember]
        public string IMPORTE_FINANCIAMIENTO { get; set; }

        [Data.DbColumn("xv_saldo_fco")]
        [DataMember]
        public string SALDO_FCO { get; set; }

        [Data.DbColumn("xv_saldo_financiamiento")]
        [DataMember]
        public string SALDO_FINANCIAMIENTO { get; set; }

        [Data.DbColumn("xv_cod_rpta")]
        [DataMember]
        public string COD_RPTA { get; set; }

        [Data.DbColumn("xv_msg_rpta")]
        [DataMember]
        public string MSG_RPTA { get; set; }

        [DataMember]
        public decimal MONTO_PAGO { get; set; }

        [DataMember]
        public string NRO_PAC { get; set; }

    }
}
