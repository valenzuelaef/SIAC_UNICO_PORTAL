using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DbTable("TEMPO")]
    [DataContract(Name = "RechargePrePaid")]
    public class Recharge
    {
        [DbColumn("FECHARECARGA")]
        [DataMember]
        public string FECHARECARGA { get; set; }

        [DbColumn("TIPORECARGA")]
        [DataMember]
        public string TIPORECARGA { get; set; }

        [DbColumn("MONTONOMINAL")]
        [DataMember]
        public string MONTONOMINAL { get; set; }

        [DbColumn("MONTOEFECTIVO")]
        [DataMember]
        public string MONTOEFECTIVO { get; set; }

        [DbColumn("CREDDEBI")]
        [DataMember]
        public string CREDDEBI { get; set; }

        [DbColumn("BOLSARECARGA")]
        [DataMember]
        public string ORIGENRECARGA { get; set; }

        [DbColumn("DETRECARGA")]
        [DataMember]
        public string DETRECARGA { get; set; }

        [DbColumn("SALDO")]
        [DataMember]
        public string SALDO { get; set; }

        [DbColumn("DESTIPORECARGA")]
        [DataMember]
        public string DESTIPORECARGA { get; set; }

        [DataMember]
        public string DESDE { get; set; }

        [DataMember]
        public string HASTA { get; set; }

        [DataMember]
        public string DIAS { get; set; }

        [DataMember]
        public string SOLICITUD { get; set; }

        [DataMember]
        public string ESTADO { get; set; }

        [DataMember]
        public string NUMREGISTRO { get; set; }

        [DataMember]
        public string TIPOMOVIMIENTO { get; set; }

        [DataMember]
        public string BOLSA { get; set; }

        [DataMember]
        public string TIPOSALDO { get; set; }

        [DataMember]
        public string DESCRIPCION { get; set; }

        [DataMember]
        public string DETALLE { get; set; }

        [DataMember]
        public string TIPOEVENTO { get; set; }

        [DataMember]
        public string SALDOFINAL { get; set; }

        [DataMember]
        public string FECHAEXPIRACION { get; set; }

    }
}