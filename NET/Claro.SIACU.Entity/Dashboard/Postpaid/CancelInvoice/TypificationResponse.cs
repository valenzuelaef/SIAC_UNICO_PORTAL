using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice
{
    public class TypificationResponse
    {
        [DataMember]
        public Typification ObjTypification { get; set; }

        [DataMember]
        public List<Typification> ListTypification { get; set; }

        [DataMember]
        public string ObjSite { get; set; }

        [DataMember]
        public string ContactSite { get; set; }

        [DataMember]
        public string Account { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }
    }

    public class Typification
    {
        [DataMember]
        //[Data.DbColumn("TIPO")]
        public string TIPO { get; set; }

        [DataMember]
        //[Data.DbColumn("CLASE")]
        public string CLASE { get; set; }

        [DataMember]
        //[Data.DbColumn("SUBCLASE")]
        public string SUBCLASE { get; set; }

        [DataMember]
        //[Data.DbColumn("INTERACCION_CODE")]
        public string INTERACCION_CODE { get; set; }

        [DataMember]
        //[Data.DbColumn("TIPO_CODE")]
        public string TIPO_CODE { get; set; }

        [DataMember]
        //[Data.DbColumn("CLASE_CODE")]
        public string CLASE_CODE { get; set; }

        [DataMember]
        //[Data.DbColumn("SUBCLASE_CODE")]
        public string SUBCLASE_CODE { get; set; }
    }
}
