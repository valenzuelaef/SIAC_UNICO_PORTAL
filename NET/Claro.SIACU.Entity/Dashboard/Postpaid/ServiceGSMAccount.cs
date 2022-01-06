using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ServiceGSMAccountPostPaid")]
    public class ServiceGSMAccount
    {
        [Data.DbColumn("NroServ")]
        [DataMember]
        public string NRO_SERV { get; set; }

        [Data.DbColumn("CO_ID")]
        [DataMember]
        public string CONTRATO { get; set; }

        [Data.DbColumn("ESTADO")]
        [DataMember]
        public string ESTADO { get; set; }

        [Data.DbColumn("PLAN_TARIFARIO")]
        [DataMember]
        public string PLAN_TARIFARIO { get; set; }

        [Data.DbColumn("COMBO")]
        [DataMember]
        public string COMBO { get; set; }

        [Data.DbColumn("DESCUENTO")]
        [DataMember]
        public string DESCUENTO { get; set; }

        [Data.DbColumn("DirecInstalac")]
        [DataMember]
        public string DIR_INSTALACION { get; set; }

      
        [DataMember]
        public string COD_PLAN { get; set; }

        [DataMember]
        public string COD_COMBO { get; set; }
        

    }
}
