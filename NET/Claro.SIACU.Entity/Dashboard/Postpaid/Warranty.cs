using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "WarrantyPostPaid")]
    public class Warranty
    {
        public Warranty()
        {
            lstCabAccountWarranty = new List<Warranty>();
            lstDebAccountWarranty = new List<Warranty>();
        }

        [DataMember]
        public string NUMERO { get; set; }

        [DataMember]
        public string FECHA { get; set; }

        [DataMember]
        public string ESTADO { get; set; }

        [DataMember]
        public string MONTO { get; set; }

        [DataMember]
        public string SALDO { get; set; }

        
        [DataMember]
        [Data.DbColumn("TIP_DESC")]
        public string TIPO_DOC { get; set; }

        
        [DataMember]
        [Data.DbColumn("TIP_VALOR")]
        public string TIPO_COD_DOC { get; set; }

        [DataMember]
        public List<Warranty> lstCabAccountWarranty { get; set; }

        [DataMember]
        public List<Warranty> lstDebAccountWarranty { get; set; }
    }
}